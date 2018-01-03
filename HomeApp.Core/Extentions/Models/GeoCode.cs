using Newtonsoft.Json;

namespace HomeApp.Core.Extentions.Models
{
    internal class GeoCode
    {
        [JsonProperty("results")]
        public Result[] Results { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    internal class Result
    {
        [JsonProperty("address_components")]
        public AddressComponent[] AddressComponents { get; set; }
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
        [JsonProperty("partial_match")]
        public bool PartialMatch { get; set; }
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
        [JsonProperty("Geometry")]
        public string[] Types { get; set; }
    }

    internal class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("location_type")]
        public string LocationType { get; set; }
        [JsonProperty("viewport")]
        public Viewport Viewport { get; set; }
    }

    internal class Location
    {
        [JsonProperty("Lat")]
        public float Lat { get; set; }
        [JsonProperty("Lng")]
        public float Lng { get; set; }
    }

    internal class Viewport
    {
        [JsonProperty("northeast")]
        public Northeast Northeast { get; set; }
        [JsonProperty("southwest")]
        public Southwest Southwest { get; set; }
    }

    internal class Northeast
    {
        [JsonProperty("Lat")]
        public float Lat { get; set; }
        [JsonProperty("Lng")]
        public float Lng { get; set; }
    }

    internal class Southwest
    {
        [JsonProperty("Lat")]
        public float Lat { get; set; }
        [JsonProperty("Lng")]
        public float Lng { get; set; }
    }

    internal class AddressComponent
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }
        [JsonProperty("short_name")]
        public string ShortName { get; set; }
        [JsonProperty("types")]
        public string[] Types { get; set; }
    }
}


