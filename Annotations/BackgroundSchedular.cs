using System;
using System.Threading;

namespace Framework.Annotations
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BackgroundSchedular : Attribute
    {
        public double ScheduledTimeMinute { get; }

        public BackgroundSchedular(double scheduledTimeMinute)
        {
            ScheduledTimeMinute = scheduledTimeMinute;
        }
    }
}
