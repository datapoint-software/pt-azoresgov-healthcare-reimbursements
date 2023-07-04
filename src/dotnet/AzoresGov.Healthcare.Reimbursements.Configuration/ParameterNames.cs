namespace AzoresGov.Healthcare.Reimbursements.Configuration
{
    internal static class ParameterNames
    {
        /// <summary>
        /// An Int32 for the user password hash work factor.
        /// </summary>
        public const string UserPasswordHashWorkFactor = nameof(UserPasswordHashWorkFactor);

        /// <summary>
        /// An Int32 for the user session access token expiration in seconds.
        /// </summary>
        public const string UserSessionAccessTokenExpiration = nameof(UserSessionAccessTokenExpiration);

        /// <summary>
        /// A Boolean for the user session persistency feature status.
        /// </summary>
        public const string UserSessionPersistencyEnabled = nameof(UserSessionPersistencyEnabled);

        /// <summary>
        /// An Int32 for the user session expiration in seconds.
        /// </summary>
        public const string UserSessionExpiration = nameof(UserSessionExpiration);
    }
}
