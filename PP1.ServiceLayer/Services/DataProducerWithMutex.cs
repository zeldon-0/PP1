using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PP1.ServiceLayer.Services
{
    public class DataProducerWithMutex : IDataProducer
    {
        private int _currentIndex;
        private List<int> _values;
        private readonly Mutex _mutex = new Mutex();

        public DataProducerWithMutex()
        {
            _currentIndex = 0;
            _values = Enumerable.Range(0, 10).ToList();
        }

        public MessageModel Produce()
        {

            MessageModel currentMessage = default(MessageModel);
            _mutex.WaitOne();
            if (_currentIndex == _values.Count)
            {
                _mutex.ReleaseMutex();
                return null;
            }

            currentMessage = new MessageModel
            {
                Data = _values[_currentIndex]
            };
            _currentIndex++;

            _mutex.ReleaseMutex();
            return currentMessage;
        }
    }
}
