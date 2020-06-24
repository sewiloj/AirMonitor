using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AirMonitor.Models
{
    public class Installation
    {
        public string Id { get; set; }
        public Location Location { get; set; }
        public Address Address { get; set; }
        public double Elevation { get; set; }
        public bool Airly { get; set; }
    }
}
