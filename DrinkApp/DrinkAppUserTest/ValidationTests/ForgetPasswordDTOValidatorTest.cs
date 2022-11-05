using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Users.Model.DTO;
using Users.Validation;

namespace Users.ValidationTests
{
    [TestFixture]
    public class ForgetPasswordDTOValidatorTest
    {
        private ForgetPasswordDTOValidator _validator;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _validator = new ForgetPasswordDTOValidator();
        }

        //email validating
        [TestCase(null)]
        public void Should_Have_Validation_Errors_For_Invalid_GeneratedCode(string GeneratedCode)
        {
            var GenerateTokenCode = _fixture.Build<ForgetPasswordDTO>().With(x => x.GenerateTokenCode, GeneratedCode).Create();
            var result = _validator.TestValidate(GenerateTokenCode);
            result.ShouldHaveValidationErrorFor(x => x.GenerateTokenCode);
        }
        [TestCase("+31645826735","xsds43557","!Mahedi017")]
        public void Send_EMail_DTO_Validator_Shoud_Accept_valid_GeneratedCode(string PhoneNumber, string GeneratedCode, string NewPassword)
        {
            var GenerateToken = _fixture.Build<ForgetPasswordDTO>()
                .With(x => x.PhoneNumber, PhoneNumber)
                .With(x => x.GenerateTokenCode, GeneratedCode)
                .With(x => x.NewPassword, NewPassword)
                .Create();
            var result = _validator.TestValidate(GenerateToken);
            result.IsValid.Should().BeTrue();
        }
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Should_Have_Validation_Errors_For_Invalid_PhoneNumber(string PhoneNumber)
        {
            var savedPhoneNumber = _fixture.Build<ForgetPasswordDTO>().With(x => x.PhoneNumber, PhoneNumber).Create();
            var result = _validator.TestValidate(savedPhoneNumber);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("hjdshw1")]
        [TestCase("dshfdsywy37432764263277dsdshdhy")]
        public void Should_Have_Validation_Errors_For_Invalid_NewPassword(string NewPassword)
        {
            var savedPassword = _fixture.Build<ForgetPasswordDTO>().With(x => x.NewPassword, NewPassword).Create();
            var result = _validator.TestValidate(savedPassword);
            result.ShouldHaveValidationErrorFor(x => x.NewPassword);
        }
    }
}
