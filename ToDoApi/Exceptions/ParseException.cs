using System;

namespace TodoApi.Exceptions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ParseException : Exception
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ParseException(string message) : base(message) { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ParseLengthException : Exception
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ParseLengthException(string message) : base(message) { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
