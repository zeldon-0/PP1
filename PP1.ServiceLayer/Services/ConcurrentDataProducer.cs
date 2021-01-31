using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP1.ServiceLayer.Services
{
    public class ConcurrentDataProducer : IDataProducer
    {
        private readonly ConcurrentQueue<int> _queue = new ConcurrentQueue<int>();

        public ConcurrentDataProducer()
        {
            foreach (var i in Enumerable.Range(0, 10))
            {
                _queue.Enqueue(i);
            }

        }

        public MessageModel Produce()
        {
            try
            {
                if (_queue.IsEmpty)
                {
                    return null;
                }

                _queue.TryDequeue(out var currentValue);
                return new MessageModel
                {
                    Data = currentValue
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
