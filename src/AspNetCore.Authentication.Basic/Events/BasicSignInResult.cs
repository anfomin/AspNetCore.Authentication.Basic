using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;

namespace AspNetCore.Authentication.Basic
{
	/// <summary>
	/// Provides result for Basic signin operation.
	/// </summary>
	public class BasicSignInResult
	{
		/// <summary>
		/// Gets if operation is successful.
		/// </summary>
		public bool IsSuccess { get; private set; }

		/// <summary>
		/// Gets error message if <see cref="IsSuccess"/> is <c>false</c>.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Gets claims used for authentication.
		/// Assigned only when <see cref="IsSuccess"/> is <c>true</c>.
		/// </summary>
		public ClaimsPrincipal Principal { get; private set; }

		/// <summary>
		/// Contains extra authentication data.
		/// Assigned only when <see cref="IsSuccess"/> is <c>true</c>.
		/// </summary>
		public AuthenticationProperties Properties { get; private set; }

		/// <summary>
		/// Initializes new empty instance.
		/// </summary>
		protected BasicSignInResult()
		{
		}

		/// <summary>
		/// Creates success signin result.
		/// </summary>
		/// <param name="principal">Claims principal used for authentication.</param>
		/// <param name="properties">Extra authentication data.</param>
		public static BasicSignInResult Success(ClaimsPrincipal principal, AuthenticationProperties properties)
		{
			if (principal == null)
				throw new ArgumentNullException(nameof(principal));
			if (properties == null)
				throw new ArgumentNullException(nameof(properties));
			return new BasicSignInResult
			{
				IsSuccess = true,
				Principal = principal,
				Properties = properties
			};
		}

		/// <summary>
		/// Creates success signin result with no extra authentication data.
		/// </summary>
		/// <param name="principal">Claims principal used for authentication.</param>
		public static BasicSignInResult Success(ClaimsPrincipal principal)
		{
			return Success(principal, new AuthenticationProperties());
		}

		/// <summary>
		/// Creates fail signin result.
		/// </summary>
		/// <param name="error">Error message.</param>
		public static BasicSignInResult Fail(string error)
		{
			if (string.IsNullOrEmpty(error))
				throw new ArgumentNullException(nameof(error));
			return new BasicSignInResult
			{
				IsSuccess = false,
				Error = error
			};
		}
	}
}