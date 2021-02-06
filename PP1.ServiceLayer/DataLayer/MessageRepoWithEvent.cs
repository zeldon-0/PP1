using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PP1.ServiceLayer.Services;

namespace PP2.ServiceLayer.DataLayer
{
    public class MessageRepoWithEvent
    {
        private readonly MessageContext _context;
        public MessageRepoWithEvent(MessageContext context)
        {
            _context = context;
        }

        public MessageModel GetById(int id)
        {
            var message = _context.Messages.SingleOrDefault(m => m.Id == id);
            return message;
        }

        public void Save(MessageModel messageModel, Action commitAction)
        {
            _context.Messages.Add(messageModel);

            //Simulate long-running transaction
            Task.Delay(2000);

            _context.SaveChanges();
            commitAction.Invoke();
        }
    }
}
