using System;

namespace Framework.Annotations
{
    [AttributeUsage(AttributeTargets.Method,Inherited = false)]
    public class ConsoleRequest: Attribute
    {
        public string Queue { get; set; }
        public Type TypeOfRequest { get; set; }

        public ConsoleRequest(string queue, Type typeOfRequest)
        {
            Queue = queue;
            TypeOfRequest = typeOfRequest;
        }
    }
}
