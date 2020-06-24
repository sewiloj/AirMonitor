using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public struct Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string DisplayAddress1 { get; set; }
        public string DisplayAddress2 { get; set; }

        public string AddressText => $"{Street} {Number}, {City} - {Country}";
    }
}
