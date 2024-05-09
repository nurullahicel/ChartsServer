   using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace ChartsServer.model;

public partial class SalesDbContext : DbContext
{
   IConfiguration _configuration;

    SqlConnection cnn = new SqlConnection("Server=.\\SQLEXPRESS;Database=SalesDB;TrustServerCertificate=True;User Id=sa;Password=Ni.0708610");
    SqlCommand cmd = new SqlCommand("SELECT * FROM Employees");
   


    public SalesDbContext()
    {
    }

    public SalesDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        cmd.Connection = cnn;  
    }
 


    public SalesDbContext(DbContextOptions<SalesDbContext> options)
        : base(options)
    {
        
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }
  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => 
        optionsBuilder.UseSqlServer(cnn);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
