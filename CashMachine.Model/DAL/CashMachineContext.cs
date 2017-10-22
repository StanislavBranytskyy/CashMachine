using CashMachine.Model.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CashMachine.Model.DAL
{
    public class CashMachineContext : DbContext
    {
        public CashMachineContext() : base("CashMachineContext")
        {
            if (!Database.Exists())
            {
                Database.SetInitializer(new CashMachineInitializer());
            }
        }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Operations> Operations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
