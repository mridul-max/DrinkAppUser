using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Users.Model.CustomException;
using Users.Model.DTO;
using Users.Model.DTO.RespononseDTO;
using Users.Services;
using Users.Validation;

namespace Users.UserController
{
    public class RegisterPatient
    {
        private readonly ILogger<RegisterPatient> _logger;
        private readonly IUserService _userService;
        public RegisterPatient(ILogger<RegisterPatient> log, IUserService userService)
        {
            _logger = log;
            _userService = userService;
        }

        [Function("RegisterPatient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [OpenApiOperation(operationId: "RegisterPatient", tags: new[] { "Users" }, Summary = "Register a new user as a patient")]
        [OpenApiRequestBody("application/json", typeof(RegisterPatientDTO), Description = "Registers a new User as a patient.", Example = typeof(RegisterPatientDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(PatientResponseDTO), Description = "The OK response with the new user.")]
        public async Task<IActionResult> Register(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "register/patient")] HttpRequestData req)
        {
            _logger.LogInformation("Creating new user.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                RegisterPatientDTOValidator validator = new RegisterPatientDTOValidator();
                var data = JsonConvert.DeserializeObject<RegisterPatientDTO>(requestBody);
                var validationResult = validator.Validate(data);
                if (!validationResult.IsValid)
                {
                    return new BadRequestObjectResult(validationResult.Errors.Select(e => new {
                        Field = e.PropertyName,
                        Error = e.ErrorMessage
                    }));
                } 
                
                var response = await _userService.RegisteredPatientAsync(data);
                return new CreatedAtActionResult("Add Patient", "RegisterPatient.cs", "none", response);
            }
            catch (RegisterUserExistingException ex)
            {
                return new UserCreationConflictException(ex.Message);
            }
        }
    }
}

