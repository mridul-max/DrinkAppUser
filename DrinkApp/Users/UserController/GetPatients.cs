using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Users.Model;
using Users.Model.DTO;
using Users.Services;

namespace Users.UserController
{
    public class GetPatients
    {
        private ILogger Logger { get; }
        private IUserService userSv { get; }
        public GetPatients(ILogger<GetPatients> log, IUserService userSv)
        {
            Logger = log;
            this.userSv = userSv;
        }

        [Function("GetAllPatients")]
        [OpenApiOperation(operationId: "GetAllPatients", tags: new[] { "Users" }, Summary = "Gets all the patients")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<PatientDTO>), Description = "The OK response with the list of Patients")]
        public async Task<HttpResponseData> GetAllPatients(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/patients")] HttpRequestData req)
        {
            Logger.LogInformation("C# HTTP trigger GetPatients in Users from GetAllPatients.cs processed a request");
            var patients = await userSv.GetAllPatients();
            HttpResponseData response = req.CreateResponse();
            await response.WriteAsJsonAsync(patients);
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}