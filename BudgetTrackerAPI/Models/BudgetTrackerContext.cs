﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerAPI.Models;

public partial class BudgetTrackerContext : IdentityDbContext<ApplicationUser>
{
    public BudgetTrackerContext(DbContextOptions<BudgetTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<BudgetView> BudgetViews { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(e => e.BudgetId).HasName("PK_Budgets");

            entity.HasIndex(e => new { e.UserId, e.Name }, "UQ_Budgets_Name").IsUnique();

            entity.Property(e => e.BudgetId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("BudgetID");

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.BudgetedAmount).HasColumnType("decimal(10,2)");
            entity.Property(e => e.ActualAmount).HasColumnType("decimal(10,2)");

            entity.Property(e => e.Period)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User)
                .WithMany(u => u.Budgets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Budget_User");

            entity.HasOne(d => d.User)
              .WithMany(u => u.Budgets)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.Cascade)
              .HasConstraintName("FK_Budgets_AspNetUsers");
        });

        modelBuilder.Entity<BudgetView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("BudgetView");

            entity.Property(e => e.Actual).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.BudgetId).HasColumnName("BudgetID");
            entity.Property(e => e.Budgeted).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_Categories");

            entity.HasIndex(e => new { e.UserId, e.Name }, "UQ_Categories_Name").IsUnique();

            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("CategoryID");

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.Property(e => e.BudgetId)
                .IsRequired()
                .HasColumnName("BudgetID");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.BudgetedAmount).HasColumnType("decimal(10,2)");
            entity.Property(e => e.ActualAmount).HasColumnType("decimal(10,2)");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(c => c.Budget)
                .WithMany(b => b.Categories)
                .HasForeignKey(c => c.BudgetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Categories_Budgets");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.GoalId).HasName("PK_Goals");
            
            entity.HasIndex(e => new { e.UserId, e.Name }, "UQ_Goals_Name").IsUnique();

            entity.Property(e => e.GoalId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("GoalID");

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.TargetAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SavedAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");

            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK_Transactions");

            entity.Property(e => e.TransactionId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("TransactionID");

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}