using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Users.Model.DTO;
using Users.Services;

namespace Users.UserController
{
    public class AddDrinkRecord
    {
        private readonly ILogger<AddDrinkRecord> _logger;
        private readonly IUserService _userService;
        public AddDrinkRecord(ILogger<AddDrinkRecord> log, IUserService userService)
        {
            _logger = log;
            _userService = userService;
        }

        [Function("AddDrinkRecord")]
        [OpenApiOperation(operationId: "AddRecord", tags: new[] { "Users" }, Summary = "Add Drink Record for Patient")]
        [OpenApiRequestBody("application/json", typeof(AddDrinkRecordDTO), Description = "Registers a new User as a patient.", Example = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(AddDrinkRecordDTO), Description = "The OK response with the new user.")]
        public async Task<IActionResult> Register(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "patient/addrecord")] HttpRequestData req)
        {
            _logger.LogInformation("Adding new Drink Record.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                var data = JsonConvert.DeserializeObject<AddDrinkRecordDTO>(requestBody);
                var response = await _userService.AddDrinkRecordAsync(data);
                return new CreatedAtActionResult("Add Drink Record", "AddDrinkRecord.cs", "none", response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
