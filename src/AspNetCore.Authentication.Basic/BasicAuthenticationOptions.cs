using AspNetCore.Authentication.Basic;

namespace Microsoft.AspNetCore.Builder
{
	/// <summary>
	/// Options used by <see cref="BasicAuthenticationHandler"/>.
	/// </summary>
	public class BasicAuthenticationOptions : AuthenticationOptions
	{
		/// <summary>
		/// Authentication scheme for basic authentication.
		/// </summary>
		public const string BasicAuthenticationScheme = "Basic";

		/// <summary>
		/// Default authentication realm.
		/// </summary>
		public const string DefaultRealm = "App";

		/// <summary>
		/// Gets or sets protection space name. Realms allow the protected resources on
		/// a server to be partitioned into a set of protection spaces, each with its own
		/// authentication scheme and/or authorization database.
		/// </summary>
		public string Realm { get; set; } = DefaultRealm;

		/// <summary>
		/// The Provider may be assigned to an instance of an object created by the application at startup time. The middleware
		/// calls methods on the provider which give the application control at certain points where processing is occurring. 
		/// If it is not provided a default instance is supplied which does nothing when the methods are called.
		/// </summary>
		public IBasicAuthenticationEvents Events { get; set; }

		/// <summary>
		/// Initializes new instance.
		/// </summary>
		public BasicAuthenticationOptions()
		{
			AutomaticAuthenticate = true;
			AutomaticChallenge = true;
			AuthenticationScheme = BasicAuthenticationScheme;
		}
	}
}