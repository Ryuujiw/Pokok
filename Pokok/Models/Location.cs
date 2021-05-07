using System;

namespace Pokok.Models
{
    public class Location
    {
        public Location(double latitude, double longitude, double weight)
        {
            Latitude = latitude;
            Longitude = longitude;
            Weight = weight;
        }

        public double Weight { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
