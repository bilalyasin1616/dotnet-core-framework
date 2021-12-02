using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Framework.Extensions
{
    public static class Configuration
    {
        public static KeyValuePair<string, string> GetKeyValue(this IConfiguration configuration, string key)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var value = configuration[key];
            if (string.IsNullOrWhiteSpace(key))
                throw new Exception($"Please provide valid value for {key} in configuration.");

            return new KeyValuePair<string, string>(key, value);
        }
    }
}