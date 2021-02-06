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

    public class MessageRepo : IMessageRepo
    {
        private readonly MessageContext _context;
        public MessageRepo(MessageContext context)
        {
            _context = context;
        }

        public MessageModel GetById(int id)
        {
            var message = _context.Messages.SingleOrDefault(m => m.Id == id);
            return message;
        }

        public void Save(MessageModel messageModel)
        {
            _context.Messages.Add(messageModel);

            //Simulate long-running transaction
            Task.Delay(2000);

            _context.SaveChanges();
        }
    }
}
