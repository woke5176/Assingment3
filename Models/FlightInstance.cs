using System;
using System.Collections.Generic;

namespace assignment3New.Models
{
    public partial class FlightInstance
    {
        public FlightInstance()
        {
            Passengers = new HashSet<Passenger>();
        }

        public int InstanceId { get; set; }
        public int RouteId { get; set; }
        public int PlaneId { get; set; }
        public int ESeat { get; set; }
        public int BSeat { get; set; }
        public int FSeat { get; set; }
        public int ECost { get; set; }
        public int BCost { get; set; }
        public int FCost { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }

        public virtual Airplane Plane { get; set; } = null!;
        public virtual Route Route { get; set; } = null!;
        public virtual RoutePlane RouteNavigation { get; set; } = null!;
        public virtual ICollection<Passenger> Passengers { get; set; }
    }
}
