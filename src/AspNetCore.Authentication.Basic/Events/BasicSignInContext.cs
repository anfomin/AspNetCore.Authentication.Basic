using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Authentication.Basic
{
	/// <summary>
	/// Contains information for Basic signin event.
	/// </summary>
	public class BasicSignInContext : BaseContext
	{
		/// <summary>
		/// Gets username from authentication header.
		/// </summary>
		public string UserName { get; private set; }

		/// <summary>
		/// Gets password from authentication header.
		/// </summary>
		public string Password { get; private set; }

		/// <summary>
		/// Initializes new instance.
		/// </summary>
		/// <param name="context">Current HTTP context.</param>
		/// <param name="userName">Username from authentication header.</param>
		/// <param name="password">Password from authentication header.</param>
		public BasicSignInContext(HttpContext context, string userName, string password)
			: base(context)
		{
			UserName = userName;
			Password = password;
		}
	}
}