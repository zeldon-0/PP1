using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PP1.ServiceLayer.Services
{
    public class BlockingDataProducer : IDataProducer
    {
        private readonly BlockingCollection<int> _queue = new BlockingCollection<int>();

        public BlockingDataProducer()
        {
            foreach (var i in Enumerable.Range(0, 10))
            {
                _queue.Add(i);
            }
            _queue.CompleteAdding();
        }

        public MessageModel Produce()
        {
            try
            {
                if (_queue.IsCompleted)
                {
                    return null;
                }
                int currentValue = _queue.Take();
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
