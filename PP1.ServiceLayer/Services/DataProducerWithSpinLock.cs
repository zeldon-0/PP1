using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PP1.ServiceLayer.Services
{
    public class DataProducerWithSpinLock : IDataProducer
    {
        private int _currentIndex;
        private List<int> _values;
        private  SpinLock _spinLock = new SpinLock();

        public DataProducerWithSpinLock()
        { 
            _currentIndex = 0;
            _values = Enumerable.Range(0, 10).ToList();
        }

        public MessageModel Produce()
        {
            MessageModel currentMessage = default(MessageModel);
            bool lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            if (_currentIndex == _values.Count)
            {
                _spinLock.Exit();
                return null;
            }

            currentMessage = new MessageModel
            {
                Data = _values[_currentIndex]
            };
            _currentIndex++;

            _spinLock.Exit();
            _spinLock = new SpinLock();
            return currentMessage;
        }
    }
}
