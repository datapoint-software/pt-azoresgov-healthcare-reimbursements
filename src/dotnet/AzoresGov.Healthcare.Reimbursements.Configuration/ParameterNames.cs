namespace AzoresGov.Healthcare.Reimbursements.Configuration
{
    internal static class ParameterNames
    {
        /// <summary>
        /// A Boolean for the authentication feature enabled status.
        /// 
        /// By authentication, we're referring to the typical sign in form only,
        /// since federation may still allow a user to sign in by going through
        /// a third-party authentication procedure.
        /// </summary>
        internal const string AuthenticationEnabled = nameof(AuthenticationEnabled);

        /// <summary>
        /// An Int32 for the user password hash work factor.
        /// </summary>
        internal const string UserPasswordHashWorkFactor = nameof(UserPasswordHashWorkFactor);

        /// <summary>
        /// An Int32 (nullable) for the user session expiration in seconds.
        /// 
        /// When `null`, user sessions can become persistent if the user checks
        /// the `Remember Me` checkbox on sign in.
        /// 
        /// This is only applicable when authentication is enabled.
        /// </summary>
        internal const string UserSessionExpiration = nameof(UserSessionExpiration);
    }
}
