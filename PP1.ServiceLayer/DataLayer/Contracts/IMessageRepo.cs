using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PP1.ServiceLayer.Services;

namespace PP2.ServiceLayer.DataLayer.Contracts
{
    public interface IMessageRepo
    {
        void Save(MessageModel messageModel);
        MessageModel GetById(int id);
    }
}
