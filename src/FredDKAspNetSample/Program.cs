using Ardalis.Result.AspNetCore;
using FredDevelopmentKit.Services;

namespace FredDKAspNetSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddFredWebServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // add a sample endpoint to get releases
            app.MapGet("/api/releases", async (IFredReleaseService releaseService) =>
            {
                return (await releaseService.GetReleases()).ToMinimalApiResult();
            });

            app.Run();
        }
    }
}
