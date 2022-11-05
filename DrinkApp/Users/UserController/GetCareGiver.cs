using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Users.Model.DTO;
using Users.Services;

namespace Users.UserController
{
    public class GetCareGiver
    {
        //private ILogger Logger { get; }
        //private IUserService userSv { get; }
        //public AssignPatient(ILogger<AssignPatient> log, IUserService userSv)
        //{
        //    Logger = log;
        //    this.userSv = userSv;
        //}

        //[Function("AssignPatientToCareGiver")]
        //[OpenApiOperation(operationId: "AssignPatient", tags: new[] { "Users" }, Summary = "Assigns a patient to the list of a specific care giver")]
        //[OpenApiParameter("caregiverId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The Guid of the Recipes to edit")]
        //[OpenApiRequestBody("application/json", typeof(Guid), Description = "the guid of the patient.")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(List<CareGiverDTO>), Description = "The OK response with the list of Care givers")]
        //public async Task<HttpResponseData> Assign(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/patients/{caregiverId}")] HttpRequestData req, Guid caregiverId)
        //{
        //    Logger.LogInformation("C# HTTP trigger Assign in Users from AssignPatient.cs processed a request");

        //    var body = await new StreamReader(req.Body).ReadToEndAsync();
        //    var patientid = Guid.Parse(body);
        //    await userSv.AssignPatient(caregiverId, patientid);

        //    HttpResponseData response = req.CreateResponse();
        //    //await response.WriteAsJsonAsync(cg);
        //    response.StatusCode = HttpStatusCode.Created;
        //    return response;
        //}
    }
}
