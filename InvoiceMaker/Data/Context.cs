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

            // WorkDone Properties
            modelBuilder.Entity<WorkDone>()
                .HasRequired(wd => wd.Client)
                .WithMany(c => c.WorkDone)
                .Map(m => m.MapKey("ClientId"));
            //modelBuilder.Entity<WorkDone>()
            //    .HasRequired(wd => wd.Client)
            //    .WithRequiredDependent()
            //    .Map(m => m.MapKey("ClientId"));

            modelBuilder.Entity<WorkDone>()
                .HasRequired(wd => wd.WorkType)
                .WithMany(wt => wt.WorkDone)
                .Map(m => m.MapKey("WorkTypeId"));
            //modelBuilder.Entity<WorkDone>()
            //    .HasRequired(wd => wd.WorkType)
            //    .WithRequiredDependent()
            //    .Map(m => m.MapKey("WorkTypeId"));

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}