using System;

namespace HomeApp.Core.Exceptions
{
    public class UpdateException : Exception
    {
        public UpdateException()
            : base()
        {

        }

        public UpdateException(string message)
            : base(message)
        {

        }
    }
}
