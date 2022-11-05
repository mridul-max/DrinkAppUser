/*using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Users.AppCrudRepositories;
using Users.Model;
using Users.Model.DTO;
using Users.Model.DTO.RespononseDTO;
using Users.Security;
using Users.Services;

namespace Users.ServicesTests
{
    [TestFixture]
    public class UserServiceTest
    {
        private Fixture _fixture;
        private MockRepository _mockRepository;
        private Mock<IAppCrudRepository> _appCrudRepositoryMock;
        private IAppCrudRepository _appCrudRepository;
        private Mock<IEmailService> _emailServiceMock;
        private Mock<IPasswordHashService> _passWordServiceMock;
        private Mock<IUserService> _userServiceMock;
        private IUserService _userService;
        private IPasswordHashService _passWordHashService;
        private IEmailService _emailService;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockRepository = new MockRepository(MockBehavior.Loose);
            _appCrudRepositoryMock = _mockRepository.Create<IAppCrudRepository>();
            _passWordServiceMock = _mockRepository.Create<IPasswordHashService>();
            _emailServiceMock = _mockRepository.Create<IEmailService>();
            _userServiceMock = _mockRepository.Create<IUserService>();
            _userService = _userServiceMock.Object;
            _passWordHashService = _passWordServiceMock.Object;
            _emailService = _emailServiceMock.Object;
            _appCrudRepository = _appCrudRepositoryMock.Object;
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TearDown]
        public void TearDown()
        {
            _mockRepository.VerifyAll();
        }
        *//*[Test]
        public async Task RegisterPatient_Should_Return_PatientResponseDTO()
        {
            // Arrange
            var careGiverId = Guid.NewGuid();
            var careGiver = _fixture.Build<CareGiver>().With(x => x.Id, careGiverId).Create();
            var createRegisterPatientDTO = _fixture.Build<RegisterPatientDTO>()
                .With(x => x.CareGiverId, careGiverId)
                .With(x => x.CareGiver, careGiver)
                .Create();
            var Id = Guid.NewGuid();
            var patientRecord = _fixture.Create<Patient>();
            var PatientResponseDTO = new PatientResponseDTO()
            {
                Id = patientRecord.Id,
                Active = patientRecord.Active,
                DateOfBirth = patientRecord.DateOfBirth,
                DailyGoal = patientRecord.DailyGoal,
                FirstName = patientRecord.FirstName,
                LastName = patientRecord.LastName,
                Email = patientRecord.Email,
                UserRole = patientRecord.UserRole,
                PhoneNumber = patientRecord.PhoneNumber,
                DailyLimit = patientRecord.DailyLimit
            };
            var Patient = new Patient
            {
                Id = Id,
                FirstName = createRegisterPatientDTO.FirstName,
                LastName = createRegisterPatientDTO.LastName,
                Password = createRegisterPatientDTO.Password,
                PhoneNumber = createRegisterPatientDTO.PhoneNumber,
                Email = createRegisterPatientDTO.Email,
                DailyGoal = createRegisterPatientDTO.DailyGoal,
                DailyLimit = createRegisterPatientDTO.DailyLimit,
                Active = createRegisterPatientDTO.Active,
                DateOfBirth = createRegisterPatientDTO.DateOfBirth,
                CareGiver = careGiver,
                UserRole = createRegisterPatientDTO.UserRole,
                DayLogs = null,
                GenerateTokenCode = null,
                TokenCodeGeneratedTime = DateTime.Now,

            };
            _appCrudRepositoryMock.Setup(x => x.CreatePatient(Patient)).ReturnsAsync(2);

            // Act
            var result = await _userService.RegisteredPatientAsync(createRegisterPatientDTO);

            // Assert
            result.Should().NotBeNull();
            //result.Should().BeEquivalentTo(PatientResponseDTO);
        }*/
        /*[Test]
        public async Task GetLoginRole_Should_Return_ListOf_Login_Role()
        {
            // Arrange
            var loginRequest = _fixture.Build<LoginRequest>()
                .With(x => x.PhoneNumber, "+31645826735")
                .With(x => x.Password,"Mahedi0344!")
                .Create();
            var createRegisterPatientDTO = _fixture.Build<RegisterPatientDTO>()
                .With(x => x.CareGiverId, careGiverId)
                .With(x => x.CareGiver, careGiver)
                .Create();
            var Id = Guid.NewGuid();
            var patientRecord = _fixture.Create<Patient>();
            var PatientResponseDTO = new PatientResponseDTO()
            {
                Id = patientRecord.Id,
                Active = patientRecord.Active,
                DateOfBirth = patientRecord.DateOfBirth,
                DailyGoal = patientRecord.DailyGoal,
                FirstName = patientRecord.FirstName,
                LastName = patientRecord.LastName,
                Email = patientRecord.Email,
                UserRole = patientRecord.UserRole,
                PhoneNumber = patientRecord.PhoneNumber,
                DailyLimit = patientRecord.DailyLimit
            };
            _appCrudRepositoryMock.Setup(x => x.GetPatientById(Patient)).ReturnsAsync(2);

            // Act
            var result = await _userService.RegisteredPatientAsync(createRegisterPatientDTO);

            // Assert
            result.Should().NotBeNull();
            //result.Should().BeEquivalentTo(PatientResponseDTO);*//*
        }
    }
}
*/