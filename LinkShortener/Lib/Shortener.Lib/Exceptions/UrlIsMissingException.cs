using System;

namespace Shortener.Lib.Exceptions
{
    public class UrlIsMissingException : Exception
    {
        public UrlIsMissingException()
        {
        }

        public UrlIsMissingException(string message) : base(message)
        {
        }
    }
}