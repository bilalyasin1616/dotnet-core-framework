using Framework.Models;
using System;
using System.Threading.Tasks;

namespace Framework.Services
{
    public interface IRabbitMqService
    {
        void ReceiveRequest<TState>(string queue, Type objectType, Func<object, TState, ConsoleRequestMeta, Task<bool>> syncFunction);
        void SendRequest<TState>(string queue, object data, TState state);
    }
}