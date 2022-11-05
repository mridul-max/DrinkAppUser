using Microsoft.Azure.Functions.Worker;
using System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Model;
using Users.Model.DTO;
using Users.Model.DTO.RespononseDTO;
using Users.Security;

namespace Users.Services
{
    public interface IUserService
    {
        Task<PatientResponseDTO> RegisteredPatientAsync(RegisterPatientDTO registeredUser);
        Task<CareGiverResponseDTO> RegisteredCareGiverAsync(RegisterCareGiverDTO registeredUser);
        Task<List<UserRole>> GetLoginRole(LoginRequest loginRequest);
        Task SendEmailToResetPassword(SendEmailDTO sendEmailDTO);
        Task PasswordResetByTokenCode(ForgetPasswordDTO ForgetPasswordDTO);
        Task<List<PatientDTO>> GetAllPatients();
        Task<List<CareGiverDTO>> GetAllCareGivers();
        Task AssignPatient(Guid cgId, Guid patientId);
        Task<Patient> EditPatientLimit(string phoneNo, int newlimit);
        Task<bool> DeactivateUser(Guid id, bool status);
        Task<DrinkRecordResponseDTO> AddDrinkRecordAsync(AddDrinkRecordDTO dto);
        Task<DrinkRecordResponseDTO> EditDrinkRecord(int mlUpdated, Guid guid);
        Task<List<DrinkRecordResponseDTO>> GetAllDrinkRecordsforoneday(Guid patientguid, string date);
        Task<DailyGoalResponseDTO> DailyGoalCheck(Guid guid);
    }
}
