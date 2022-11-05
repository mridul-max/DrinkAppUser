using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Model;

namespace Users.AppCrudRepositories
{
    public interface IAppCrudRepository
    {
        Task<int> CreatePatient(Patient patient);
        Task<int> CreateCareGiver(CareGiver careGiver);
        Task<CareGiver> GetCareGiverById(Guid Id);
        Task<CareGiver> GetCareGiverByPhoneNumber(string phoneNumber);
        Task<Patient> GetPatientByPhoneNumber(string phoneNumber);
        Task<Patient> GetPatientByEmail(string email);
        Task<CareGiver> GetCareGiverByEmail(string email);
        Task SaveUpdatedCareGiver(CareGiver careGiver);
        Task SaveUpdatedPatient(Patient Patient);
        Task<Patient> GetPatientById(Guid Id);
        Task<CareGiver> UpdateCareGiver(CareGiver caregiver);
        Task<Patient> UpdatePatient(Patient patient);
        Task<List<DrinkRecord>> GetListofDrinkRecordsforonePerson(Guid guid);
        Task<DrinkRecord> GetDrinkRecordbyId(Guid guid);
        Task<DrinkRecord> UpdateDrinkRecord(DrinkRecord drinkRecord);
        Task<DrinkRecord> CreateRecord(DrinkRecord drinkRecord);
    }
}
