using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Users.Model;
using Users.Model.CustomException;
using Users.Services;

namespace Users.UserController
{
    public class EditDailyLimit
    {
        private ILogger Logger { get; }
        private IUserService _userService { get; }

        public EditDailyLimit(ILogger<EditDailyLimit> log, IUserService userService)
        {
            Logger = log;
            _userService = userService;
        }


        [Function("EditDailyLimit")]
        [OpenApiOperation("EditDailyLimit", tags: new[] { "Users" }, Summary = "Edits the daily limit of drinking for a Patient")]
        [OpenApiParameter("PhoneNo", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The Phone Number of the user that you want to change their daily drink limit")]
        [OpenApiParameter("Newlimit", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "The amount of drink to have per day.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Patient), Description = "The Created response with the edited Patient.")]
        public async Task<IActionResult> EditLimit(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "patient/editdailylimit")] HttpRequestData req, string phoneNo,int newlimit)
        {
            Logger.LogInformation("Running the Run method from edit daily limit.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            HttpResponseData response = req.CreateResponse();
            try
            {
                var patient = await _userService.EditPatientLimit(phoneNo, newlimit);
                await response.WriteAsJsonAsync(patient);
                response.StatusCode = HttpStatusCode.Created;
                return new CreatedAtActionResult("Edit Limit", "EditDailyLimit.cs", "none", patient);

            }
            catch (NotFoundException ex)
            {
                return new EntryNotFoundException(ex.Message);
            }
        }

    }
}
