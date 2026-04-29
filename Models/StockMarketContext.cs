using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StockMarketAPI.Models;

public partial class StockMarketContext : DbContext
{
    public StockMarketContext()
    {
    }

    public StockMarketContext(DbContextOptions<StockMarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Port=3306;Database=StockMarket;User=root;Password=123456;",
                ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=StockMarket;User=root;Password=123456;")
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.Code).IsUnique();

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Exchange).HasMaxLength(50);
            entity.Property(e => e.Favorite).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PreviousPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.Username).IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
