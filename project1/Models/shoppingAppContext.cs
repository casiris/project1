using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace project1.Models
{
    public partial class shoppingAppContext : DbContext
    {
        public shoppingAppContext()
        {
        }

        public shoppingAppContext(DbContextOptions<shoppingAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server = DESKTOP-UF6LITD; database = shoppingApp; integrated security = true;");
            }
        }

        // i don't think i need any of this. as far as i can tell, this just mimics the database structure, but i don't care about that
        // obviously, in a real project, the user wouldn't know the details of how the database was implemented, but whatever. none of this is real, it's just to-spec
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.StateInitials)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("stateInitials");

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("street");

                entity.Property(e => e.Zipcode).HasColumnName("zipcode");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.DateOrdered)
                    .HasColumnType("datetime")
                    .HasColumnName("dateOrdered");

                //entity.HasOne(d => d.Customer)
                //    .WithMany(p => p.Orders)
                //    .HasForeignKey(d => d.CustomerId)
                //    .HasConstraintName("FK__Orders__customer__286302EC");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.DetailsId)
                    .HasName("PK__OrderDet__EB8EA79003274041");

                entity.Property(e => e.DetailsId).HasColumnName("detailsID");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.QuantityOrdered).HasColumnName("quantityOrdered");

                //entity.HasOne(d => d.Order)
                //    .WithMany(p => p.OrderDetails)
                //    .HasForeignKey(d => d.OrderId)
                //    .HasConstraintName("FK__OrderDeta__order__2B3F6F97");

                //entity.HasOne(d => d.Product)
                //    .WithMany(p => p.OrderDetails)
                //    .HasForeignKey(d => d.ProductId)
                //    .HasConstraintName("FK__OrderDeta__produ__2C3393D0");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("productName");

                entity.Property(e => e.QuantityInStock).HasColumnName("quantityInStock");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
