using System.Threading.Tasks;

namespace AspNetCore.Authentication.Basic
{
	/// <summary>
	/// Specifies callback methods which the <see cref="BasicAuthenticationHandler"/> invokes to enable developer control over the authentication process.
	/// </summary>
	public interface IBasicAuthenticationEvents
	{
		/// <summary>
		/// Called when authentication handler wants to validate username and password.
		/// </summary>
		/// <param name="context">Contains information about the basic login session.</param>
		/// <returns>Result that contains success or fail login information.</returns>
		Task<BasicSignInResult> SignIn(BasicSignInContext context);
	}
}