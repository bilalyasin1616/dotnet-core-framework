using System;

namespace Framework.Models
{
    public class ConsoleRequest<TState>
    {
        public string Data { get; set; }
        public TState State { get; set; }
        public ConsoleRequestMeta Meta { get; set; }
    }

    public class ConsoleRequestMeta
    {
        public string Queue { get; set; }
        public DateTime Created { get; set; }
    }
}
