using System;

namespace HomeApp.Core.Exceptions
{
    public class GeoCodeException : Exception
    {
        public GeoCodeException()
            : base()
        {

        }

        public GeoCodeException(string message)
            : base(message)
        {

        }
    }
}
