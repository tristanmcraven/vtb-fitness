
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

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IBankingDetailsService, BankingDetailsService>();
            builder.Services.AddScoped<IPassportService, PassportService>();

            builder.Services.AddDbContext<VtbContext>(options =>
                options.UseNpgsql("Host=localhost;Database=vtb;Username=postgres;Password=1234").UseLazyLoadingProxies(false));

            var app = builder.Build();

            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
                });
            };

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
