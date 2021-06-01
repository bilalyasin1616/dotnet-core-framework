using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Kafka
{
    public class TopicRequest<S,R>
    {
        public S State { get; set; }
        public R Request { get; set; }

        public TopicRequest(S state, R request)
        {
            State = state;
            Request = request;
        }

        public TopicRequest()
        {
        }
    }
}
