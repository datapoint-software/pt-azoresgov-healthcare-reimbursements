namespace AzoresGov.Healthcare.Reimbursements.Configuration.Constants
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
        /// An array of <see cref="EntityKind"/> for the kind of entities which
        /// processes are allowed to be created on.
        /// 
        /// By default, processes can only be created on <see cref="EntityKind.HealthCenter"/>.
        /// </summary>
        internal const string ProcessCreationEntityKinds = nameof(ProcessCreationEntityKinds);

        /// <summary>
        /// An Int32 for the user password hash work factor.
        /// </summary>
        internal const string UserPasswordHashWorkFactor = nameof(UserPasswordHashWorkFactor);

        /// <summary>
        /// An Int32 (nullable) for the user session expiration in seconds.
        /// 
        /// When set, persistent sessions become enabled and the user may choose
        /// to enable this by ticking the "Remember me" checkbox in the sign in
        /// screen.
        /// 
        /// This only applies when `AuthenticationEnabled` is set to `true`.
        /// </summary>
        internal const string UserSessionExpiration = nameof(UserSessionExpiration);
    }
}
