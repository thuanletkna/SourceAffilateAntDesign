using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AffilateSource.Data.DataEntity.Entities;

namespace AffilateSource.Data.DataEntity
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // config những thuộc tính trong db mà chúng ta k cài đặt được ở trong class như key, maxlenght bao nhiêu...
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
            builder.Entity<User>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);

            builder.Entity<LabelInProduct>()
                        .HasKey(c => new { c.LabelId, c.ProductId });

            builder.Entity<Permission>()
                       .HasKey(c => new { c.RoleId, c.FunctionId, c.CommandId });

            builder.Entity<Vote>()
                        .HasKey(c => new { c.ProductId, c.UserId });


            builder.Entity<Feedback>()
                        .HasKey(c => new { c.ProductId, c.UserId });
            builder.Entity<OrderProduct>()
                        .HasKey(c => new { c.ProductId, c.TimeLineId });

            builder.Entity<CommandInFunction>()
                       .HasKey(c => new { c.CommandId, c.FunctionId });

            builder.HasSequence("ProductAffilatesSequences");

        }
        public DbSet<Command> Commands { set; get; }
        public DbSet<CommandInFunction> CommandInFunctions { set; get; }
        public DbSet<ActivityLog> ActivityLogs { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<Package> Packages { set; get; }
        public DbSet<OrderProduct> OrderProducts { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<DocStatus> DocStatus { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<Question> Questions { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Function> Functions { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<CategoryImage> CategoryImages { set; get; }
        public DbSet<PostImage> PostImages { set; get; }
        public DbSet<PostDetail> PostDetails { set; get; }
        public DbSet<Label> Labels { set; get; }
        public DbSet<LabelInProduct> LabelProducts { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<Report> Reports { set; get; }
        public DbSet<Vote> Votes { set; get; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Country> Country { set; get; }
        public DbSet<Province> Province { set; get; }
        public DbSet<District> District { set; get; }
        public DbSet<Ward> Ward { set; get; }
    }
}
