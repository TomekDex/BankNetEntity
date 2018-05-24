using BankNetEntityDB.DBModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankNetEntityDB
{
    public class ContextBankNetEntityDB : DbContext
    {
        public ContextBankNetEntityDB(string nameOrConnectionString)
        : base(nameOrConnectionString)
        {
        }

        public ContextBankNetEntityDB()
            : base(SeachConnectionString())
        {
        }

        public static string SeachConnectionString()
        {
            SqlConnectionStringBuilder SCSB = new SqlConnectionStringBuilder();

            SCSB.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework";
            //if (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Contains("assembly"))
            //    return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\BankNetEntity\BankNetEntity\BankNetEntityDB\BankNetEntityDB.mdf;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework";
            SCSB.Add("AttachDbFilename", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\BankNetEntityDB.mdf");

            File.WriteAllText("C:\\a.txt", SCSB.ConnectionString);
            return SCSB.ConnectionString;
        }
        public DbSet<User> User { get; set; }
        public DbSet<Transfers> Transfers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfers>().HasRequired(m => m.UserFrom).WithMany(m => m.TransfersFrom).HasForeignKey(m => m.UserIDFrom)
                 .WillCascadeOnDelete(false);
            modelBuilder.Entity<Transfers>().HasRequired(m => m.UserTo).WithMany(m => m.TransfersTo).HasForeignKey(m => m.UserIDTo)
                .WillCascadeOnDelete(false);
        }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}
