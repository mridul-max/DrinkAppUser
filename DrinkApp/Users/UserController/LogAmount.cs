/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Users.Model.DTO;
using Users.Services;

namespace Users.UserController
{
    public class LogAmount
    {
        private readonly ILogger logger;
        public LogAmount(ILogger<LogAmount> log)
        {
            logger = log;

        }
        [Function("LogAmount")]
        [OpenApiOperation(operationId: "LogAmount", tags: new[] { "LoggingAmount" }, Summary = "Log a drink ")]
        [OpenApiRequestBody("application/json", typeof(LogAmountDTO), Description = "Registers a new User.", Example = typeof(LogAmountDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(LogAmountDTO), Description = "The OK response with the new user.", Example = typeof(LogAmountDTOExampleGenerator))]
        public async Task<IActionResult> Register(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "patients/log")] HttpRequestData req)
        {
            logger.LogInformation("add new log");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                LogAmountDTO data = JsonConvert.DeserializeObject<LogAmountDTO>(requestBody);
                LogAmountDTO logDTO = null;
                return new CreatedAtActionResult("Add log", "LogAmount.cs", "none", logDTO);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
*/