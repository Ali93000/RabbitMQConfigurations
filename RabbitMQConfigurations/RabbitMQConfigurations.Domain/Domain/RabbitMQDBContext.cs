using Microsoft.EntityFrameworkCore;
using RabbitMQConfigurations.Entities.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Domain.Domain
{
    public partial class RabbitMQDBContext : DbContext
    {
        public RabbitMQDBContext(DbContextOptions<RabbitMQDBContext> options) : base(options) { }

        public virtual DbSet<DB_Product> Products { get; set; } = null!;
        public virtual DbSet<DB_ProductsChart> ProductsCharts { get; set; } = null!;
        public virtual DbSet<DB_StringMessageConsumer> StringMessageConsumers { get; set; } = null!;
        public virtual DbSet<DB_User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=RabbitMQDB;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Trusted_Connection=TRUE");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DB_Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            });

            modelBuilder.Entity<DB_ProductsChart>(entity =>
            {
                entity.ToTable("ProductsChart");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Referece).HasMaxLength(50);

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsCharts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductsChart_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProductsCharts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ProductsChart_User");
            });

            modelBuilder.Entity<DB_StringMessageConsumer>(entity =>
            {
                entity.ToTable("StringMessageConsumer");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.Message).HasMaxLength(1500);
            });

            modelBuilder.Entity<DB_User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
