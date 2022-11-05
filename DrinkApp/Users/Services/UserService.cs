using AutoMapper;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Users.AppCrudRepositories;
using Users.Model;
using Users.Model.CustomException;
using Users.Model.DTO;
using Users.Model.DTO.RespononseDTO;
using Users.Security;

namespace Users.Services
{
    public class UserService : IUserService
    {
        IPasswordHashService _passwordHashService { get; }
        IEmailService _emailService { get; }
        private ILogger _logger { get; }
        IAppCrudRepository _appCrudRepository;
        public UserService(ILogger<UserService> logger, IPasswordHashService PasswordHashService,
            IEmailService EmailService, IAppCrudRepository appCrudRepository)
        {
            _logger = logger;
            _passwordHashService = PasswordHashService;
            _emailService = EmailService;
            _appCrudRepository = appCrudRepository;
        }
        // care givers
        public async Task AssignPatient(Guid cgId, Guid patientId)
        {
          /*  var patient = await dbContext.Patients.Where(p => p.Id == patientId).FirstOrDefaultAsync();
            var cg = await dbContext.CareGivers.Where(c => c.Id == cgId).FirstOrDefaultAsync();
            if(cg == null && patient == null)
            {
                return;
            }
            cg.Patients.Add(patient);
            dbContext.SaveChangesAsync();
            //var dto = MapCareGiverToDTO(cg);
            //return dto;*/
        }
        public async Task<CareGiver> GetOneCareGiver(Guid id)
        {
/*            var cg = await dbContext.CareGivers.Where(c => c.Id == id).FirstOrDefaultAsync();*/
            return null;

        }
        public async Task<List<CareGiverDTO>> GetAllCareGivers()
        {
  /*          List<CareGiver> careGivers = null;
            List<CareGiverDTO> dtos = null;
            if (dbContext.CareGivers == null)
            {
                return dtos;
            }
            careGivers = await dbContext.CareGivers.ToListAsync();
            dtos = new();
            foreach (var cg in careGivers)
            {
                var dto = MapCareGiverToDTO(cg);
                dtos.Add(dto);
            }*/
            return null;

        }
        private CareGiverDTO MapCareGiverToDTO(CareGiver source)
        {
            // map the caregiver itself
            var map = new MapperConfiguration(mp =>
            {
                mp.CreateMap<CareGiver, CareGiverDTO>().ForMember(c => c.Patients, option => option.Ignore());
            });
            IMapper iMap = map.CreateMapper();

            var caregiver = iMap.Map<CareGiver, CareGiverDTO>(source);


            // map its patients
            var ptmap = new MapperConfiguration(mp =>
            {
                mp.CreateMap<Patient, CareGiverPatient>();
            });
            IMapper ptMapper = ptmap.CreateMapper();
            caregiver.Patients = new List<CareGiverPatient>();
            foreach (var p in source.Patients)
            {
                var cgdto = ptMapper.Map<Patient, CareGiverPatient>(p);
                caregiver.Patients.Add(cgdto);
            }
            
            return caregiver;
        }
        //Patient working functions
        public async Task<List<PatientDTO>> GetAllPatients()
        {
/*            List<Patient> patients = null;
            List<PatientDTO> patientsdto = null;
            if (dbContext.Patients == null)
            {
                return patientsdto;
            }
            patients = await dbContext.Patients.ToListAsync();
            patientsdto = new List<PatientDTO>();
            foreach (var patient in patients)
            {
                var dto = MapPatientToDto(patient);
                patientsdto.Add(dto);
            }*/
            return null;
        }

        private PatientDTO MapPatientToDto(Patient source)
        {
            var map = new MapperConfiguration(mp =>
            {
                mp.CreateMap<Patient, PatientDTO>();
            });
            IMapper iMap = map.CreateMapper();

            //return iMap.Map<Patient, PatientDTO>(source);
            var patientdto = iMap.Map<Patient, PatientDTO>(source);
            var cgMap = new MapperConfiguration(mp =>
            {
                mp.CreateMap<CareGiver, PatientCareGiverDTO>();
            });
            IMapper cgMapper = cgMap.CreateMapper();
            var cgdto = cgMapper.Map<CareGiver, PatientCareGiverDTO>(source.CareGiver);

            patientdto.Nurse = cgdto;

            return patientdto;
        }
        public async Task<PatientResponseDTO> RegisteredPatientAsync(RegisterPatientDTO registeredUser)
        {
            if (registeredUser.CareGiverId != Guid.Empty)
            {
                registeredUser.CareGiver = await _appCrudRepository.GetCareGiverById(registeredUser.CareGiverId);
            }
            var patient = MapDTOToPatient(registeredUser);
            patient.Id = Guid.NewGuid();
            patient.Password = _passwordHashService.HashPassword(registeredUser.Password);
            var patientResult = await _appCrudRepository.GetPatientByPhoneNumber(registeredUser.PhoneNumber);
            var careGiverResult = await _appCrudRepository.GetCareGiverByPhoneNumber(registeredUser.PhoneNumber);
            if (patientResult != null || careGiverResult != null)
            {
                throw new RegisterUserExistingException($"This User already exist with phoneNumber {registeredUser.PhoneNumber}");
            }
            await _appCrudRepository.CreatePatient(patient);
            var response = MapDTOToPatientResponse(patient);
            return response;
        }
        public Patient MapDTOToPatient(RegisterPatientDTO registerUserDTO)
        {
            var map = new MapperConfiguration(mp =>
            {
                mp.CreateMap<RegisterPatientDTO, Patient>();
            });
            IMapper iMap = map.CreateMapper();
            return iMap.Map<RegisterPatientDTO, Patient>(registerUserDTO);
        }
        public PatientResponseDTO MapDTOToPatientResponse(Patient patient)
        {
            var map = new MapperConfiguration(mp =>
            {
                mp.CreateMap<Patient, PatientResponseDTO>();
            });
            IMapper iMap = map.CreateMapper();
            return iMap.Map<Patient, PatientResponseDTO>(patient);
        }
        public async Task<Patient> EditPatientLimit(string phoneNo, int newlimit)
        {
            var patient = await _appCrudRepository.GetPatientByPhoneNumber(phoneNo);
            if (patient != null)
            {
                patient.DailyLimit = newlimit;
                var updatedPatient = await _appCrudRepository.UpdatePatient(patient);
                return updatedPatient;
            }
            throw new NotFoundException("The user phone number could not be found"); 
        }
        public async Task<bool> DeactivateUser(Guid id, bool status)
        {
            var patient = await _appCrudRepository.GetPatientById(id);
            var caregiver = await _appCrudRepository.GetCareGiverById(id);

            if (patient != null)
            {
                patient.Active = status;
                var updatedPatient = await _appCrudRepository.UpdatePatient(patient);
                return patient.Active;
            }
            else if (caregiver != null)
            {
                caregiver.Active = status;
                var updatedCareGiver = await _appCrudRepository.UpdateCareGiver(caregiver);
                return caregiver.Active;
            }
            throw new NotFoundException("The user could not be found");

        }
        //CareGiver functions
        public async Task<CareGiverResponseDTO> RegisteredCareGiverAsync(RegisterCareGiverDTO dto)
        {
            var careGiver = MapCareGiverDtoToCareGiver(dto);
            careGiver.Id = Guid.NewGuid();
            careGiver.Password = _passwordHashService.HashPassword(careGiver.Password);
           
            var patientResult = await _appCrudRepository.GetPatientByPhoneNumber(dto.PhoneNumber);
            var careGiverResult = await _appCrudRepository.GetCareGiverByPhoneNumber(dto.PhoneNumber);
            if (patientResult != null || careGiverResult != null)
            {
                throw new RegisterUserExistingException($"This User already exist with phoneNumber {dto.PhoneNumber}");
            }
            var saveResult = await _appCrudRepository.CreateCareGiver(careGiver);
            var response = MapDTOToCareGiverResponse(careGiver);
            return response;
        }

        public CareGiver MapCareGiverDtoToCareGiver(RegisterCareGiverDTO dto)
        {
            var map = new MapperConfiguration(mp =>
            {
                mp.CreateMap<RegisterCareGiverDTO, CareGiver>();
            });
            IMapper iMap = map.CreateMapper();
            return iMap.Map<RegisterCareGiverDTO, CareGiver>(dto);
        }
        private CareGiverResponseDTO MapDTOToCareGiverResponse(CareGiver careGiver)
        {
            var map = new MapperConfiguration(mp =>
            {
                mp.CreateMap<CareGiver, CareGiverResponseDTO>();
            });
            IMapper iMap = map.CreateMapper();
            return iMap.Map<CareGiver, CareGiverResponseDTO>(careGiver);
        }
        public async Task<List<UserRole>> GetLoginRole(LoginRequest loginRequest)
        {
            List<UserRole> Roles = new List<UserRole>();
            var patient = await _appCrudRepository.GetPatientByPhoneNumber(loginRequest.PhoneNumber);
            var careGiver = await _appCrudRepository.GetCareGiverByPhoneNumber(loginRequest.PhoneNumber);
            if (patient == null && careGiver == null)
            {
                throw new NotFoundException("Your phone number or password is incorrecct");
            }
            if (patient != null && _passwordHashService.ValidatePassword(patient.Password))
            {
                Roles.Add(patient.UserRole);
            }
            else if (careGiver != null && _passwordHashService.ValidatePassword(careGiver.Password))
            {
                foreach (UserRole u in careGiver.UserRoles)
                {
                    Roles.Add(u);
                }
                return Roles;
            }
            return Roles;
        }
        public async Task SendEmailToResetPassword(SendEmailDTO sendEmailDTO)
        {
            var patient = await _appCrudRepository.GetPatientByEmail(sendEmailDTO.Email);
            var careGiver = await _appCrudRepository.GetCareGiverByEmail(sendEmailDTO.Email);
            if (patient == null && careGiver == null)
            {
                throw new NotFoundException("Your email address is not found");
            }
            if (patient != null)
            {
                await SendEmailToPatientAsync(patient, sendEmailDTO);
            }
            else if (careGiver != null)
            {
                await SendEmailToCareGiverAsync(careGiver, sendEmailDTO);
            }
        }
        
        //using send grid to send email
        string Message = "You can use this code to reset your password. ";
        public async Task SendEmailToCareGiverAsync(CareGiver careGiver, SendEmailDTO sendEmailDTO)
        {
            string generateTokenCode = _emailService.GenerateTokenCode();
            string body = $"{Message}  {generateTokenCode}";
            careGiver.TokenCodeGeneratedTime = DateTime.Now;   
            careGiver.GenerateTokenCode = generateTokenCode;
            await _emailService.SendEmail(SendGridBuildMessage(body, sendEmailDTO.Email));
            await _appCrudRepository.SaveUpdatedCareGiver(careGiver);
        }
        public async Task SendEmailToPatientAsync(Patient patient, SendEmailDTO sendEmailDTO)
        {
            string generateTokenCode = _emailService.GenerateTokenCode();
            string body = $"{Message}  {generateTokenCode}";
            patient.TokenCodeGeneratedTime = DateTime.Now;
            patient.GenerateTokenCode = generateTokenCode;
            await _emailService.SendEmail(SendGridBuildMessage(body, sendEmailDTO.Email));
            await _appCrudRepository.SaveUpdatedPatient(patient);
        }
        public async Task PasswordResetByTokenCode(ForgetPasswordDTO ForgetPasswordDTO)
        {
            var patient = await _appCrudRepository.GetPatientByPhoneNumber(ForgetPasswordDTO.PhoneNumber);
            var careGiver = await _appCrudRepository.GetCareGiverByPhoneNumber(ForgetPasswordDTO.PhoneNumber);
            if (patient == null && careGiver == null)
            {
                throw new NotFoundException("Your phone number is not found");
            }
            if (patient != null)
            {
                TimeSpan span = DateTime.Now.Subtract(patient.TokenCodeGeneratedTime);
                if (patient.GenerateTokenCode.Contains(ForgetPasswordDTO.GenerateTokenCode) && span.Hours <= 1)
                {
                    patient.Password = _passwordHashService.HashPassword(ForgetPasswordDTO.NewPassword);
                    patient.GenerateTokenCode = null;
                    await _appCrudRepository.SaveUpdatedPatient(patient);
                }
            }
            else if (careGiver != null)
            {
                TimeSpan span = DateTime.Now.Subtract(careGiver.TokenCodeGeneratedTime);
                if (careGiver.GenerateTokenCode.Contains(ForgetPasswordDTO.GenerateTokenCode) && span.Hours <= 1)
                {
                    careGiver.Password = _passwordHashService.HashPassword(ForgetPasswordDTO.NewPassword);
                    careGiver.GenerateTokenCode = null;
                    await _appCrudRepository.SaveUpdatedCareGiver(careGiver);
                }
            }
        }
        //Drink Record Or Drink Logs methods 
        public async Task<DrinkRecordResponseDTO> AddDrinkRecordAsync(AddDrinkRecordDTO dto)
        {
            dto.DateTime = DateTime.Now;
            dto.patient = await _appCrudRepository.GetPatientByPhoneNumber(dto.Patientno);
            var drinkRecord = MapAddDrinkRecordDtoToDrinkRecord(dto);
            //DrinkRecords drinkRecord = new DrinkRecords();
            drinkRecord.PatientId = Guid.NewGuid();
            //drinkRecord.DateTime = DateTime.Now;
            //drinkRecord.Mililiters = dto.Mililiters;
            var newRecord = await _appCrudRepository.CreateRecord(drinkRecord);
            var response = MapDTOToDrinkRecordResponse(newRecord);
            if (newRecord != null)
            {
                return response;
            }
            return null;
        }
        public DrinkRecord MapAddDrinkRecordDtoToDrinkRecord(AddDrinkRecordDTO dto)
        {
            var map = new MapperConfiguration(mp =>
            {
                mp.CreateMap<AddDrinkRecordDTO, DrinkRecord>();
            });
            IMapper iMap = map.CreateMapper();
            return iMap.Map<AddDrinkRecordDTO, DrinkRecord>(dto);
        }
        private DrinkRecordResponseDTO MapDTOToDrinkRecordResponse(DrinkRecord drinkRecord)
        {
            var map = new MapperConfiguration(mp =>
            {
                mp.CreateMap<DrinkRecord, DrinkRecordResponseDTO>();
            });
            IMapper iMap = map.CreateMapper();
            return iMap.Map<DrinkRecord, DrinkRecordResponseDTO>(drinkRecord);
        }
        public async Task<DrinkRecordResponseDTO> EditDrinkRecord(int mlUpdated, Guid guid)
        {
            var drinkRecord = await _appCrudRepository.GetDrinkRecordbyId(guid);
            drinkRecord.Mililiters = mlUpdated;
            var updatedRecord = await _appCrudRepository.UpdateDrinkRecord(drinkRecord);
            var patient = await _appCrudRepository.GetPatientById(updatedRecord.PatientId1);
            updatedRecord.Patient = patient;
            if (updatedRecord != null)
            {
                DrinkRecordResponseDTO responseDTO = MapDTOToDrinkRecordResponse(updatedRecord);
                return responseDTO;
            }
            return null;
        }

        public async Task<List<DrinkRecordResponseDTO>> GetAllDrinkRecordsforoneday(Guid patientguid,string userdate)
        {
            //List<DrinkRecord> drinkRecords = null;
            //List<DrinkRecord> returnRecords = null;
            DateTime date = DateTime.Now;
            

            List<DrinkRecordResponseDTO> drinkRecordResponseDTOs = new List<DrinkRecordResponseDTO>();
            var drinkRecords = await _appCrudRepository.GetListofDrinkRecordsforonePerson(patientguid);
            if (userdate != null)
            {
                date = DateTime.ParseExact(userdate, "dd/MM/yyyy", null);
                return GetDateofSpecificDay(drinkRecords, drinkRecordResponseDTOs, date);
            }
            if (drinkRecords != null)
            {
                foreach (var d in drinkRecords.ToList())
                {
                    // d.Patient = patients;
                    if (d.DateTime.Date.Equals(DateTime.Today))
                    {
                        DrinkRecordResponseDTO dTO = new DrinkRecordResponseDTO
                        {
                            Id = d.PatientId,
                            Mililiters = d.Mililiters,
                            Patient = d.Patient,
                            DateTime = d.DateTime
                        };

                        drinkRecordResponseDTOs.Add(dTO);
                    }
                }
                return drinkRecordResponseDTOs;
            }
            throw new EntitiesNotFoundException("List has null");
            
        }
        public List<DrinkRecordResponseDTO> GetDateofSpecificDay(List<DrinkRecord> drinkRecords, List<DrinkRecordResponseDTO> responseDTO, DateTime date)
        {
            foreach (var d in drinkRecords.ToList())
            {
                // d.Patient = patients;
                if (d.DateTime.Date.Equals(date.Date))
                {
                    DrinkRecordResponseDTO dTO = new DrinkRecordResponseDTO
                    {
                        Id = d.PatientId,
                        Mililiters = d.Mililiters,
                        Patient = d.Patient,
                        DateTime = d.DateTime
                    };
                    responseDTO.Add(dTO);
                }
            }
            return responseDTO;
        }
        public async Task<DailyGoalResponseDTO> DailyGoalCheck(Guid guid)
        {
            var record = await _appCrudRepository.GetListofDrinkRecordsforonePerson(guid);
            var patient = await _appCrudRepository.GetPatientById(guid);
            DailyGoalResponseDTO responseDTO = new DailyGoalResponseDTO();
            int checker = 0;
            foreach (var d in record)
            {
                d.Patient = patient;
                checker += d.Mililiters;
            }
            if(checker >= patient.DailyGoal)
            {
                responseDTO.message += "Congratulations you have reached your daily goal";
                return responseDTO;
            }
            else
            {
                int remaining = patient.DailyGoal - checker;
                responseDTO.message += "Keep at it you only have  " + remaining;
                return responseDTO;
            }
        }
        public SendGridMessage SendGridBuildMessage(string body, string sendEmailDTO)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Environment.GetEnvironmentVariable("SENDER_ADDRESS"), "FROM"),
                Subject = "Forget Passowrd Token",
                PlainTextContent = "",
                HtmlContent = $"<strong>{body} .your code will expire soon</strong>"
            };
            msg.AddTo(new EmailAddress(sendEmailDTO, null));
            return msg;
        }
    }
}
