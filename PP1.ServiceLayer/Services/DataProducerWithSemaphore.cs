using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PP1.ServiceLayer.Services
{
    public class DataProducerWithSemaphore : IDataProducer
    {
        private int _currentIndex;
        private List<int> _values;
        private readonly Semaphore _semaphore = new Semaphore(1, 1);

        public DataProducerWithSemaphore()
        {
            _currentIndex = 0;
            _values = Enumerable.Range(0, 10).ToList();
        }

        public MessageModel Produce()
        {

            MessageModel currentMessage = default(MessageModel);
            _semaphore.WaitOne();
            if (_currentIndex == _values.Count)
            {
                _semaphore.Release();
                return null;
            }

            currentMessage = new MessageModel
            {
                Data = _values[_currentIndex]
            };
            _currentIndex++;

            _semaphore.Release();
            return currentMessage;
        }
    }
}
