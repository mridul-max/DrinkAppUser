/*using FluidIntake.Users.Model.DTO;
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
using Users.Model.CustomException;
using Users.Model.DTO.RespononseDTO;
using Users.Model.DTO;
using Users.Services;
using Users.Validation;

namespace Users.UserController
{
    public class PatientLikeRecipes
    {
        private readonly ILogger<PatientLikeRecipes> _logger;
        private readonly IUserService _userService;
        public PatientLikeRecipes(ILogger<PatientLikeRecipes> log, IUserService userService)
        {
            _logger = log;
            _userService = userService;
        }

        [Function("PatientLikeRecipes")]
        [OpenApiOperation(operationId: "LikeRecipe", tags: new[] { "Users" }, Summary = "Add like reciepe to patient")]
        [OpenApiRequestBody("application/json", typeof(RecipeDTO), Description = "Add like reciepe to patient.", Example = typeof(RecipeDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(PatientResponseDTO), Description = "The OK response with Liked recipe.")]
        public async Task<IActionResult> Register(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "patient/likerecipe")] HttpRequestData req)
        {
            _logger.LogInformation("Creating new user.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                var data = JsonConvert.DeserializeObject<RecipeDTO>(requestBody);
                var response = await _userService.(data);
                return new CreatedAtActionResult("Add Patient", "RegisterPatient.cs", "none", response);
            }
            catch (RegisterUserExistingException ex)
            {
                return new UserCreationConflictException(ex.Message);
            }
        }
    }
}
*/