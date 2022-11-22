using TodoApi.Exceptions;
using TodoApi.Models;

namespace TodoApi.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PeselParserService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static Pesel Parse(string peselNumber)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            bool isNumber = long.TryParse(peselNumber, out long pesel);

            if (!isNumber)
                throw new ParseException($"'{peselNumber}' is not a number.");

            if (peselNumber.Length != 11)
                throw new ParseLengthException($"'{peselNumber}' has incorrect length.");
            
            return new Pesel(peselNumber);
        }
    }
}
