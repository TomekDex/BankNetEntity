using BankNetEntityDB;
using BankNetEntityDB.DBModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNetEntityDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ContextBankNetEntityDB fotoAppDBContext = new ContextBankNetEntityDB();
            //fotoAppDBContext.Set<User>().Add(new User() { FirstName = "aa" });
            //fotoAppDBContext.User.Add(new User() { FirstName = "aa" });

          var a =  fotoAppDBContext.User.ToList();
            //fotoAppDBContext.SaveChanges();
        }
    }
}
