/*using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Recipe.API.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Users.Model;
using Users.Services;

namespace Users.UserController
{
    public class GetAllUsers
    {
        private ILogger Logger { get; }

        public GetAllUsers(ILogger<GetAllUsers> log)
        {
            Logger = log;
        }

        [Function("GetAllUsers")]
        [OpenApiOperation(operationId: "GetAllusers", tags: new[] { "Users" }, Summary = "Gell all the users")]
        [UserAuth]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Patient>), Description = "The OK response with the list of Users")]
        public async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequestData req)
        {
            ;
            try
            {
                return new OkObjectResult(null);
            }

            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
*/