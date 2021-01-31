using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PP1.ServiceLayer.Services
{
    public class DataProducerWithMonitor : IDataProducer
    {
        private int _currentIndex;
        private List<int> _values;
        public DataProducerWithMonitor()
        {
            _currentIndex = 0;
            _values = Enumerable.Range(0, 10).ToList();
        }

        public MessageModel Produce()
        {

            MessageModel currentMessage = default(MessageModel);
            Monitor.Enter(_values);
            if (_currentIndex == _values.Count)
            {
                Monitor.Exit(_values);
                return null;
            }

            currentMessage = new MessageModel
            {
                Data = _values[_currentIndex]
            };
            _currentIndex++;

            Monitor.Exit(_values);
            return currentMessage;
        }
    }
}
