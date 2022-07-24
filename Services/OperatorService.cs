using assignment3New.Models;
using Microsoft.Data.SqlClient;

namespace assignment3New.Services
{
    public class OperatorService
    {
        private readonly AirlineBookingContext _context;
        private readonly string _connectionString;

        public OperatorService(AirlineBookingContext context, IConfiguration config)
        {
            _context = context;
            _connectionString = config.GetConnectionString("AirlineBookingContext");
        }
        public void addFlightInstance(int instanceid,int routeid,int planeid, int e_seat,int b_seat,int f_seat,int e_cost, int b_cost,int f_cost)
        {   
            
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var sql = @"INSERT INTO FlightInstances (InstanceId,RouteId,PlaneId,ESeat,BSeat,FSeat,ECost,BCost,FCost,Departure,Arrival)"
        + "VALUES (@InstanceId,@RouteId,@PlaneId,@ESeat,@BSeat,@FSeat,@ECost,@BCost,@FCost,@Departure,@Arrival)";

                using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@InstanceId", instanceid);
                    sqlCommand.Parameters.AddWithValue("@RouteId", routeid);
                    sqlCommand.Parameters.AddWithValue("@PlaneId", planeid);
                    sqlCommand.Parameters.AddWithValue("@ESeat", e_seat);
                    sqlCommand.Parameters.AddWithValue("@BSeat", b_seat);
                    sqlCommand.Parameters.AddWithValue("@FSeat", f_seat);
                    sqlCommand.Parameters.AddWithValue("@ECost", e_cost);
                    sqlCommand.Parameters.AddWithValue("@BCost", b_cost);
                    sqlCommand.Parameters.AddWithValue("@FCost", f_cost);
             


                    var rowsAffected = sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
