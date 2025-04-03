using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication1.Config
{
    public static class CorsConfig
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MiPoliticaCors", builder =>
                {
                    builder.WithOrigins(
                            "http://localhost:5173",
                            "http://localhost:8000",
                            "http://192.168.232.230:80",
                            "http://192.168.232.230",
                            "http://localhost",
                            "http://localhost/",
                            "*"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
        }

        public static void UseCorsConfiguration(this IApplicationBuilder app)
        {
            app.UseCors("MiPoliticaCors");
        }
    }
}
