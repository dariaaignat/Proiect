using IgnatDariaLaboratoryAnalysis.Models;
using Microsoft.EntityFrameworkCore;

namespace IgnatDariaLaboratoryAnalysis.Database
{
    public class LabDbContext : DbContext
    {
        public LabDbContext(DbContextOptions<LabDbContext> options) : base(options) { }

        public required DbSet<RoleModel> Roles { get; set; }
        public required DbSet<UserModel> Users { get; set; }
        public required DbSet<ClientModel> Clients { get; set; }
        public required DbSet<EmployeeModel> Employees { get; set; }
        public required DbSet<AnalysisModel> Analyses { get; set; }
        public required DbSet<OrderModel> Orders { get; set; }
        public required DbSet<AnalysisOrderModel> AnalysisOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleModel>().ToTable("roles");
            modelBuilder.Entity<UserModel>().ToTable("users");
            modelBuilder.Entity<ClientModel>().ToTable("clients");
            modelBuilder.Entity<EmployeeModel>().ToTable("employee");
            modelBuilder.Entity<AnalysisModel>().ToTable("analysis");
            modelBuilder.Entity<OrderModel>().ToTable("orders");
            modelBuilder.Entity<AnalysisOrderModel>().ToTable("analysis_orders");

            modelBuilder.Entity<ClientModel>()
                .Property(c => c.TelephoneNumber)
                .HasColumnName("telephone_number");

            modelBuilder.Entity<ClientModel>()
                .Property(c => c.WorkPoint)
                .HasColumnName("work_point");

            modelBuilder.Entity<ClientModel>()
                .Property(c => c.DelegateName)
                .HasColumnName("delegate_name");

            modelBuilder.Entity<ClientModel>()
                .Property(c => c.UserId)
                .HasColumnName("user_id");

            modelBuilder.Entity<EmployeeModel>()
                .Property(e => e.UserId)
                .HasColumnName("user_id");

            modelBuilder.Entity<EmployeeModel>()
                .Property(e => e.FirstName)
                .HasColumnName("first_name");

            modelBuilder.Entity<EmployeeModel>()
                .Property(e => e.LastName)
                .HasColumnName("last_name");

            modelBuilder.Entity<AnalysisOrderModel>()
                .HasKey(ao => new { ao.AnalysisId, ao.OrderId });

            modelBuilder.Entity<ClientModel>()
                .HasOne(c => c.User)
                .WithOne(u => u.Client)
                .HasForeignKey<ClientModel>(c => c.UserId);

            modelBuilder.Entity<EmployeeModel>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<EmployeeModel>(e => e.UserId);

            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId);

            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeId);

            modelBuilder.Entity<AnalysisOrderModel>()
                .HasOne(ao => ao.Analysis)
                .WithMany(a => a.AnalysisOrders)
                .HasForeignKey(ao => ao.AnalysisId);

            modelBuilder.Entity<AnalysisOrderModel>()
                .HasOne(ao => ao.Order)
                .WithMany(o => o.AnalysisOrders)
                .HasForeignKey(ao => ao.OrderId);
        }
    }
}