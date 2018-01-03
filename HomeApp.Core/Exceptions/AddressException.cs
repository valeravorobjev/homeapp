using System;

namespace HomeApp.Core.Exceptions
{
    public class AddressException : Exception
    {
        public AddressException()
            : base()
        {

        }

        public AddressException(string message)
            : base(message)
        {

        }
    }
}
