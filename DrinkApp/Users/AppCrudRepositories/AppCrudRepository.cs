using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.DataAccess;
using Users.Model;

namespace Users.AppCrudRepositories
{
    public class AppCrudRepository : IAppCrudRepository
    {
        public IUserDbContext _userDbContext;
        public AppCrudRepository(IUserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public async Task<int> CreatePatient(Patient patient)
        {
            _userDbContext.Patients.Add(patient);
            var result = await _userDbContext.SaveChangesAsync();
            return result;
        }
        public async Task<CareGiver> GetCareGiverById(Guid Id)
        {
            var careGiver = await _userDbContext.CareGivers.Where(c => c.Id.Equals(Id)).FirstOrDefaultAsync();
            return careGiver;
        }
        public async Task<Patient> GetPatientById(Guid Id)
        {
            var patient = await _userDbContext.Patients.Where(c => c.Id.Equals(Id)).FirstOrDefaultAsync();
            return patient;
        }
        public async Task<CareGiver> GetCareGiverByPhoneNumber(string phoneNumber)
        {
            var careGiver = await _userDbContext.CareGivers.Where(c => c.PhoneNumber.Equals(phoneNumber)).FirstOrDefaultAsync();
            return careGiver;
        }
        public async Task<Patient> GetPatientByPhoneNumber(string phoneNumber)
        {
            var patient = await _userDbContext.Patients.Where(p => p.PhoneNumber.Equals(phoneNumber)).FirstOrDefaultAsync();
            return patient;
        }
        public async Task<int> CreateCareGiver(CareGiver careGiver)
        {
            _userDbContext.CareGivers.Add(careGiver);
            var result = await _userDbContext.SaveChangesAsync();
            return result;
        }
        public async Task<Patient> GetPatientByEmail(string email)
        {
            var patient = await _userDbContext.Patients.Where(p => p.Email.Equals(email)).FirstOrDefaultAsync();
            return patient;
        }
        public async Task<CareGiver> GetCareGiverByEmail(string email)
        {
            var careGiver = await _userDbContext.CareGivers.Where(p => p.Email.Equals(email)).FirstOrDefaultAsync();
            return careGiver;
        }
        public Task SaveUpdatedCareGiver(CareGiver careGiver)
        {
            _userDbContext.MarkAsModifiedCareGiver(careGiver);
            _userDbContext.SaveChangesAsync();
            return Task.CompletedTask;
        }
        public async Task<Patient> UpdatePatient(Patient patient)
        {
            _userDbContext.MarkAsModifiedPatient(patient);
            await _userDbContext.SaveChangesAsync();
            var updatedpatient = await _userDbContext.Patients.Where(c => c.Id.Equals(patient.Id)).FirstOrDefaultAsync();
            return updatedpatient;
        }
        public async Task<CareGiver> UpdateCareGiver(CareGiver caregiver)
        {
            _userDbContext.MarkAsModifiedCareGiver(caregiver);
            await _userDbContext.SaveChangesAsync();
            var updatedcaregiver = await _userDbContext.CareGivers.Where(c => c.Id.Equals(caregiver.Id)).FirstOrDefaultAsync();
            return updatedcaregiver;
        }

        public Task SaveUpdatedPatient(Patient Patient)
        {
            _userDbContext.MarkAsModifiedPatient(Patient);
            _userDbContext.SaveChangesAsync();
            return Task.CompletedTask;
        }
        public async Task<List<DrinkRecord>> GetListofDrinkRecordsforonePerson(Guid guid)
        {
            var records = await _userDbContext.DrinkRecords.Where(x => x.PatientId1.Equals(guid)).ToListAsync();
            return records;
        }
        public async Task<DrinkRecord> GetDrinkRecordbyId(Guid guid)
        {
            var record = await _userDbContext.DrinkRecords.Where(d => d.PatientId.Equals(guid)).FirstOrDefaultAsync();
            return record;
        }
        public async Task<DrinkRecord> UpdateDrinkRecord(DrinkRecord drinkRecord)
        {
            _userDbContext.MarkAsModifiedDrinkRecord(drinkRecord);
            await _userDbContext.SaveChangesAsync();
            var updatedRecord = await _userDbContext.DrinkRecords.Where(p => p.PatientId.Equals(drinkRecord.PatientId)).FirstOrDefaultAsync();
            return updatedRecord;
        }
        public async Task<DrinkRecord> CreateRecord(DrinkRecord drinkRecord)
        {
            _userDbContext.DrinkRecords.Add(drinkRecord);
            await _userDbContext.SaveChangesAsync();
            var newRecord = await _userDbContext.DrinkRecords.Where(r => r.PatientId.Equals(drinkRecord.PatientId)).FirstOrDefaultAsync();
            return newRecord;
        }
    }
}
