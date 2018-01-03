using System;

namespace HomeApp.Core.Exceptions
{
    public class LoginOrPasswordException : Exception
    {
        public LoginOrPasswordException()
            : base()
        {

        }

        public LoginOrPasswordException(string message)
            : base(message)
        {

        }
    }
}
