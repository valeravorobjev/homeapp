using System;

namespace HomeApp.Core.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base()
        {

        }

        public UserNotFoundException(string message)
            : base(message)
        {

        }
    }
}
