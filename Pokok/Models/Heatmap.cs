﻿using System;

namespace Pokok.Models
{
    public class Heatmap
    {
        public Heatmap(double latitude, double longitude, double weight)
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
