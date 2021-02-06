using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PP1.ServiceLayer.Services;
using PP2.ServiceLayer.DataLayer.Contracts;

namespace PP2.ServiceLayer.DataLayer
{
    public class MessageRepoWithMutex : IMessageRepo
    {
        private readonly MessageContext _context;
        private readonly Mutex _mutex = new Mutex();
        public MessageRepoWithMutex(MessageContext context)
        {
            _context = context;
        }

        public MessageModel GetById(int id)
        {
            _mutex.WaitOne();
            var message = _context.Messages.SingleOrDefault(m => m.Id == id);
            _mutex.ReleaseMutex();
            return message;
        }

        public void Save(MessageModel messageModel)
        {
            _mutex.WaitOne();
            _context.Messages.Add(messageModel);

            //Simulate long-running transaction
            Task.Delay(2000);

            _context.SaveChanges();
            _mutex.ReleaseMutex();
        }
    }
}
