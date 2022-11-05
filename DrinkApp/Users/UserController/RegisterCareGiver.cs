using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Users.Model;
using Users.Model.CustomException;
using Users.Model.DTO;
using Users.Model.DTO.RespononseDTO;
using Users.Security;
using Users.Services;
using Users.Validation;

namespace Users.UserController
{
    public class RegisterCareGiver
    {
        private readonly ILogger<RegisterCareGiver> _logger;
        private readonly IUserService _userService;
        public RegisterCareGiver(ILogger<RegisterCareGiver> log, IUserService userService)
        {
            _logger = log;
            _userService = userService; 
        }

        [Function("RegisterCareGiver")]
        //[UsersAuth]
        [OpenApiOperation(operationId: "RegisterCareGiver", tags: new[] { "Users" }, Summary = "Register a new user as a CareGiver")]
        [OpenApiRequestBody("application/json", typeof(RegisterCareGiverDTO), Description = "Registers a new User as a CareGiver.", Example = typeof(RegisterCareGiverDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(CareGiverResponseDTO), Description = "The OK response with the new user.", Example = typeof(RegisterCareGiverDTOExampleGenerator))]
        public async Task<IActionResult> Register(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "register/caregiver")] HttpRequestData req, FunctionContext Context)
        {
            _logger.LogInformation("Creating new CareGiver.");
            //ClaimsPrincipal claimsPrincipal = Context.GetUser();
            //if (claimsPrincipal == null)
            //{
            //    return new UnauthorizedResult();
            //}
            //if (!claimsPrincipal.IsInRole(Role.CARE_GIVER.ToString()) || !claimsPrincipal.IsInRole(Role.ADMIN.ToString()))
            //{
            //    return new ForbidResult(HttpStatusCode.Forbidden.ToString());
            //}
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                RegisterCareGiverDTOValidator validator = new RegisterCareGiverDTOValidator();
                var data = JsonConvert.DeserializeObject<RegisterCareGiverDTO>(requestBody);
                var validationResult = validator.Validate(data);
                if (!validationResult.IsValid)
                {
                    return new BadRequestObjectResult(validationResult.Errors.Select(e => new {
                        Field = e.PropertyName,
                        Error = e.ErrorMessage
                    }));
                }
                var response = await _userService.RegisteredCareGiverAsync(data);
                return new CreatedAtActionResult("Add CareGiver", "RegisterCareGiver.cs", "register/careGiver", response);
            }
            catch (RegisterUserExistingException ex)
            {
                return new UserCreationConflictException(ex.Message);
            }
        }
    }
}
