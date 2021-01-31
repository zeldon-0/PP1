using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PP1.ServiceLayer.Services
{
    public class DataProducer : IDataProducer
    {
        private int _currentIndex;
        private List<int> _values;
        public DataProducer()
        {
            _currentIndex = 0;
            _values = Enumerable.Range(0, 10).ToList();
        }

        public MessageModel Produce()
        {
            if (_currentIndex == _values.Count)
            {
                return null;
            }
            var currentMessage = new MessageModel
            {
                Data = _values[_currentIndex]
            };
            _currentIndex++;
            return currentMessage;
        }
    }
}
