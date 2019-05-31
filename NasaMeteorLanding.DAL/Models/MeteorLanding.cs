using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NasaMeteorLanding.DAL.Models
{
    public class MeteorLanding
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nametype { get; set; }
        public string Recclass { get; set; }
        public double Mass { get; set; }
        public string Fall { get; set; }

        [JsonProperty("year")]
        public DateTime Date { get; set; }
        public double Reclat { get; set; }
        public double Reclong { get; set; }
        public Geolocation Geolocation { get; set; }
    }
}
