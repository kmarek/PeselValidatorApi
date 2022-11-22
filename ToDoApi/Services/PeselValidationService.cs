using System;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Exceptions;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PeselValidationService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private Pesel _pesel;
        private List<PeselValidationError> _errors;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public PeselValidationService()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _errors = new List<PeselValidationError>();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public PeselValidationResponse Validate(string pesel)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            pesel = Normalize(pesel);

            if (!Parse(pesel))
                return GenerateResponse(pesel);

            ValidateCheckSum();
            ExtractGender();
            ExtractDateOfBirth();                      

            return GenerateResponse();
        }

        private void ValidateCheckSum()
        {
            var peselExtractorService = new PeselExtractorService(_pesel);

            if(CalculateCheckSum() != peselExtractorService.GetChecksum())
                _errors.Add(ValidationErrorRepository.InvalidCheckSumError);
        }

        private int CalculateCheckSum()
        {
            int checkSum = 9 * _pesel.GetDigit(0) +
                    7 * _pesel.GetDigit(1) +
                    3 * _pesel.GetDigit(2) +
                    1 * _pesel.GetDigit(3) +
                    9 * _pesel.GetDigit(4) +
                    7 * _pesel.GetDigit(5) +
                    3 * _pesel.GetDigit(6) +
                    1 * _pesel.GetDigit(7) +
                    9 * _pesel.GetDigit(8) +
                    7 * _pesel.GetDigit(9);

            return checkSum % 10;
        }

        private void ExtractGender() 
        {
            int genderDigit = _pesel.GetDigit(9);

            _pesel.Gender = genderDigit % 2 == 0 ? Enums.Gender.Female : Enums.Gender.Male;
        }

        private void ExtractDateOfBirth()
        {
            var peselExtractorService = new PeselExtractorService(_pesel);

            int year = peselExtractorService.GetBirthYear();
            int month = peselExtractorService.GetBirthMonth();
            int day = peselExtractorService.GetBirthDay();

            if (!IsYearValid(year))
                _errors.Add(ValidationErrorRepository.InvalidYearError);

            if (!IsMonthValid(month))
                _errors.Add(ValidationErrorRepository.InvalidMonthError);

            if (!IsDayValid(year,month,day))
                _errors.Add(ValidationErrorRepository.InvalidDayError);

            bool dateValid = DateTime.TryParse($"{year}-{month}-{day}", out DateTime dateTime);

            if (dateValid)
                _pesel.DateOfBirth = dateTime.Date;
        }

        private bool IsDayValid(int year, int month, int day)
        {
            if ((day > 0 && day < 32) && IsMonth31(month))
            {
                return true;
            }
            else if ((day > 0 && day < 31) && IsMonth30(month))
            {
                return true;
            }
            else if ((day > 0 && day < 30 && IsYearLeap(year)) ||
                  (day > 0 && day < 29 && !IsYearLeap(year)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsMonth31(int month)
        {
            return month == 1 || month == 3 || month == 5 ||
                    month == 7 || month == 8 || month == 10 ||
                    month == 12;
        }

        private bool IsMonth30(int month)
        {
            return month == 4 || month == 6 || month == 9 || month == 11;
        }

        private bool IsYearLeap(int year)
        {
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                return true;
            else
                return false;
        }

        private bool IsYearValid(int year)
        {
            return year > 1799 && year < 2300;
        }

        private bool IsMonthValid(int month)
        {
            return month > 0 && month < 13;
        }

        private PeselValidationResponse GenerateResponse(string pesel = "")
        {
            return new PeselValidationResponse()
            {
                Pesel = string.IsNullOrEmpty(pesel) ? _pesel.PeselNumber : pesel,
                IsValid = !_errors.Any(),
                DateOfBirth = _pesel?.DateOfBirth ?? null,
                Gender = _pesel?.Gender.ToString(),
                Errors = _errors.ToArray()
            };
        }

        private string Normalize(string pesel)
        {
            return pesel.Trim().Replace(" ", "");
        }

        private bool Parse(string pesel)
        {
            try
            {
                _pesel = PeselParserService.Parse(pesel);
                return true;
            }
            catch(ParseException)
            {
                _errors.Add(ValidationErrorRepository.NumberRequiredError);
            }
            catch (ParseLengthException)
            {
                _errors.Add(ValidationErrorRepository.InvalidLengthError);
            }
            return false;
        }
    }
}
