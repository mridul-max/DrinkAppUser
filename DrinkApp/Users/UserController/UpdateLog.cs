/*using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Recipe.API.Configuration;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Users.Model.DTO;
using Users.Services;

namespace Users.UserController
{
    public class UpdateLog
    {
        private ILogger Logger { get; }

        public UpdateLog(ILogger<UpdateLog> log)
        {
            Logger = log;
        }

        [Function("UpdateLog")]
        [OpenApiOperation(operationId: "UpdateLog", tags: new[] { "LoggingAmount" }, Summary = "Update a log ml")]
        [UserAuth]
        [OpenApiRequestBody("application/json", typeof(LogAmountDTO), Description = "Edits an existing log.", Example = typeof(LogAmountDTOExampleGenerator))]
        [OpenApiParameter("logId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The Guid of the log to update")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(LogAmountDTO), Description = "The Created response with the updated user.")]
        public async Task<IActionResult> Update([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "patients/logs/{logId}")] HttpRequestData req, string logId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                LogAmountDTO data = JsonConvert.DeserializeObject<LogAmountDTO>(requestBody);
                LogAmountDTO log = null;
                return new CreatedAtActionResult("update log", "LogDrink.cs", "none", log.millilitres);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
*/