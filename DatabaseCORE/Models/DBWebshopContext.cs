using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DatabaseCORE.Models;

namespace DatabaseCORE.Models
{
    public partial class DBWebshopContext : DbContext
    {
        public DBWebshopContext()
        {
        }

        public DBWebshopContext(DbContextOptions<DBWebshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Newsletter> Newsletter { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<PaymentInformation> PaymentInformation { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Shipment> Shipment { get; set; }
        public virtual DbSet<ShipmentItem> ShipmentItem { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<ZipCode> ZipCode { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-HH6J00I\\SQLEXPRESS;Initial Catalog=DBWebshop;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.ProductPrice)
                    .HasColumnName("productPrice")
                    .HasMaxLength(10);

                entity.Property(e => e.Productname)
                    .IsRequired()
                    .HasColumnName("productname")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("totalAmount")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Cart");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceId).HasColumnName("invoiceId");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnName("invoiceDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.InvoiceDetails)
                    .IsRequired()
                    .HasColumnName("invoiceDetails")
                    .HasMaxLength(100);

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.PaymentId).HasColumnName("paymentId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Order");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Payment");
            });

            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Subscribed).HasColumnName("subscribed");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Newsletter)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Newsletter_User");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.AmountToPay)
                    .HasColumnName("amountToPay")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DateCompleted)
                    .HasColumnName("dateCompleted")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("paymentId");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnName("paymentAmount")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("paymentDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentInfomrationId).HasColumnName("paymentInfomrationId");

                entity.HasOne(d => d.PaymentInfomration)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.PaymentInfomrationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_PaymentInformation");
            });

            modelBuilder.Entity<PaymentInformation>(entity =>
            {
                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasColumnName("cardNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.CardOwnerName)
                    .IsRequired()
                    .HasColumnName("cardOwnerName")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PaymentInformation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentInformation_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleDescription)
                    .HasColumnName("roleDescription")
                    .HasMaxLength(100);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("roleName")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.Property(e => e.ShipmentId).HasColumnName("shipmentId");

                entity.Property(e => e.InvoiceNumber).HasColumnName("invoiceNumber");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.ShipmentDate)
                    .HasColumnName("shipmentDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ShipmentTrackingNumber).HasColumnName("shipmentTrackingNumber");

                entity.HasOne(d => d.InvoiceNumberNavigation)
                    .WithMany(p => p.Shipment)
                    .HasForeignKey(d => d.InvoiceNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipment_Invoices");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Shipment)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipment_Order");
            });

            modelBuilder.Entity<ShipmentItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Cityname)
                    .IsRequired()
                    .HasColumnName("cityname")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Streetname)
                    .IsRequired()
                    .HasColumnName("streetname")
                    .HasMaxLength(70);

                entity.Property(e => e.UserZipcode).HasColumnName("userZipcode");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role_User");
            });

            modelBuilder.Entity<ZipCode>(entity =>
            {
                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(255);

                entity.Property(e => e.ZipCode1).HasColumnName("zipCode");
            });
        }

        public DbSet<DatabaseCORE.Models.ProductItem> ProductItem { get; set; }
    }
}
