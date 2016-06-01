using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Authentication.Basic
{
	/// <summary>
	/// Specifies callback methods for the <see cref="BasicAuthenticationHandler"/> which authenticates throw ASP.NET Identity.
	/// </summary>
	public class BasicIdentityAuthenticationEvents<TUser> : IBasicAuthenticationEvents
		where TUser : class
	{
		/// <inheritdoc/>
		public async Task<BasicSignInResult> SignIn(BasicSignInContext context)
		{
			var resolver = context.HttpContext.RequestServices;

			// find user and validate password
			var userManager = resolver.GetRequiredService<UserManager<TUser>>();
			var user = await userManager.FindByNameAsync(context.UserName);
			if (user == null)
				return BasicSignInResult.Fail("User is not found");
			if (await userManager.IsLockedOutAsync(user))
				return BasicSignInResult.Fail("User is locked out");
			if (!await userManager.CheckPasswordAsync(user, context.Password))
				return BasicSignInResult.Fail("Invalid user password");

			// create principal
			var signInManager = resolver.GetRequiredService<SignInManager<TUser>>();
			var principal = await signInManager.CreateUserPrincipalAsync(user);
			var props = GetAuthenticationProperties(context, user);
			return BasicSignInResult.Success(principal, props);
		}

		/// <summary>
		/// Returns extra authentication data that will be added to the authentication ticket.
		/// </summary>
		/// <param name="context">Contains information about the basic login session.</param>
		/// <param name="user">Current authenticating user.</param>
		protected virtual AuthenticationProperties GetAuthenticationProperties(BasicSignInContext context, TUser user)
		{
			return new AuthenticationProperties();
		}
	}
}