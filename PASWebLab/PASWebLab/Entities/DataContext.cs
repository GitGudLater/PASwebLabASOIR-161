using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASWebLab.Entities.SubLayerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PASWebLab.Entities
{
    public class DataContext:DbContext
    {
        public DbSet<Employe> Employers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public IConfiguration Configuration { get; }

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Contracts to employe
            modelBuilder.Entity<ContractsEmploye>()
                .HasKey(t => new { t.ContractId, t.EmployeId });

            modelBuilder.Entity<ContractsEmploye>()
                .HasOne(sc => sc.Contract)
                .WithOne(s => s.Employe);

            modelBuilder.Entity<ContractsEmploye>()
                .HasOne(sc => sc.Employe)
                .WithMany(c => c.Contracts)
                .HasForeignKey(sc => sc.EmployeId);

            //Supply to Contracts
            modelBuilder.Entity<SupplyContract>()
                .HasKey(t => new { t.SupplyId, t.ContractId });

            modelBuilder.Entity<SupplyContract>()
                .HasOne(sc => sc.Supply)
                .WithOne(c => c.Contract);

            modelBuilder.Entity<SupplyContract>()
                .HasOne(sc => sc.Contract)
                .WithOne(c => c.Supply);

            //Supply to Organization
            modelBuilder.Entity<SupplyOrganizations>()
                .HasKey(t => new { t.SupplyId, t.OrganizationId });

            modelBuilder.Entity<SupplyOrganizations>()
                .HasOne(sc => sc.Organization)
                .WithOne(s => s.Supply);

            modelBuilder.Entity<SupplyOrganizations>()
                .HasOne(sc => sc.Supply)
                .WithMany(sc => sc.Organization);
                //.HasForeignKey(fk => fk.OrganizationId);


        }



    }
}
