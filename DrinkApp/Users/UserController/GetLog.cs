/*using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Recipe.API.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Users.Model.DTO;
using Users.Services;

namespace Users.UserController
{
    public class GetLog
    {
        private ILogger Logger { get; }

        public GetLog(ILogger<GetLog> log)
        {
            Logger = log;
        }

        [Function("GetLog")]
        [OpenApiOperation(operationId: "GetLog", tags: new[] { "LoggingAmount" }, Summary = "Gell all the logs")]
        [UserAuth]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<LogAmountDTO>), Description = "The OK response with the list of logs")]
        public async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "patients/logs")] HttpRequestData req)
        {
            ;
            return new OkObjectResult(null);
        }
    }
}
*/