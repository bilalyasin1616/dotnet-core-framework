using System;

namespace Framework.Annotations
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class BackgroundRequest : Attribute
    {
        public string Queue { get; set; }
        public Type TypeOfRequest { get; set; }

        public BackgroundRequest(string queue, Type typeOfRequest)
        {
            Queue = queue;
            TypeOfRequest = typeOfRequest;
        }
    }
}
