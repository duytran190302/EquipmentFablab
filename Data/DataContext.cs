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
		public DbSet <EquipmentBorrow> EquipmentBorrows { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Xác định các thuộc tính làm khóa phức
			modelBuilder.Entity<Supplier>().HasKey(p => p.SupplierName);
			modelBuilder.Entity<EquipmentType>().HasKey(e => e.Id);
			modelBuilder.Entity<Location>().HasKey(e=> e.LocationID);
			modelBuilder.Entity<Project>().HasKey(p => p.ProjectName);


			modelBuilder.Entity<Equipment>().HasKey(e => e.EquipmentId);
			modelBuilder.Entity<Borrow>().HasKey(p => p.BorrowID);

			modelBuilder.Entity<Borrow>()
			.HasMany(s => s.Equipments)
			.WithMany(c => c.Borrows)
			.UsingEntity<EquipmentBorrow>(
				j => j
					.HasOne(e => e.Equipment)
					.WithMany()
					.HasForeignKey(e => e.EquipmentId),
				j => j
					.HasOne(e => e.Borrow)
					.WithMany()
					.HasForeignKey(e => e.BorrowID)
			);


		}
	}
}
