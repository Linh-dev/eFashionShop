﻿using eFashionShop.Data.Entities;
using eFashionShop.Data.Enums;
using eFashionShop.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eFashionShop.Data.EF
{
    public class EShopDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public EShopDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AppConfig>().HasKey(x => x.Key);
            modelBuilder.Entity<AppConfig>().Property(x => x.Value).IsRequired(true);

            modelBuilder.Entity<Cart>().HasIndex(x => x.Id);
            modelBuilder.Entity<Cart>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Cart>().HasOne(x => x.Product).WithMany(x => x.Carts).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Cart>().HasOne(x => x.AppUser).WithMany(x => x.Carts).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Category>().Property(x => x.Status).HasDefaultValue(Status.Active);

            modelBuilder.Entity<Contact>().HasKey(x => x.Id);

            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().HasOne(x => x.AppUser).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<OrderDetail>().HasKey(x => new {x.OrderId, x.ProductId });
            modelBuilder.Entity<OrderDetail>().HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);
            modelBuilder.Entity<OrderDetail>().HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<Product>().HasKey(x => x.Id);

            modelBuilder.Entity<ProductImage>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductImage>().HasOne(x => x.Product).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<ProductInCategory>().HasKey(t => new { t.CategoryId, t.ProductId });
            modelBuilder.Entity<ProductInCategory>().HasOne(t => t.Product).WithMany(pc => pc.ProductInCategories)
                .HasForeignKey(pc => pc.ProductId);
            modelBuilder.Entity<ProductInCategory>().HasOne(t => t.Category).WithMany(pc => pc.ProductInCategories)
              .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<Promotion>().HasKey(x => x.Id); 

            modelBuilder.Entity<Slide>().HasKey(x => x.Id);

            modelBuilder.Entity<Transaction>().HasKey(x => x.Id);
            modelBuilder.Entity<Transaction>().HasOne(x => x.AppUser).WithMany(x => x.Transactions).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<AppRole>().ToTable("AppRole");
            modelBuilder.Entity<AppRole>().Property(x => x.Description).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<AppUser>().ToTable("AppUser");
            modelBuilder.Entity<AppUser>().Property(x => x.FirstName).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<AppUser>().Property(x => x.LastName).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<AppUser>().Property(x => x.Dob).IsRequired();

            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            modelBuilder.Seed();
            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Slide> Slides { get; set; }
    }
}
