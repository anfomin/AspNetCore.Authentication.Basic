using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCore.Authentication.Basic
{
	/// <summary>
	/// Provides basic authentication via HTTP "Authorization" header.
	/// </summary>
	public class BasicAuthenticationMiddleware : AuthenticationMiddleware<BasicAuthenticationOptions>
	{
		/// <summary>
		/// Creates new middleware instance.
		/// </summary>
		public BasicAuthenticationMiddleware(RequestDelegate next,
			IOptions<BasicAuthenticationOptions> options,
			ILoggerFactory loggerFactory,
			UrlEncoder urlEncoder)
			: base(next, options, loggerFactory, urlEncoder)
		{
			var opt = options.Value;
			if (opt.Events == null)
				throw new ArgumentException("Basic authentication options events does not set");
		}

		/// <inheritdoc/>
		protected override AuthenticationHandler<BasicAuthenticationOptions> CreateHandler()
		{
			return new BasicAuthenticationHandler();
		}
	}
}