namespace TodoApi.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PeselValidationError
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string ErrorCode { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string ErrorMessage { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public PeselValidationError(string errorCode, string errorMessage)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
