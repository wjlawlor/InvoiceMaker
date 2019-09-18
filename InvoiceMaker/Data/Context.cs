using InvoiceMaker.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InvoiceMaker.Data
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<WorkDone> WorkDones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Client Properties
            modelBuilder.Entity<Client>()
                .Property(p => p.Name)
                    .IsRequired()
                    .HasColumnName("ClientName")
                    .HasMaxLength(255);
            modelBuilder.Entity<Client>()
                .Property(p => p.IsActive)
                    .HasColumnName("IsActivated");

            // WorkType Properties
            modelBuilder.Entity<WorkType>()
                .Property(p => p.Name)
                    .IsRequired()
                    .HasColumnName("WorkTypeName")
                    .HasMaxLength(255);
            modelBuilder.Entity<WorkType>()
                .Property(p => p.Rate)
                    .HasPrecision(18, 2);


            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}