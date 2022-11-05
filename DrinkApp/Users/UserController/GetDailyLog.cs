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

namespace Users.UserController
{
    public class GetDailyLog
    {
        private ILogger Logger { get; }

        public GetDailyLog(ILogger<GetDailyLog> log)
        {
            Logger = log;
        }

        [Function("GetDailyLog")]
        [OpenApiOperation(operationId: "GetDailyLog", tags: new[] { "LoggingAmount" }, Summary = "Gell all the logs of a specific day")]
        [UserAuth]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<LogAmountDTO>), Description = "The OK response with the list of logs")]
        public async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "patients/logs/dailylog")] HttpRequestData req)
        {

            return new OkObjectResult(HttpStatusCode.NoContent);
        }
    }
}
*/