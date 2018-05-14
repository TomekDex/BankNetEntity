using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNetEntityDB.DBModel
{
    public class Transfers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Value { get; set; }
        public string Title { get; set; }
        public int UserIDTo { get; set; }
        public int UserIDFrom { get; set; }

        public User UserIDsTo { get; set; }
        public User UserIDsFrom { get; set; }
    }
}
