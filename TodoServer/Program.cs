using Microsoft.AspNetCore.Http.Connections;

namespace TodoApplication
{
    public class Program
    {
        private const string CORS_POLICY_NAME = "CorsPolicy";

        public static void Main(string[] args)
        {
            // Application Builder
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services);

            // Application
            WebApplication application = builder.Build();
            ConfigureApplication(application);

            // Start server
            application.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY_NAME,
                    builder => builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        private static void ConfigureApplication(WebApplication application)
        {
            application.UseCors(CORS_POLICY_NAME);
            application.UseRouting();

            application.MapHub<TaskHub>("/Tasks", options =>
            {
                options.Transports = HttpTransportType.WebSockets;
                options.AllowStatefulReconnects = true;
            });
        }
    }
}