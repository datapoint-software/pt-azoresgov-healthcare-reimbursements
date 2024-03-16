using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public sealed class HealthcareReimbursementsUnitOfWork : EntityFrameworkCoreUnitOfWork, IHealthcareReimbursementsUnitOfWork
    {
        public HealthcareReimbursementsUnitOfWork(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entity> Entities { get; set; } = default!;
        
        public DbSet<EntityParentEntity> EntityParentEntities { get; set; } = default!;
        
        public DbSet<Parameter> Parameters { get; set; } = default!;

        public DbSet<PatientEntity> PatientEntities { get; set; } = default!;

        public DbSet<PatientFamilyIncomeStatement> PatientFamilyIncomeStatements { get; set; } = default!;

        public DbSet<PatientLegalRepresentative> PatientLegalRepresentatives { get; set; } = default!;

        public DbSet<Patient> Patients { get; set; } = default!;

        public DbSet<ProcessEntity> ProcessEntities { get; set; } = default!;

        public DbSet<Process> Processes { get; set; } = default!;

        public DbSet<ProcessPatientFamilyIncomeStatement> ProcessPatientFamilyIncomeStatements { get; set; } = default!;

        public DbSet<ProcessPatientLegalRepresentative> ProcessPatientLegalRepresentatives { get; set; } = default!;

        public DbSet<ProcessPatient> ProcessPatients { get; set; } = default!;

        public DbSet<ProcessPaymentConfiguration> ProcessPaymentSettings { get; set; } = default!;

        public DbSet<ProcessConfiguration> ProcessSettings { get; set; } = default!;
        
        public DbSet<Role> Roles { get; set; } = default!;

        public DbSet<Sequence> Sequences { get; set; } = default!;
        
        public DbSet<UserEntity> UserEntities { get; set; } = default!;
        
        public DbSet<UserPassword> UserPasswords { get; set; } = default!;
        
        public DbSet<UserRole> UserRoles { get; set; } = default!;
        
        public DbSet<User> Users { get; set; } = default!;
        
        public DbSet<UserSession> UserSessions { get; set; } = default!;

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder model) =>

            model.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
