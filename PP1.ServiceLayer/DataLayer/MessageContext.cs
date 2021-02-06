using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PP1.ServiceLayer.Services;

namespace PP2.ServiceLayer.DataLayer
{

    public class MessageContext : DbContext
    {
        public DbSet<MessageModel> Messages { get; set; }
        public MessageContext() : base()
        {
            Database.EnsureCreated();
        }

        public MessageContext(DbContextOptions<MessageContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=.;Database=PP;Trusted_Connection=True");
        //}

    }
}
