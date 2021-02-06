using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PP1.ServiceLayer.Services
{
    public class DataProducer : IDataProducer
    {
        private Queue<MessageModel> _queue;
        public DataProducer()
        {
            _queue = new Queue<MessageModel>();
            _queue.Enqueue(new MessageModel
            {
                Id = 1,
                Data = 1
            });
        }

        public MessageModel Produce()
        {
            var currentMessage = _queue.Dequeue();
            return currentMessage;
        }
    }
}
