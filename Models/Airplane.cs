using System;
using System.Collections.Generic;

namespace assignment3New.Models
{
    public partial class Airplane
    {
        public Airplane()
        {
            FlightInstances = new HashSet<FlightInstance>();
            RoutePlanes = new HashSet<RoutePlane>();
        }

        public int AirplaneId { get; set; }
        public string AirplaneName { get; set; } = null!;
        public int BSeats { get; set; }
        public int ESeats { get; set; }
        public int FSeats { get; set; }

        public virtual ICollection<FlightInstance> FlightInstances { get; set; }
        public virtual ICollection<RoutePlane> RoutePlanes { get; set; }
    }
}
