using FluentAssertions;
using System;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Services;
using Xunit;

namespace TodoApiTests
{
    public class PeselValidationServiceTests
    {
        [Theory]
        [InlineData("85040814691", "1985-04-08", "Male")] 
        [InlineData("00810199183", "1800-01-01", "Female")]
        [InlineData("000101 47069", "1900-01-01", "Female")]
        [InlineData("00210134168", "2000-01-01", "Female")]
        [InlineData("0 0410107243", "2100-01-01", "Female")]
        [InlineData("00610139 004", "2200-01-01", "Female")]
        [InlineData("99923130877", "1899-12-31", "Male")]
        [InlineData("991 23181152", "1999-12-31", "Male")]
        [InlineData("99323118031", "2099-12-31", "Male")]
        [InlineData("99523106414", "2199-12-31", "Male")]
        [InlineData("99 72 31 20 779 ", "2299-12-31", "Male")]
        [InlineData("16222900123", "2016-02-29", "Female")]       
        public void ValidateValidPesel(string pesel, string expectedDateOfBirth, string expectedSex)
        {
            var expectedResponse = GetPeselResponse(pesel, DateTime.Parse(expectedDateOfBirth), expectedSex);
            expectedResponse.Errors = new PeselValidationError[] { };

            new PeselValidationService()
                .Validate(pesel)
                .Should()
                .BeEquivalentTo(expectedResponse);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("0123456789 ")]
        [InlineData("0123 456789 ")]
        [InlineData(" 1123 456789")]
        public void ValidatePeselWithInvalidLength(string pesel)
        {
            var expectedResponse = GetPeselInvalidResponse(pesel);
            expectedResponse.Errors = new PeselValidationError[] { ValidationErrorRepository.InvalidLengthError };
            
            new PeselValidationService()
                .Validate(pesel)
                .Should()
                .BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public void ValidatePeselWithInvalidCharacters()
        {
            string pesel = "1234567890a";

            var expectedResponse = GetPeselInvalidResponse(pesel);
            expectedResponse.Errors = new PeselValidationError[] { ValidationErrorRepository.NumberRequiredError };

            new PeselValidationService()
                .Validate(pesel)
                .Should()
                .BeEquivalentTo(expectedResponse);
        }

        [Theory]
        [InlineData("00610139003")]
        [InlineData("85040814692")]
        [InlineData("0041010724 2 ")]
        public void ValidatePeselWithInvalidCheckSum(string pesel)
        {
            var expectedResponse = GetPeselInvalidResponse(pesel);
            expectedResponse.Errors = new PeselValidationError[] { ValidationErrorRepository.InvalidCheckSumError };

            new PeselValidationService()
                .Validate(pesel)
                .Should()
                .BeEquivalentTo(expectedResponse, options => options
                .Including(x => x.Errors));
        }

        [Theory]
        [InlineData("15222900126")] // 29.02.2015 which is not leap year
        [InlineData("20223026128")]
        [InlineData("88813240730")]
        [InlineData("97822960777")]
        [InlineData("02233282093")]
        [InlineData("00443152410")]
        [InlineData("01653233485")]
        [InlineData("88863139628")] 
        [InlineData("95073293783")]
        [InlineData("01283212892")]
        [InlineData("01493175497")]
        [InlineData("22703278454")]
        [InlineData("64913146712")]
        [InlineData("52123298932")] 
        public void ValidatePeselWithInvalidDay(string pesel)
        {
            var expectedResponse = GetPeselInvalidResponse(pesel);
            expectedResponse.Errors = new PeselValidationError[] { ValidationErrorRepository.InvalidDayError };

            new PeselValidationService()
                .Validate(pesel)
                .Should()
                .BeEquivalentTo(expectedResponse, options => options.Excluding(x=>x.Gender));
        }

        [Theory]
        [InlineData("88941134564")]
        public void ValidatePeselWithInvalidMonth(string pesel)
        {
            var expectedResponse = GetPeselInvalidResponse(pesel);
            expectedResponse.Errors = new PeselValidationError[] { ValidationErrorRepository.InvalidMonthError };

            new PeselValidationService()
                .Validate(pesel)
                .Should()
                .BeEquivalentTo(expectedResponse, options => options
                .Including(x => x.Errors));
        }

        

        private PeselValidationResponse GetPeselInvalidResponse(string pesel)
        {
            return new PeselValidationResponse()
            {
                Pesel = pesel.Trim().Replace(" ", ""),
                IsValid = false,
                DateOfBirth = null,
                Gender = null
            };
        }

        private PeselValidationResponse GetPeselResponse(string pesel, DateTime birthDate, string gender)
        {
            return new PeselValidationResponse()
            {
                Pesel = pesel.Trim().Replace(" ", string.Empty), //test
                IsValid = true,
                DateOfBirth = birthDate,
                Gender = gender
            };
        }
    }
}
