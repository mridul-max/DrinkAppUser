using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Users.Model.DTO.RespononseDTO;
using Users.Services;

namespace Users.UserController
{
    public class DailyGoalCheck
    {
        private ILogger Logger { get; }
        private IUserService _userService { get; }

        public DailyGoalCheck(ILogger<DailyGoalCheck> log, IUserService userService)
        {
            Logger = log;
            _userService = userService;
        }

        [Function("DailyGoalCheck")]
        [OpenApiOperation(operationId: "GetAll", tags: new[] { "Users" }, Summary = "checks if dailygoal has been reached or not")]
        [OpenApiParameter("Guid", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "Id of the patient whose goal you want to check")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<DrinkRecordResponseDTO>), Description = "The OK response with a message of the remainder of the goal")]
        public async Task<IActionResult> RunDailyGoalCheck(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "patient/checkdailygoal")] HttpRequestData req, Guid Guid)
        {
            try
            {
                //DateOnly Date;

                var response = await _userService.DailyGoalCheck(Guid);
                return new CreatedAtActionResult("Get Drink Records", "DailyGoalCheck.cs", "none", response);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
