using System;

namespace HomeApp.Core.Exceptions
{
    public class EmailInvalidException : Exception
    {
        public EmailInvalidException()
            : base()
        {

        }

        public EmailInvalidException(string message)
            : base(message)
        {

        }
    }
}
