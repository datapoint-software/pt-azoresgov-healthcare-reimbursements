using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware
{
    internal static class Assert
    {
        internal static void EntityNature(IReadOnlyCollection<EntityNature> entityNatureExpectation, EntityNature entityNature)
        {
            if (entityNatureExpectation.Contains(entityNature))
                return;
            
            throw new BusinessException("Entity nature mismatch.")
                .WithErrorMessage("O tipo de entidade não é compatível com esta operação.");
        }
        
        internal static void EntityNature(EntityNature entityNatureExpectation, EntityNature entityNature)
        {
            if (entityNature == entityNatureExpectation)
                return;

            throw new BusinessException("Entity nature mismatch.")
                .WithErrorMessage("O tipo de entidade não é compatível com esta operação.");
        }

        internal static void Found<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (entity is null)
            {
                throw new BusinessException("Entity is was not found.")
                    .WithErrorMessage("Os registos associados a esta operação não foram encontrados.");
            }
        }

        internal static void NotEmpty<TSubject>(TSubject? subject) where TSubject : struct
        {
            if (subject.HasValue && !subject.Equals(default))
                return;

            throw new BusinessException("Subject is empty or default.")
                .WithErrorMessage("Os identificadores associados a esta operação não foram incluídos no pedido.");
        }

        internal static async Task PatientEntityAccessAsync(
            IPatientEntityRepository patientEntities,
            long patientId,
            long entityId,
            CancellationToken ct)
        {
            if (await patientEntities.AnyByPatientIdAndEntityIdAsync(patientId, entityId, ct))
                return;

            throw new BusinessException("Patient entity mismatch.")
                .WithErrorMessage("O utente não está associado a esta entidade.");
        }
        
        internal static void ProcessStatus(ProcessStatus processStatusExpectation, ProcessStatus processStatus)
        {
            if (processStatus == processStatusExpectation)
                return;

            throw new BusinessException("Process status mismatch.")
                .WithErrorMessage("O estado do processo não é compatível com esta operação.");
        }

        internal static void RowVersion(Guid rowVersionIdExpectation, Guid? rowVersionId)
        {
            if (!rowVersionId.HasValue)
            {
                throw new BusinessException("Row version identifier is not set.")
                    .WithErrorMessage("O identificador de versão do registo associado a esta operação não está definido.");
            }
            
            if (!rowVersionIdExpectation.Equals(rowVersionId.Value))
            {
                throw new ConcurrencyException("Row version identifier mismatch.")
                    .WithErrorMessage("Os registos associados a esta operação foram modificados por outro utilizador.");
            }
        }

        internal static async Task UserEntityAccessAsync(
            IUserEntityRepository userEntities,
            long userId,
            long entityId,
            CancellationToken ct)
        {
            if (await userEntities.AnyByUserIdAndEntityIdAsync(userId, entityId, ct))
                return;

            throw new BusinessException("User entity mismatch.")
                .WithErrorMessage("O perfil do utilizador não está associado a esta entidade.");
        }
    }
}