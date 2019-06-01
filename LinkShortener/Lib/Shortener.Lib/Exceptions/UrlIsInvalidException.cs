using System;

namespace Shortener.Lib.Exceptions
{
    public class UrlIsInvalidException : Exception
    {
        public UrlIsInvalidException()
        {
        }

        public UrlIsInvalidException(string message) : base(message)
        {
        }
    }
}