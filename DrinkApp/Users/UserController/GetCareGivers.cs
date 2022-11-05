using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Users.Model;
using Users.Services;
using Users.Model.DTO;

namespace Users.UserController
{
    public class GetCareGivers
    {
        private ILogger Logger { get; }
        private IUserService userSv { get; }
        public GetCareGivers(ILogger<GetCareGivers> log, IUserService userSv)
        {
            Logger = log;
            this.userSv = userSv;
        }

        [Function("GetPatients")]
        [OpenApiOperation(operationId: "GetAllCareGivers", tags: new[] { "Users" }, Summary = "Gets all the care givers")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CareGiverDTO>), Description = "The OK response with the list of Care givers")]
        public async Task<HttpResponseData> GetAllPatients(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequestData req)
        {
            Logger.LogInformation("C# HTTP trigger GetPatients in Users from GetAllPatients.cs processed a request");
            var careGivers = await userSv.GetAllCareGivers();
            HttpResponseData response = req.CreateResponse();
            await response.WriteAsJsonAsync(careGivers);
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}
