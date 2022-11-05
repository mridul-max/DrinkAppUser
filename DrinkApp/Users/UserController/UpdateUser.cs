/*using FluidIntake.Users.Model.DTO;
using Microsoft.AspNetCore.Mvc;
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
using Users.Model;
using Users.Model.DTO;
using Users.Services;

namespace Users.UserController
{
    public class UpdateUser
    {
        private ILogger Logger { get; }

        public UpdateUser(ILogger<UpdateUser> log)
        {
            Logger = log;
        }

        [Function("UpdateUser")]
        [OpenApiOperation(operationId: "UpdateUser", tags: new[] { "Users" }, Summary = "Update a user with user id")]
        [UserAuth]
        [OpenApiRequestBody("application/json", typeof(UpdateUserDTO), Description = "Edits an existing User.", Example = typeof(RegisterCareGiverDTOExampleGenerator))]
        [OpenApiParameter("userId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The Guid of the user to update")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Patient), Description = "The Created response with the updated user.")]
        public async Task<IActionResult> Update([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/{userId}")] HttpRequestData req, string userId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                UpdateUserDTO data = JsonConvert.DeserializeObject<UpdateUserDTO>(requestBody);
                
                return new CreatedAtActionResult("Update user", "UpdateUser.cs", "none", null);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

    }
}
*/