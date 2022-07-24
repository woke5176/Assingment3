using assignment3New.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace assignment3New.Services
{
    public class UserService
    {
        private readonly AirlineBookingContext _context;
        private readonly string _connectionString;

        public UserService(AirlineBookingContext context, IConfiguration config)
        {
            _context = context;
            _connectionString = config.GetConnectionString("AirlineBookingContext");
        }

        public List<FlightInstance> ViewFlightDetails(int sourceCode, int destinationCode)
        {
            var route = _context.Routes.Where(x => (x.DepartureAirportCode == destinationCode && x.ArrivalAirportCode == destinationCode)).ToList().FirstOrDefault();
            int routeId = route.RouteId;


            var rplist = _context.RoutePlanes.Where(r => r.RouteId == routeId).ToList();

            List<FlightInstance> flightInstance = new List<FlightInstance>();
            foreach (var rp in rplist)
            {
                var list = rp.FlightInstances;
                flightInstance.Add((FlightInstance)list);
            }

            return flightInstance;

        }

        void bookTicket(int planeid, int userId, string seatType, int phone, int age, string sex)
        {
            var res = _context.Airplanes.Where(x => x.AirplaneId == planeid).FirstOrDefault();
            var userlist = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            var flightinstanceid = _context.FlightInstances.Where(x => x.PlaneId == planeid).FirstOrDefault();
            var contactdetails = _context.ContactDetails.Include(x => x.Users);
            Random rnd = new Random();
            int passengerid = rnd.Next(100, 1000);
            int seatno = rnd.Next(6, 15);
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var sql = @"INSERT INTO Passengers (PassengerId,PassengerName,Type,SeatNo,UserId,FlightInstId,EmailId,Phone,Age,Sex,Confirmed,Cancelled)"
        + "VALUES (@PassengerId,@PassengerName,@Type,@SeatNo,@UserId,@FlightInstId,@EmailId,@Phone,@Age,@Sex,@Confirmed,@Cancelled)";

                using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ProductId", passengerid);
                    sqlCommand.Parameters.AddWithValue("@ProductName", userlist.UserName);
                    sqlCommand.Parameters.AddWithValue("@Type", seatType);
                    sqlCommand.Parameters.AddWithValue("@SeatNo", seatno);
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlCommand.Parameters.AddWithValue("@FlightInstId", flightinstanceid);
                    sqlCommand.Parameters.AddWithValue("@EmailId", userlist.Email);
                    sqlCommand.Parameters.AddWithValue("@Phone", phone);
                    sqlCommand.Parameters.AddWithValue("@Age", age);
                    sqlCommand.Parameters.AddWithValue("@Sex", sex);
                    sqlCommand.Parameters.AddWithValue("@Confirmed", "Yes");
                    sqlCommand.Parameters.AddWithValue("@Cancelled", "No");


                    var rowsAffected = sqlCommand.ExecuteNonQuery();
                }
            }
        }
        public User viewbookings(int userid)
        {
            var passengerdetails = _context.Users.Include(x => x.Passengers).Where(y => y.UserId == userid).FirstOrDefault();
            return passengerdetails;
        }
    }
}
