using Fablab.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fablab.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options) { }
		public DbSet<EquipmentType> EquipmentTypes { get; set; }
		public DbSet<Location> Location { get; set; }
		public DbSet<Borrow> Borrow { get; set; }
		public DbSet<Equipment> Equipment { get; set; }
		public DbSet<Project> Project { get; set; }
		public DbSet <Supplier> Suppliers { get; set; }
		//public DbSet <EquipmentBorrow> EquipmentBorrows { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Xác định các thuộc tính làm key
			modelBuilder.Entity<Supplier>().HasKey(p => p.SupplierName);
			modelBuilder.Entity<EquipmentType>().HasKey(e => e.EquipmentTypeId);
			modelBuilder.Entity<Location>().HasKey(p=>p.LocationId);
			modelBuilder.Entity<Project>().HasKey(p => p.ProjectName);


			modelBuilder.Entity<Equipment>().HasKey(e => e.EquipmentId);
			modelBuilder.Entity<Borrow>().HasKey(p => p.BorrowId);
			modelBuilder.Entity<Borrow>().HasMany<Equipment>(b => b.Equipments).WithMany(e => e.Borrows);



		}
	}
}
