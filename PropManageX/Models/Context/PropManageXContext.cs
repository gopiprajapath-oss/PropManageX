using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PropManageX.Migrations;
using PropManageX.Models.Entities;
using System.Diagnostics.Contracts;

namespace PropManageX.Models.Context
{
    public class PropManageXContext : DbContext
    {
        public PropManageXContext()
        {
            
        }
        public PropManageXContext(DbContextOptions<PropManageXContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }
        
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PropertyModel> Properties { get; set; }
        public DbSet<UnitModel> Units { get; set; }
        public DbSet<LeadModel> Leads { get; set; }
        public DbSet<SiteVisitModel> SiteVisits { get; set; }
        public DbSet<DealModel> Deals { get; set; }
        public DbSet<ContractModel> Contracts { get; set; }
        public DbSet<InvoiceModel> Invoices { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }
        public DbSet<AmenityModel> Amenities { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }
        public DbSet<MaintenanceRequestModel> MaintenanceRequests { get; set; }
        public DbSet<RenewalModel> Renewals { get; set; }
        public DbSet<RevenueReportModel> RevenueReports { get; set; }

        public DbSet<VendorAssignmentModel> VendorAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Deal → Lead (Disable cascade)
            modelBuilder.Entity<DealModel>()
                .HasOne(d => d.LeadModel)
                .WithMany(l => l.Deals)
                .HasForeignKey(d => d.LeadID)
                .OnDelete(DeleteBehavior.Restrict);

            // Deal → Unit (Disable cascade)
            modelBuilder.Entity<DealModel>()
                .HasOne(d => d.UnitModel)
                .WithMany(u => u.Deals)
                .HasForeignKey(d => d.UnitID)
                .OnDelete(DeleteBehavior.Restrict);

            // Deal → Contract
            modelBuilder.Entity<ContractModel>()
                .HasOne(c => c.Deal)
                .WithMany(d => d.Contracts)
                .HasForeignKey(c => c.DealID)
                .OnDelete(DeleteBehavior.Restrict);

            // Contract → Invoice
            modelBuilder.Entity<InvoiceModel>()
                .HasOne(i => i.Contract)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.ContractID)
                .OnDelete(DeleteBehavior.Restrict);

            //UNit -> MaintenanceRequest
            modelBuilder.Entity<MaintenanceRequestModel>()
                .HasOne(m => m.UnitModel)
                .WithMany(u => u.MaintenanceRequests)
                .HasForeignKey(m => m.UnitID)
                .OnDelete(DeleteBehavior.Restrict);
            //Tenant -> MaintenanceRequest
            modelBuilder.Entity<MaintenanceRequestModel>()
                .HasOne(m => m.Tenant)
                .WithMany(t => t.MaintenanceRequests)
                .HasForeignKey(m => m.TenantID)
                .OnDelete(DeleteBehavior.Restrict);
            // MaintenanceRequest → VendorAssignment    
            modelBuilder.Entity<VendorAssignmentModel>()
                .HasOne(v => v.MaintenanceRequestModel)
                .WithMany(m => m.VendorAssignments)
                .HasForeignKey(v => v.RequestID)
                .OnDelete(DeleteBehavior.Restrict);



            // Property → Units
            modelBuilder.Entity<UnitModel>()
                .HasOne(u => u.PropertyModel)
                .WithMany(p => p.Units)
                .HasForeignKey(u => u.PropertyID)
                .OnDelete(DeleteBehavior.Restrict);

            // Property → Amenities
            modelBuilder.Entity<AmenityModel>()
                .HasOne(a => a.PropertyModel)
                .WithMany(p => p.Amenities)
                .HasForeignKey(a => a.PropertyID)
                .OnDelete(DeleteBehavior.Restrict);

            // Property → Leads
            modelBuilder.Entity<LeadModel>()
                .HasOne(l => l.PropertyModel)
                .WithMany(p => p.Leads)
                .HasForeignKey(l => l.PropertyID)
                .OnDelete(DeleteBehavior.Restrict);

            // Lead → Deals
            modelBuilder.Entity<DealModel>()
                .HasOne(d => d.LeadModel)
                .WithMany(l => l.Deals)
                .HasForeignKey(d => d.LeadID)
                .OnDelete(DeleteBehavior.Restrict);

            // Lead → SiteVisits
            modelBuilder.Entity<SiteVisitModel>()
                .HasOne(s => s.Lead)
                .WithMany(l => l.SiteVisits)
                .HasForeignKey(s => s.LeadID)
                .OnDelete(DeleteBehavior.Restrict);

            // SiteVisit → Agent (User)
            modelBuilder.Entity<SiteVisitModel>()
                .HasOne(s => s.Agent)
                .WithMany()
                .HasForeignKey(s => s.AgentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Deal → Unit
            modelBuilder.Entity<DealModel>()
                .HasOne(d => d.UnitModel)
                .WithMany(u => u.Deals)
                .HasForeignKey(d => d.UnitID)
                .OnDelete(DeleteBehavior.Restrict);

            // Deal → Contracts
            modelBuilder.Entity<ContractModel>()
                .HasOne(c => c.Deal)
                .WithMany(d => d.Contracts)
                .HasForeignKey(c => c.DealID)
                .OnDelete(DeleteBehavior.Restrict);

            // Contract → Invoices
            modelBuilder.Entity<InvoiceModel>()
                .HasOne(i => i.Contract)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.ContractID)
                .OnDelete(DeleteBehavior.Restrict);

            // Contract → Renewals
            modelBuilder.Entity<RenewalModel>()
                .HasOne(r => r.ContractModel)
                .WithMany()
                .HasForeignKey(r => r.ContractID)
                .OnDelete(DeleteBehavior.Restrict);

            // Unit → MaintenanceRequests
            modelBuilder.Entity<MaintenanceRequestModel>()
                .HasOne(m => m.UnitModel)
                .WithMany(u => u.MaintenanceRequests)
                .HasForeignKey(m => m.UnitID)
                .OnDelete(DeleteBehavior.Restrict);

            // Tenant (User) → MaintenanceRequests
            modelBuilder.Entity<MaintenanceRequestModel>()
                .HasOne(m => m.Tenant)
                .WithMany(t => t.MaintenanceRequests)
                .HasForeignKey(m => m.TenantID)
                .OnDelete(DeleteBehavior.Restrict);

            // MaintenanceRequest → VendorAssignments
            modelBuilder.Entity<VendorAssignmentModel>()
                .HasOne(v => v.MaintenanceRequestModel)
                .WithMany(m => m.VendorAssignments)
                .HasForeignKey(v => v.RequestID)
                .OnDelete(DeleteBehavior.Restrict);

            // User → Notifications
            modelBuilder.Entity<NotificationModel>()
                .HasOne(n => n.UserModel)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContractModel>()
                .HasOne(c => c.Deal)
                .WithMany(d => d.Contracts)
                .HasForeignKey(c => c.DealID)
                .OnDelete(DeleteBehavior.Cascade);

            // Deal → Contracts (Cascade delete)
            modelBuilder.Entity<ContractModel>()
                .HasOne(c => c.Deal)
                .WithMany(d => d.Contracts)
                .HasForeignKey(c => c.DealID)
                .OnDelete(DeleteBehavior.Cascade);

            // Contract → Invoices (Cascade delete)
            modelBuilder.Entity<InvoiceModel>()
                .HasOne(i => i.Contract)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.ContractID)
                .OnDelete(DeleteBehavior.Cascade);

            // Contract → Renewals (Cascade delete)
            modelBuilder.Entity<RenewalModel>()
                .HasOne(r => r.ContractModel)
                .WithMany()
                .HasForeignKey(r => r.ContractID)
                .OnDelete(DeleteBehavior.Cascade);




        }


    }
}
