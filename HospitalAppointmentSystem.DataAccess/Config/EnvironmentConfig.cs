using DotNetEnv;

namespace HospitalAppointmentSystem.DataAccess.Config;

public abstract class EnvironmentConfig
{
    public static string PostgreSqlConnection => 
        $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
        $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
        $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
        $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
        $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};";

    public static void LoadEnv()
    {
        try
        {
            var projectDir =
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
            var envFilePath = Path.Combine(projectDir, "HospitalAppointmentSystem.DataAccess", "Docker", ".env");

            if (File.Exists(envFilePath))
            {
                Env.Load(envFilePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}