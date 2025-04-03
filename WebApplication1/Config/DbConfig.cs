
using DotNetEnv;

namespace WebApplication1.Config
{
    public static class DbConfig
    {
        public static string GetConnectionString()
        {
            Env.Load();

            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var name = Environment.GetEnvironmentVariable("DB_NAME");
            var useTrusted = Environment.GetEnvironmentVariable("USE_TRUSTED_CONNECTION");

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Faltan variables esenciales de conexión en el archivo .env");
            }

            if (useTrusted?.ToLower() == "true")
            {
                return $"Server={host},{port};Database={name};Trusted_Connection=True;TrustServerCertificate=True;";
            }
            else
            {
                var user = Environment.GetEnvironmentVariable("DB_USER");
                var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    throw new InvalidOperationException("Faltan usuario o contraseña en el archivo .env");
                }

                return $"Server={host},{port};Database={name};User Id={user};Password={password};TrustServerCertificate=True;";
            }
        }
    }
}
