namespace AzoresGov.Healthcare.Reimbursements.Configuration
{
    internal static class ParameterNames
    {
        /// <summary>
        /// An Int32 for the user password hash work factor.
        /// </summary>
        internal const string UserPasswordHashWorkFactor = nameof(UserPasswordHashWorkFactor);

        /// <summary>
        /// An Int32 (nullable) for the user session expiration in seconds.
        /// 
        /// When `null`, user sessions can become persistent if the user checks
        /// the `Remember Me` checkbox on sign in.
        /// </summary>
        internal const string UserSessionExpiration = nameof(UserSessionExpiration);
    }
}
