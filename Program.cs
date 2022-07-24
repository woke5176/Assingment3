using assignment3New.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace assignment3New
{
    public class Program
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AirlineBooking"].ConnectionString;
        public static void Main(string[] args)
        {
          
            var options = new DbContextOptionsBuilder<AirlineBookingContext>()
                               .UseLazyLoadingProxies()
                             .UseSqlServer(connectionString)
                              .Options;
            var dbContext = new AirlineBookingContext(options);
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<AirlineBookingContext>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}