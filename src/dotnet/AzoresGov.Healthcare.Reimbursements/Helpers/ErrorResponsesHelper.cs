using Datapoint;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Helpers
{
    internal static class ErrorResponsesHelper
    {
        internal static string CreateUserErrorMessage(Exception exception) => 
            
            exception is AuthenticationException ? "A sua sessão expirou." :
            
            exception is AuthorizationException ? "Não tem permissões suficientes para executar esta operação." :

            exception is ValidationException ? "O formulário apresenta erros de validação que carecem a sua atenção." :

                "Ocorreu um erro inesperado que impediu a execução desta operação.";
    }
}
