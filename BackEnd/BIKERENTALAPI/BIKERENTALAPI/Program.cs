
using BIKERENTALAPI.Database;
using BIKERENTALAPI.IRepository;
using BIKERENTALAPI.IServies;
using BIKERENTALAPI.Repository;
using BIKERENTALAPI.Servies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace BIKERENTALAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseWebRoot("wwwroot");
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

         

        

            var connectionString = builder.Configuration.GetConnectionString("Bikeconnection");
            builder.Services.AddScoped<IManagerRepository>(provider => new ManagerRepository(connectionString));
            builder.Services.AddScoped<IManagerServies, ManagerService>();

            builder.Services.AddScoped<ICustomerRepository>(provider => new CustomerRepository(connectionString));
            builder.Services.AddScoped<ICustomerServies, CustomerServies>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });


            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();
         


            app.Run();
        }
    }
}
