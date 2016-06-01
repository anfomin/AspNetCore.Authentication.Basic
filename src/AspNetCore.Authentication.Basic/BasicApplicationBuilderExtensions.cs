using System;
using System.Threading.Tasks;
using AspNetCore.Authentication.Basic;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
	/// <summary>
	/// Extension methods for <see cref="IApplicationBuilder"/> to add <see cref="BasicAuthenticationMiddleware"/> middleware.
	/// </summary>
	public static class BasicApplicationBuilderExtensions
	{
		/// <summary>
		/// Adds a basic authentication to your web application pipeline.
		/// You must specify signin event in options.
		/// </summary>
		/// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
		/// <param name="options">Options for <see cref="BasicAuthenticationMiddleware"/>.</param>
		public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder app, BasicAuthenticationOptions options)
		{
			if (options == null)
				throw new ArgumentNullException(nameof(options));
			return app.UseMiddleware<BasicAuthenticationMiddleware>(Options.Create(options));
		}

		/// <summary>
		/// Adds a basic authentication to your web application pipeline.
		/// </summary>
		/// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
		/// <param name="onSignIn">Signin callback which called when authentication handler wants to validate username and password.</param>
		public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder app, Func<BasicSignInContext, Task<BasicSignInResult>> onSignIn)
		{
			var options = new BasicAuthenticationOptions
			{
				Events = new BasicCustomAuthenticationEvents { OnSignIn = onSignIn }
			};
			return app.UseMiddleware<BasicAuthenticationMiddleware>(Options.Create(options));
		}

		/// <summary>
		/// Adds basic authentication via ASP.NET Identity to the application pipeline.
		/// </summary>
		/// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
		/// <param name="options">Options for <see cref="BasicAuthenticationMiddleware"/>.</param>
		/// <typeparam name="TUser">Type of ASP.NET Identity user.</typeparam>
		public static IApplicationBuilder UseBasicAuthentication<TUser>(this IApplicationBuilder app, BasicAuthenticationOptions options)
			where TUser : class
		{
			if (options == null)
				throw new ArgumentNullException(nameof(options));
			options.Events = new BasicIdentityAuthenticationEvents<TUser>();
			return UseBasicAuthentication(app, options);
		}

		/// <summary>
		/// Adds basic authentication via ASP.NET Identity to the application pipeline.
		/// </summary>
		/// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
		/// <typeparam name="TUser">Type of ASP.NET Identity user.</typeparam>
		public static IApplicationBuilder UseBasicAuthentication<TUser>(this IApplicationBuilder app)
			where TUser : class
		{
			var options = new BasicAuthenticationOptions();
			return UseBasicAuthentication<TUser>(app, options);
		}
	}
}