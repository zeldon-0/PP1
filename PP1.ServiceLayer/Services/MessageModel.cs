using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PP1.ServiceLayer.Services
{
    [Table("tbl_message")]
    public class MessageModel
    {
        [Column("message_id"), Key]
        public int Id { get; set; }
        [Column("data")]
        public int Data { get; set; }
    }
}
