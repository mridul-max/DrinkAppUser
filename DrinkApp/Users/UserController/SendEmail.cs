using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
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
using Users.Model.DTO.RespononseDTO;
using Users.Model.DTO;
using Users.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Users.Security;
using Microsoft.Azure.Cosmos;
using Users.Model;
using Users.Validation;
using Users.Model.CustomException;

namespace Users.UserController
{
    public class SendEmail
    {
        private readonly ILogger<SendEmail> _logger;
        private readonly IUserService _userService;
        
        public SendEmail(ILogger<SendEmail> log, IUserService UserService)
        {
            _logger = log;
            _userService = UserService;
        }

        [Function("SendEmail")]
        [UsersAuth]
        [OpenApiOperation(operationId: "SendEmail", tags: new[] { "Users" }, Summary = "Send forget password code to user")]
        [OpenApiRequestBody("application/json", typeof(SendEmailDTO), Description = "Send forget password code to user.", Example = typeof(SendEmailDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Accepted, contentType: "application/json", bodyType: typeof(SendEmailDTO), Description = "The OK response with ")]
        public async Task<IActionResult> Sendemail(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sendemail")] HttpRequestData req, FunctionContext Context)
        {
            _logger.LogInformation("giving email address");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                SendEmailDTOValidator validator = new SendEmailDTOValidator();
                var data = JsonConvert.DeserializeObject<SendEmailDTO>(requestBody);
                var validationResult = validator.Validate(data);
                if (!validationResult.IsValid)
                {
                    return new BadRequestObjectResult(validationResult.Errors.Select(e => new {
                        Field = e.PropertyName,
                        Error = e.ErrorMessage
                    }));
                }
                await _userService.SendEmailToResetPassword(data);
                return new SuccessMessageResponse("we have send you password reset code");
            }
            catch (NotFoundException ex)
            {
                return new EntryNotFoundException(ex.Message);
            }
        }
    }
}
