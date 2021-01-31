using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP1.ServiceLayer.Services
{
    public interface IDataProducer
    {
        MessageModel Produce();
    }
}
