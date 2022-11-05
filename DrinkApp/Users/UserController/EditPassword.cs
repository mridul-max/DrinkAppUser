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
using Users.Model.DTO;
using Users.Services;

namespace Users.UserController
{
    public class EditPassword
    {
        private ILogger Logger { get; }
        public EditPassword(ILogger<EditDailyLimit> log)
        {
            Logger = log;
        }


        [Function("EditPassword")]
        [OpenApiOperation("EditPassword", tags: new[] { "Users" }, Summary = "Edits the password for a user")]
        [UserAuth]
        [OpenApiParameter("userId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The id of the user that you want to change their password")]
        [OpenApiRequestBody("application/json", typeof(ChangePasswordDTO), Description = "the old password or the code that is sent to your phone number and the new password.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(ChangePasswordDTO), Description = "The Created response with the edited user.")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Users/ChangePassword/{userId}")] HttpRequestData req, Guid userId)
        {
            Logger.LogInformation("Running the Run method from edit daily limit.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                ChangePasswordDTO data = JsonConvert.DeserializeObject<ChangePasswordDTO>(requestBody);
                return new CreatedAtActionResult("Set activity", "DeactivateUser.cs", "none", null);

            }
            catch (Exception e)
            {

                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
*/