using System;

namespace HomeApp.Core.Exceptions
{
    public class UserNotActivatedException : Exception
    {
        public UserNotActivatedException()
            : base()
        {

        }

        public UserNotActivatedException(string message)
            : base(message)
        {

        }
    }
}
