using RidersWebAPI.DBContext;
using Microsoft.EntityFrameworkCore;
using RidersWebAPI.IServices;
using RidersWebAPI.Services;

namespace RidersWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // connnection

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IRideService, RideService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<IDriverLocationService, DriverLocationService>();
            builder.Services.AddScoped<IRideRequestService, RideRequestService>();
            builder.Services.AddScoped<IRiderService, RiderService>();
            builder.Services.AddScoped<IRideStatusHistoryService, RideStatusHistoryService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
