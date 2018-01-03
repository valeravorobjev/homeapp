using System;

namespace HomeApp.Core.Exceptions
{
    public class ActiveCodeException : Exception
    {
        public ActiveCodeException()
            : base()
        {

        }

        public ActiveCodeException(string message)
            : base(message)
        {

        }
    }
}
