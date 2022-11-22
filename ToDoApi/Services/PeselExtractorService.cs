using TodoApi.Models;

namespace TodoApi.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PeselExtractorService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private Pesel _pesel;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public PeselExtractorService(Pesel pesel)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _pesel = pesel;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int GetBirthMonth()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            int month = GetMonth();

            if (month > 80 && month < 93)
            {
                month -= 80;
            }
            else if (month > 20 && month < 33)
            {
                month -= 20;
            }
            else if (month > 40 && month < 53)
            {
                month -= 40;
            }
            else if (month > 60 && month < 73)
            {
                month -= 60;
            }
            return month;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int GetBirthYear()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            int year = GetYear();
            int month = GetMonth();

            if (month > 80 && month < 93)
            {
                year += 1800;
            }
            else if (month > 0 && month < 13)
            {
                year += 1900;
            }
            else if (month > 20 && month < 33)
            {
                year += 2000;
            }
            else if (month > 40 && month < 53)
            {
                year += 2100;
            }
            else if (month > 60 && month < 73)
            {
                year += 2200;
            }

            return year;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int GetBirthDay()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return 10 * _pesel.GetDigit(4) + _pesel.GetDigit(5);
        }

        private int GetMonth()
        {
            return 10 * _pesel.GetDigit(2) + _pesel.GetDigit(3);
        }

        private int GetYear()
        {
            return 10 * _pesel.GetDigit(0) + _pesel.GetDigit(1);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int GetChecksum()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return _pesel.GetDigit(10);
        }
    }
}
