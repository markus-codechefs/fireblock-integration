using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Noodles.Dashboard.Data;
using Noodles.Dashboard.Services;
using Noodles.Dashboard.Configuration;

namespace Noodles.Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            // Configure Fireblocks settings from environment variables
            var fireblocksConfig = new FireblocksConfig
            {
                ApiKey = Environment.GetEnvironmentVariable("FIREBLOCKS_API_KEY") ?? "",
                PrivateKeyPath = Environment.GetEnvironmentVariable("FIREBLOCKS_PRIVATE_KEY_PATH") ?? "",
                BaseUrl = Environment.GetEnvironmentVariable("FIREBLOCKS_BASE_URL") ?? "https://sandbox-api.fireblocks.io/",
                UseMockApi = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("FIREBLOCKS_API_KEY"))
            };

            builder.Services.AddSingleton(fireblocksConfig);

            // Add HTTP client for Fireblocks API
            builder.Services.AddHttpClient<FireblocksService>();
            builder.Services.AddScoped<FireblocksService>();
            builder.Services.AddScoped<FireblocksJwtService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
