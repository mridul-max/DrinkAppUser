/*using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Model;

namespace Users.Model.DTO
{
    public class LogAmountDTOExampleGenerator : OpenApiExample<LogAmountDTO>
    {
        //public User user1;
        public override IOpenApiExample<LogAmountDTO> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("blablabla", new LogAmountDTO()
            {
                millilitres = 200,
                patient = new Patient
                {
                    FirstName = "ju",
                    LastName = "er",
                    PhoneNumber = "34455654",
                    Email = "df@gmail.com",
                    Password = "3434",
                    //Roles = new() { Role.PATIENT }
                },
                time = DateTime.UtcNow

            }, namingStrategy)) ;
            return this;
        }
    }
}*/