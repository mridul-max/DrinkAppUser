using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using Users.Model;
using Users.Model.DTO;

namespace Users.Model.DTO
{
    public class RegisterCareGiverDTOExampleGenerator : OpenApiExample<RegisterCareGiverDTO>
    {
        public override IOpenApiExample<RegisterCareGiverDTO> Build(NamingStrategy NamingStrategy = null)
        {

            //            "firstName": "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz",
            //            "lastName": "Narvekar",
            //            "email": "abi@gmail.com",
            //            "phoneNumber": "+31645876734",
            //            "password": "Mona0179!",
            //            "active": false,
            //            "dailyLimit": 45,
            //            "userRole": {
            //                          "role": "PATIENT"
            //            },
            //            "dailyGoal": 49,
            //            "dateOfBirth": "1950-07-06T00:00:00",
            Patient p = new Patient()
            {
                FirstName = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz",
                LastName = "Narvekar",
                PhoneNumber = "+31645876734",
                Active = false,
                DailyLimit = 45,
                UserRole = new UserRole { Role = Role.PATIENT },
                DailyGoal = 49,
                DateOfBirth = new DateTime(1950, 12, 12)
            };
            Patient t = new Patient()
            {
                FirstName = "tttttttttttttttttttttttttttttttt",
                LastName = "Narvekar",
                PhoneNumber = "+31645876734",
                Active = false,
                DailyLimit = 45,
                UserRole = new UserRole { Role = Role.PATIENT },
                DailyGoal = 49,
                DateOfBirth = new DateTime(1950, 12, 12)
            };

            Examples.Add(OpenApiExampleResolver.Resolve("Abhishek", new RegisterCareGiverDTO()
            {
                FirstName = "Abhishek",
                LastName = "Narvekar",
                PhoneNumber = "0612345678",
                Email = "abi@gmail.com",
                Password = "abc",
                UserRoles = new List<UserRole> { new UserRole { 
                    Role = Role.ADMIN }, 
                    new UserRole { Role = Role.CARE_GIVER } },
                
            }, NamingStrategy));
            Examples.Add(OpenApiExampleResolver.Resolve("Mona", new RegisterCareGiverDTO()
            {
                FirstName = "Mona",
                Email = "mo@gmail.com",
                LastName = "Rostami",
                PhoneNumber = "0612345678",
                Password = "ijk",
                UserRoles = new List<UserRole> { new UserRole {
                    Role = Role.ADMIN }, }
            }, NamingStrategy));
            Examples.Add(OpenApiExampleResolver.Resolve("Junayeth", new RegisterCareGiverDTO()
            {
                FirstName = "Junayeth",
                Email = "ju@gmail.com",
                LastName = "Provat",
                PhoneNumber = "+31645876734",
                Password = "Mona0179!",
                UserRoles = new List<UserRole> { new UserRole {
                    Role = Role.ADMIN },
                    new UserRole { Role = Role.CARE_GIVER } },
                Patients = new List<Patient>() { p, t }
            }, NamingStrategy)) ;
            return this;
        }
    }
}
