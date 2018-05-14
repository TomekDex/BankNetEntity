using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNetEntityDB.DBModel
{
    public class User
    {
        public User()
        {
            this.TransfersFrom = new HashSet<Transfers>();
            this.TransfersTo = new HashSet<Transfers>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pass { get; set; }
        public string Login { get; set; }

        public virtual ICollection<Transfers> TransfersFrom { get; set; }
        public virtual ICollection<Transfers> TransfersTo { get; set; }
    }
}
