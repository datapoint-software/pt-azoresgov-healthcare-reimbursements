namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.Logical
{
    public sealed class UserPermissionGrantLogicalEntity
    {
        public long UserId { get; init; }

        public long PermissionId { get; init; }

        public bool Granted { get; init; }
    }
}
