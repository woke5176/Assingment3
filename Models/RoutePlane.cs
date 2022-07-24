using System;
using System.Collections.Generic;

namespace assignment3New.Models
{
    public partial class RoutePlane
    {
        public RoutePlane()
        {
            FlightInstances = new HashSet<FlightInstance>();
        }

        public int RouteId { get; set; }
        public int PlaneId { get; set; }

        public virtual Airplane Plane { get; set; } = null!;
        public virtual Route Route { get; set; } = null!;
        public virtual ICollection<FlightInstance> FlightInstances { get; set; }
    }
}
