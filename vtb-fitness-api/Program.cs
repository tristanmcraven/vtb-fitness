
using Microsoft.EntityFrameworkCore;
using vtb_fitness_api.Model;
using vtb_fitness_api.Services;
using vtb_fitness_api.Services.Interfaces;

namespace vtb_fitness_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IBankingDetailsService, BankingDetailsService>();
            builder.Services.AddScoped<IPassportService, PassportService>();

            builder.Services.AddDbContext<VtbContext>(options =>
                options.UseNpgsql("Host=localhost;Database=vtb;Username=postgres;Password=1234"));

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
