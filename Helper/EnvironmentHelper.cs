namespace Framework.Helper
{
    public static class EnvironmentHelper
    {
        private enum Environments
        {
            Development,
            Staging,
            Production
        }
        public static string GetEnvironment()
        {
            return System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public static bool IsDevelopment()
        {
            return GetEnvironment() == Environments.Development.ToString();
        }
        public static bool IsStaging()
        {
            return GetEnvironment() == Environments.Staging.ToString();
        }
        public static bool IsProduction()
        {
            return GetEnvironment() == Environments.Production.ToString();
        }
    }
}
