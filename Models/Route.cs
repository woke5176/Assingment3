using System;
using System.Collections.Generic;

namespace assignment3New.Models
{
    public partial class Route
    {
        public Route()
        {
            FlightInstances = new HashSet<FlightInstance>();
        }

        public int RouteId { get; set; }
        public int AirlineCode { get; set; }
        public int DepartureAirportCode { get; set; }
        public int ArrivalAirportCode { get; set; }

        public virtual Airline AirlineCodeNavigation { get; set; } = null!;
        public virtual Airport ArrivalAirportCodeNavigation { get; set; } = null!;
        public virtual Airport DepartureAirportCodeNavigation { get; set; } = null!;
        public virtual RoutePlane RouteNavigation { get; set; } = null!;
        public virtual ICollection<FlightInstance> FlightInstances { get; set; }
    }
}
