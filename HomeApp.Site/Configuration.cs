using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApp.Site
{
    public class AppOptions
    {
        public Geocode Geocode { get; set; }
    }

    public class Geocode
    {
        public string GoogleKey { get; set; }
    }
}
