using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Users.AppCrudRepositories;
using Users.Model;

namespace Users.AppCrudRepositoriesTests
{
    public class AppCrudRepositoryTest
    {
        private Fixture _fixture;
        private MockRepository _mockRepository;
        private Mock<IAppCrudRepository> _appCrudRepositoryMock;
        private IAppCrudRepository _appCrudRepository;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _appCrudRepositoryMock = _mockRepository.Create<IAppCrudRepository>();
            _appCrudRepository = _appCrudRepositoryMock.Object;
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository.VerifyAll();
        }
        //need to do find the answer
        [Test]
        public async Task CreatePatient_Should_Return_Patient_Data()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var patientRecord = _fixture.Build<Patient>().With(x => x.Id, patientId)
                .Create();
            _appCrudRepositoryMock.Setup(x => x.CreatePatient(patientRecord)).ReturnsAsync(2) ;

            // Act 
            var result =  _appCrudRepository.CreatePatient(patientRecord);

            // Assert
            Assert.AreEqual(result.Result, 2);
        }

        [Test]
        public async Task CreateCareGiver_Should_Return_CareGiver_Data()
        {
            // Arrange
            var careGiverId = Guid.NewGuid();
            var careGiverRecord = _fixture.Build<CareGiver>().With(x => x.Id, careGiverId)
                .Create();
            _appCrudRepositoryMock.Setup(x => x.CreateCareGiver(careGiverRecord)).ReturnsAsync(2);

            // Act 
            var result = _appCrudRepository.CreateCareGiver(careGiverRecord);

            // Assert
            Assert.AreEqual(result.Result, 2);
        }

        [Test]
        public async Task GetPatientById_Should_Retrieve_Patient_Data()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var patientRecord = _fixture.Build<Patient>().With(x => x.Id, patientId)
                .Create();
            _appCrudRepositoryMock.Setup(d => d.GetPatientById(patientId))
                          .ReturnsAsync(patientRecord);
            // Act 
            var result = _appCrudRepository.GetPatientById(patientId);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeEquivalentTo(patientRecord);
        }
        [Test]
        public async Task GetCareGiverById_Should_Retrieve_CareGiver_Data()
        {
            // Arrange
            var careGiverId = Guid.NewGuid();
            var careGiverRecord = _fixture.Build<CareGiver>().With(x => x.Id, careGiverId)
                .Create();
            _appCrudRepositoryMock.Setup(d => d.GetCareGiverById(careGiverId))
                          .ReturnsAsync(careGiverRecord);
            // Act 
            var result = _appCrudRepository.GetCareGiverById(careGiverId);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeEquivalentTo(careGiverRecord);
        }
        [Test]
        public async Task GetPatientByEmail_Should_Retrieve_Patient_Data()
        {
            // Arrange
            var patientEmail = "mahedi@gmail.com";
            var patientRecord = _fixture.Build<Patient>().With(x => x.Email, patientEmail)
                .Create();
            _appCrudRepositoryMock.Setup(d => d.GetPatientByEmail(patientEmail))
                          .ReturnsAsync(patientRecord);
            // Act 
            var result = _appCrudRepository.GetPatientByEmail(patientEmail);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeEquivalentTo(patientRecord);
        }
        [Test]
        public async Task GetCareGiverByEmail_Should_Retrieve_CareGiver_Data()
        {
            // Arrange
            var careGiverEmail = "mahedi@gmail.com";
            var careGiverRecord = _fixture.Build<CareGiver>().With(x => x.Email, careGiverEmail)
                .Create();
            _appCrudRepositoryMock.Setup(d => d.GetCareGiverByEmail(careGiverEmail))
                          .ReturnsAsync(careGiverRecord);
            // Act 
            var result = _appCrudRepository.GetCareGiverByEmail(careGiverEmail);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeEquivalentTo(careGiverRecord);
        }
        [Test]
        public async Task GetPatientByPnoneNumber_Should_Retrieve_Patient_Data()
        {
            // Arrange
            var patientPhoneNumber = "+31645826735";
            var patientRecord = _fixture.Build<Patient>().With(x => x.PhoneNumber, patientPhoneNumber)
                .Create();
            _appCrudRepositoryMock.Setup(d => d.GetPatientByEmail(patientPhoneNumber))
                          .ReturnsAsync(patientRecord);
            // Act 
            var result = _appCrudRepository.GetPatientByEmail(patientPhoneNumber);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeEquivalentTo(patientRecord);
        }
        [Test]
        public async Task GetCareGiverByPhoneNumber_Should_Retrieve_CareGiver_Data()
        {
            // Arrange
            var careGiverPhoneNumber = "+31645826735";
            var careGiverRecord = _fixture.Build<CareGiver>().With(x => x.Email, careGiverPhoneNumber)
                .Create();
            _appCrudRepositoryMock.Setup(d => d.GetCareGiverByEmail(careGiverPhoneNumber))
                          .ReturnsAsync(careGiverRecord);
            // Act 
            var result = _appCrudRepository.GetCareGiverByEmail(careGiverPhoneNumber);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeEquivalentTo(careGiverRecord);
        }
    }
}
