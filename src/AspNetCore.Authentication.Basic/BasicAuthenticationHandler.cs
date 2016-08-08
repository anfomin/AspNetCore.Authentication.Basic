using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features.Authentication;

namespace AspNetCore.Authentication.Basic
{
	/// <summary>
	/// Handles basic authentication via "Authorization" header.
	/// </summary>
	public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
	{
		/// <summary>
		/// HTTP request authorization header name.
		/// </summary>
		public const string AuthorizationHeader = "Authorization";

		/// <summary>
		/// HTTP response authentication header name.
		/// </summary>
		public const string WWWAuthenticateHeader = "WWW-Authenticate";

		/// <inheritdoc />
		protected override sealed async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			// get authorization header
			string auth = Request.Headers[AuthorizationHeader];
			if (auth == null)
				return AuthenticateResult.Skip();

			// confirm authorization scheme
			string[] authParts = auth.Split(' ');
			if (authParts.Length != 2)
				return AuthenticateResult.Fail("Invalid authorization header");
			else if (!ShouldHandleScheme(authParts[0], Options.AutomaticAuthenticate))
				return AuthenticateResult.Fail($"Authorization scheme \"{authParts[0]}\" is not supported");

			// extract authorization value
			string base64 = authParts[1];
			string authValue;
			try
			{
				byte[] bytes = Convert.FromBase64String(base64);
				authValue = Encoding.ASCII.GetString(bytes);
			}
			catch
			{
				authValue = null;
			}
			if (string.IsNullOrEmpty(authValue))
				return AuthenticateResult.Fail("Invalid authorization header base64 value");

			// extract username and password
			string userName;
			string password;
			int sepIndex = authValue.IndexOf(':');
			if (sepIndex == -1)
			{
				userName = authValue;
				password = null;
			}
			else
			{
				userName = authValue.Substring(0, sepIndex);
				password = authValue.Substring(sepIndex + 1);
			}

			// authenticate user
			var signInContext = new BasicSignInContext(Context, userName, password);
			var result = await Options.Events.SignIn(signInContext);
			if (!result.IsSuccess)
				return AuthenticateResult.Fail(result.Error);

			// create authentication ticket
			var ticket = new AuthenticationTicket(result.Principal, result.Properties, Options.AuthenticationScheme);
			return AuthenticateResult.Success(ticket);
		}

		/// <inheritdoc />
		protected override Task<bool> HandleUnauthorizedAsync(ChallengeContext context)
		{
			Response.Headers[WWWAuthenticateHeader] = $"{Options.AuthenticationScheme} realm=\"{Options.Realm}\"";
			return base.HandleUnauthorizedAsync(context);
		}
	}
}