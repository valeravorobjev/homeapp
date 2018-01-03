using System;

namespace HomeApp.Core.Exceptions
{
    public class PasswordLengthException : Exception
    {
        public PasswordLengthException()
            : base()
        {

        }

        public PasswordLengthException(string message)
            : base(message)
        {

        }
    }
}
