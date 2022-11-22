using FluentAssertions;
using System;
using TodoApi.Models;
using Xunit;

namespace TodoApiTests
{
    public class PeselTests
    {
        [Theory]
        [InlineData("85040814691", 0, 8)]
        [InlineData("85040814691", 10, 1)]
        public void TestGetDigit(string peselNumber, int digit, int expectedValue)
        {
            var pesel = new Pesel(peselNumber);
            pesel.GetDigit(digit)
                .Should()
                .Be(expectedValue);
        }

        [Theory]
        [InlineData("85040814691", -1)]
        [InlineData("85040814691", 11)]
        public void TestGetDigitInvalidValue(string peselNumber, int digit)
        {
            var pesel = new Pesel(peselNumber);

            Action act = () => pesel.GetDigit(digit);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Index {digit} is invalid for {peselNumber.Length} lenght.");
        }
    }
}
