using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NasaMeteorLanding.DAL.Models
{
    public class Geolocation
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
