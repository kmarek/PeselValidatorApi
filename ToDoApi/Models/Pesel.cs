using System;
using TodoApi.Enums;

namespace TodoApi.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Pesel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string PeselNumber { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public DateTime? DateOfBirth { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public Gender Gender { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public Pesel(string peselNumber)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            PeselNumber = peselNumber;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int GetDigit(int index)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            if (index < 0 || index >= PeselNumber.Length)
                throw new ArgumentException($"Index {index} is invalid for {PeselNumber.Length} lenght.");

            char character = PeselNumber[index];
            return Convert.ToInt32(character.ToString());
        }
    }
}
