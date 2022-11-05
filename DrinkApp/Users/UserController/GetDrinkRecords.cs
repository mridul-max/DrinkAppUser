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
using Users.Model;
using Users.Model.DTO.RespononseDTO;
using Users.Services;

namespace Users.UserController
{
    public class GetDrinkRecords
    {
        private ILogger Logger { get; }
        private IUserService _userService { get; }

        public GetDrinkRecords(ILogger<GetDrinkRecords> log, IUserService userService)
        {
            Logger = log;
            _userService = userService;
        }

        [Function("GetDrinkRecords")]
        [OpenApiOperation(operationId: "GetAll", tags: new[] { "Users" }, Summary = "gets all the Drink Records for the user for 1 day")]
        [OpenApiParameter("Guid", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "Id of the patient you want records of")]
        [OpenApiParameter("Date", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Date/Month/Year")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<DrinkRecordResponseDTO>), Description = "The OK response with the list of Drink Records for the Day")]
        public async Task<IActionResult> RunGetDrinkRecords(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "patient/getdrinks")] HttpRequestData req,Guid Guid,string Date)
        {
            try
            {
                //DateOnly Date;
                
                var response = await _userService.GetAllDrinkRecordsforoneday(Guid,Date);
                return new CreatedAtActionResult("Get Drink Records", "GetDrinkRecords.cs", "none", response);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
