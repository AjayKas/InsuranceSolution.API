using Microsoft.EntityFrameworkCore; 
using InsuranceSolution.API.Models;
using System;

namespace InsuranceSolution.API.Data.Contexts
{
    public class AppDbContext : DbContext
    { 

        public DbSet<Contact> Contacts{ get; set; }
        public DbSet<CoveragePlan> CoveragePlans  { get; set; }
        public DbSet<RateChart> RateCharts{ get; set; }

        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

             

            #region CoveragePlans
            builder.Entity<CoveragePlan>().ToTable("CoveragePlans");
            builder.Entity<CoveragePlan>().HasKey(cp => cp.Id);
            builder.Entity<CoveragePlan>().Property(cp => cp.Name);
            builder.Entity<CoveragePlan>().Property(cp => cp.EligibilityDateFrom);
            builder.Entity<CoveragePlan>().Property(cp => cp.EligibilityDateTo);
            builder.Entity<CoveragePlan>().Property(cp => cp.IsDefault);
            builder.Entity<CoveragePlan>().Property(cp => cp.EligibilityCountry);
            builder.Entity<CoveragePlan>().HasMany(cp => cp.RateCharts).WithOne(cp=>cp.CoveragePlan).HasForeignKey(cp=>cp.CoveragePlanId);
            builder.Entity<CoveragePlan>().HasData
          (
                new CoveragePlan
                {
                    Id=1,
                    Name="Gold",
                    EligibilityDateFrom=Convert.ToDateTime("2009-01-01"),
                    EligibilityDateTo = Convert.ToDateTime("2021-01-01"),
                    EligibilityCountry="USA",
                    IsDefault=false
                },
                new CoveragePlan
                {
                    Id=2,
                    Name = "Platinum",
                    EligibilityDateFrom = Convert.ToDateTime("2005-01-01"),
                    EligibilityDateTo = Convert.ToDateTime("2023-01-01"),
                    EligibilityCountry = "CAN",
                    IsDefault = false
                },
                new CoveragePlan
                {
                    Id=3,
                    Name = "Silver",
                    EligibilityDateFrom = Convert.ToDateTime("2001-01-01"),
                    EligibilityDateTo = Convert.ToDateTime("2026-01-01"),
                    EligibilityCountry = "* (any)",
                    IsDefault = true
                }
                );
            #endregion
            #region RateChart
            builder.Entity<RateChart>().ToTable("RateChart");
            builder.Entity<RateChart>().HasKey(rc => rc.Id);
            builder.Entity<RateChart>().Property(rc => rc.AgeBelow);
            builder.Entity<RateChart>().Property(rc => rc.AgeAbove);
            builder.Entity<RateChart>().Property(rc => rc.Gender);
            builder.Entity<RateChart>().Property(rc => rc.NetPrice);
            builder.Entity<RateChart>().HasMany(p => p.Contacts).WithOne(p => p.RateChart).HasForeignKey(p => p.RateChartId);
            builder.Entity<RateChart>().HasData
                (
                    new RateChart
                    {
                        Id=1,
                        CoveragePlanId=1,
                        AgeBelow=41,
                        AgeAbove =null,
                        NetPrice =1000,
                        Gender="M", 
                    },

                    new RateChart
                    {
                        Id=2,
                        CoveragePlanId = 1,
                        AgeBelow=null,
                        AgeAbove=40,
                        NetPrice = 2000,
                        Gender = "M",
                    },
                    new RateChart
                    {
                        Id=3,
                        CoveragePlanId = 1,
                        AgeBelow = 41,
                        AgeAbove = null,
                        NetPrice = 1200,
                        Gender = "F",
                    },
                    new RateChart
                    {
                        Id = 4,
                        CoveragePlanId = 1,
                        AgeBelow = null,
                        AgeAbove = 40,
                        NetPrice = 2500,
                        Gender = "F",
                    },
                    new RateChart
                    {
                        Id = 5,
                        CoveragePlanId = 2,
                        AgeBelow = 41,
                        AgeAbove = null,
                        NetPrice = 1500,
                        Gender = "M",
                    },
                    new RateChart
                    {
                        Id = 6,
                        CoveragePlanId = 2,
                        AgeBelow = null,
                        AgeAbove = 40,
                        NetPrice = 2600,
                        Gender = "M",
                    },
                    new RateChart
                    {
                        Id = 7,
                        CoveragePlanId = 2,
                        AgeBelow = 41,
                        AgeAbove = null,
                        NetPrice = 1900,
                        Gender = "F",
                    },
                    new RateChart
                    {
                        Id = 8,
                        CoveragePlanId = 2,
                        AgeBelow = null,
                        AgeAbove = 40,
                        NetPrice = 2800,
                        Gender = "F",
                    },
                    new RateChart
                    {
                        Id = 9,
                        CoveragePlanId = 3,
                        AgeBelow = 41,
                        AgeAbove = null,
                        NetPrice = 1900,
                        Gender = "M",
                    },
                    new RateChart
                    {
                        Id = 10,
                        CoveragePlanId = 3,
                        AgeBelow = null,
                        AgeAbove = 40,
                        NetPrice = 2900,
                        Gender = "M",
                    },
                    new RateChart
                    {
                        Id = 11,
                        CoveragePlanId = 3,
                        AgeBelow = 41,
                        AgeAbove = null,
                        NetPrice = 2100,
                        Gender = "F",
                    },
                    new RateChart
                    {
                        Id = 12,
                        CoveragePlanId = 3,
                        AgeBelow = null,
                        AgeAbove = 40,
                        NetPrice = 3200,
                        Gender = "F",
                    }
                );
            #endregion
            #region Contacts
            builder.Entity<Contact>().ToTable("Contacts");
            builder.Entity<Contact>().HasKey(p => p.Id);
            builder.Entity<Contact>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Contact>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Contact>().Property(p => p.Address).IsRequired().HasMaxLength(150);
            builder.Entity<Contact>().Property(p => p.City).IsRequired().HasMaxLength(100);
            builder.Entity<Contact>().Property(p => p.Sate).IsRequired().HasMaxLength(100);
            builder.Entity<Contact>().Property(p => p.Country).IsRequired().HasMaxLength(150);
            builder.Entity<Contact>().Property(p => p.DOB).IsRequired();
            builder.Entity<Contact>().Property(p => p.Gender).IsRequired().HasMaxLength(1);
            builder.Entity<Contact>().Property(p => p.SaleDate).IsRequired();
            

            #endregion
        }
    }
}