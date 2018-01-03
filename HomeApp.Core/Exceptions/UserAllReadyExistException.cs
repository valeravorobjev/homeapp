using System;

namespace HomeApp.Core.Exceptions
{
    public class UserAllReadyExistException : Exception
    {
        public UserAllReadyExistException()
            : base()
        {

        }

        public UserAllReadyExistException(string message)
            : base(message)
        {

        }
    }
}
