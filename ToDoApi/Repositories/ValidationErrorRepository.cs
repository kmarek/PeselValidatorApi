using TodoApi.Models;

namespace TodoApi.Repositories
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ValidationErrorRepository
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PeselValidationError InvalidLengthError => new PeselValidationError("INVL", "Invalid length. Pesel should have exactly 11 digits.");
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PeselValidationError NumberRequiredError => new PeselValidationError("NBRQ", "Invalid characters. Pesel should be a number.");
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PeselValidationError InvalidYearError => new PeselValidationError("INVY", "Invalid year.");
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PeselValidationError InvalidMonthError => new PeselValidationError("INVM", "Invalid month.");
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PeselValidationError InvalidDayError => new PeselValidationError("INVD", "Invalid day.");
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PeselValidationError InvalidCheckSumError => new PeselValidationError("INVC", "Check sum is invalid. Check last digit.");
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
