namespace TodoApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Application Builder
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Application
            WebApplication application = builder.Build();

            application.Run();
        }
    }
}