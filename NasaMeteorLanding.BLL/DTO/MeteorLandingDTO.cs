using System;
using System.Collections.Generic;
using System.Text;

namespace NasaMeteorLanding.BLL.DTO
{
    public class MeteorLandingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nametype { get; set; }
        public string Recclass { get; set; }
        public double Mass { get; set; }
        public string Fall { get; set; }

        public DateTime Date { get; set; }
        public double Reclat { get; set; }
        public double Reclong { get; set; }
        public GeolocationDTO Geolocation { get; set; }
    }
}
