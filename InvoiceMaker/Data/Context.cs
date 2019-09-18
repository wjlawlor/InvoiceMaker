using InvoiceMaker.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InvoiceMaker.Data
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}