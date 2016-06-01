using System;
using System.Threading.Tasks;

namespace AspNetCore.Authentication.Basic
{
	/// <summary>
	/// Specifies custom callback methods for the <see cref="BasicAuthenticationHandler"/>.
	/// </summary>
	public class BasicCustomAuthenticationEvents : IBasicAuthenticationEvents
	{
		/// <summary>
		/// Gets or sets signin callback which called when authentication handler wants to validate username and password.
		/// </summary>
		public Func<BasicSignInContext, Task<BasicSignInResult>> OnSignIn { get; set; }

		/// <inheritdoc/>
		public Task<BasicSignInResult> SignIn(BasicSignInContext context)
		{
			return OnSignIn(context);
		}
	}
}