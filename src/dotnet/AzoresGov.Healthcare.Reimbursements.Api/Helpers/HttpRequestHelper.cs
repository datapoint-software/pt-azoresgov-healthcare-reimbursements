using Datapoint;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace AzoresGov.Healthcare.Reimbursements.Api.Helpers
{
    internal static class HttpRequestHelper
    {
        private const string GenericUserMessage = "Este navegador não é compatível com este serviço aplicacional.";

        internal static IPAddress GetRemoteAddress(this HttpRequest request) =>

            request.HttpContext.Connection.RemoteIpAddress ??

                throw new ValidationException("The user agent header was not set for the incomming request.")
                    .WithCode("UAHXKZ")
                    .WithUserMessage(GenericUserMessage);

        internal static string GetUserAgent(this HttpRequest request)
        {
            var userAgent = request.Headers.UserAgent.ToString();

            if (string.IsNullOrEmpty(userAgent))
            {
                throw new ValidationException("The user agent header was not set for the incomming request.")
                    .WithCode("UAHXKZ")
                    .WithUserMessage(GenericUserMessage);
            }

            return userAgent;
        }
    }
}
