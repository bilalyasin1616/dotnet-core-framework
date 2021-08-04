using System;

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
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == null)
                throw new Exception("Application environment is not set make sure to set ASPNETCORE_ENVIRONMENT with a proper variable");
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower();
        }

        public static bool IsDevelopment()
        {
            return GetEnvironment() == Environments.Development.ToString().ToLower();
        }
        public static bool IsStaging()
        {
            return GetEnvironment() == Environments.Staging.ToString().ToLower();
        }
        public static bool IsProduction()
        {
            return GetEnvironment() == Environments.Production.ToString().ToLower();
        }
    }
}
