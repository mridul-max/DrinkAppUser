using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Users.Model;
using Users.Model.DTO.RespononseDTO;
using Users.Services;

namespace Users.UserController
{
    public class EditDrinkRecord
    {
        private ILogger Logger { get; }
        private IUserService _userService { get; }

        public EditDrinkRecord(ILogger<EditDrinkRecord> log, IUserService userService)
        {
            Logger = log;
            _userService = userService;
        }


        [Function("EditDrinkRecord")]
        [OpenApiOperation("EditDrinkRecord", tags: new[] { "Users" }, Summary = "Edits the drink record for a Patient")]
        [OpenApiParameter("Millilitre", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Updated amount of Drink.")]
        [OpenApiParameter("Guid", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "The guid of the drink record.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(DrinkRecordResponseDTO), Description = "The Created response with the new drink record.")]
        public async Task<IActionResult> EditLimit(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "patient/editdrinkrecord")] HttpRequestData req, int Millilitre, Guid Guid)
        {
            //Logger.LogInformation("Running the Run method.");
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            HttpResponseData response = req.CreateResponse();
            try
            {
                var drinkRecord = await _userService.EditDrinkRecord(Millilitre, Guid);
                //await response.WriteAsJsonAsync(drinkRecord);
                return new CreatedAtActionResult("Edit Limit", "EditDrinkRecord.cs", null, drinkRecord);

            }
            catch (Exception e)
            {

                return new BadRequestObjectResult(e.Message);
            }
        }

    }
}
