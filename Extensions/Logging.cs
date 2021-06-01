using Serilog;
using System;

namespace Framework.Extensions
{
    public static class Logging
    {
        public static void LogInformation(this ILogger logger, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace).Information(message, propertyValues);
        }

        public static void LogWarning(this ILogger logger, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace).Warning(message, propertyValues);
        }

        public static void LogError(this ILogger logger, Exception ex, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace).Error(ex, message, propertyValues);
        }

        public static void LogError(this ILogger logger, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace).Error(message, propertyValues);
        }

        public static void LogFatal(this ILogger logger, Exception ex, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace).Fatal(ex, message, propertyValues);
        }

        public static void LogVerbose(this ILogger logger, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace).Verbose(message, propertyValues);
        }

        public static void LogInformation(this ILogger logger, string logContext, object logObject, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace)
                .ForContext(logContext, Newtonsoft.Json.JsonConvert.SerializeObject(logObject))
                .Information(message, propertyValues);
        }

        public static void LogWarning(this ILogger logger, string logContext, object logObject, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace)
                .ForContext(logContext, Newtonsoft.Json.JsonConvert.SerializeObject(logObject))
                .Warning(message, propertyValues);
        }

        public static void LogError(this ILogger logger, string logContext, object logObject, Exception ex, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace)
                .ForContext(logContext, Newtonsoft.Json.JsonConvert.SerializeObject(logObject))
                .Error(ex, message, propertyValues);
        }

        public static void LogFatal(this ILogger logger, string logContext, object logObject, Exception ex, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace)
                .ForContext(logContext, Newtonsoft.Json.JsonConvert.SerializeObject(logObject))
                .Fatal(ex, message, propertyValues);
        }

        public static void LogVerbose(this ILogger logger, string logContext, object logObject, string message, params object[] propertyValues)
        {
            logger.ForContext("StackTrace", Environment.StackTrace)
                .ForContext(logContext, Newtonsoft.Json.JsonConvert.SerializeObject(logObject))
                .Verbose(message, propertyValues);
        }
    }
}
