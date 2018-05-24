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
            ContextBankNetEntityDB DBContext = new ContextBankNetEntityDB();

        //  var a =  DBContext.User.Add(new User() { FirstName ="Tomasz", LastName = "Juszczak", Login = ""})
            //fotoAppDBContext.SaveChanges();
        }
    }
}
