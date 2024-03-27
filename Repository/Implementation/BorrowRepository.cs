using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO;
using Fablab.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net.WebSockets;

namespace Fablab.Repository.Implementation
{
	public class BorrowRepository : Repository<Borrow>, IBorrowRepository
	{
		private readonly DataContext _db;
		public BorrowRepository(DataContext db) : base(db)
		{
			_db = db;
		}

		public async Task ChangeEquipmentOfBorrowAsync(Borrow entity, bool active)
		{
			foreach (var EquipBorrow in entity.Equipments.ToList())
			{
				if(active== true)
				{
					var equip = await _db.Equipment.FindAsync(EquipBorrow.EquipmentId);
					equip.Status= EquipmentStatus.Inactive;
					_db.Equipment.Update(equip);
				}
				else
				{
					var equip = await _db.Equipment.FindAsync(EquipBorrow.EquipmentId);
					equip.Status = EquipmentStatus.Active;
					_db.Equipment.Update(equip);
				}
			}
			await _db.SaveChangesAsync();

		}

		public async Task<List<Borrow>> GetAllBorrowAsync(int pageSize = 0, int pageNumber = 1)
		{
			if (pageSize > 0)
			{
				if (pageSize > 100)
				{
					pageSize = 100;
				}
				var query = await _db.Borrow.Include(x => x.Equipments)
					.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
				return query;
			}

			return null;
		}

		public async Task <Borrow> GetBorrowByNameAsync(string name)
		{
			var borrow =  _db.Borrow.Include(x => x.Equipments).Include(x => x.Project).FirstOrDefault();
			return borrow;
		}

		public async Task<Borrow> PostBorrowAsync(PostBorrowDTO entity)
		{
			// Thêm bước kiểm tra điều kiện thiết bị có thuọc dự án không: 


			foreach (var EquipBorrow in entity.Equipment)
			{
				var equip = _db.Equipment.Where(x => x.EquipmentId == EquipBorrow).FirstOrDefault();// tìm thiết bị ở database
				if (equip != null) 
				{
					if (equip.Status != EquipmentStatus.Active)
					{
						entity.Equipment.Remove(EquipBorrow);
					}
				}
			}

			var project_temp = _db.Project.Where(x=>x.ProjectName==entity.ProjectName).FirstOrDefault();
			List<string> strings = new List<string>();
			foreach (var a  in project_temp.Equipments)
			{
				strings.Add(a.EquipmentName);
			}

			entity.Equipment = entity.Equipment.Intersect(strings).ToList();


			var equipment = _db.Equipment.Where(x => entity.Equipment.Contains(x.EquipmentId)).ToList();// lấy tất cả thiết bị ở equipment postDTOcó id theo list 
			// Kiểm tra có thuộc dự án không, duyệt 2 vòng lặp hoặc thêm trường khóa phụ cho Project

			var borrow = new Borrow()
			{
				BorrowId = entity.BorrowId,
				BorrowedDate = entity.BorrowedDate,
				ReturnedDate = entity.ReturnedDate,
				Borrower = entity.Borrower,
				Reason = entity.Reason,
				OnSide = entity.OnSide,
				Project = _db.Project.FirstOrDefault(x => x.ProjectName == entity.ProjectName),
				///Equipments= new List<Equipment> ()
				Equipments = equipment
			};
			 /// _db.Attach(borrow); 
			//var dbBorrow = _db.Borrow.Include(x=>x.Equipments).First();
			await _db.Borrow.AddAsync(borrow);
			await _db.SaveChangesAsync();

			//var dbBorrow = _db.Borrow.Include(x => x.Equipments).FirstOrDefault(x=>x.BorrowId==entity.BorrowId);
			//  dbBorrow.Equipments.AddRange(equipment);
			//await _db.SaveChangesAsync();


			var borrow2 = _db.Borrow.Include(x => x.Equipments).Include(x => x.Project).FirstOrDefault(x => x.BorrowId == entity.BorrowId);
			return borrow2;



		}

		public async Task<List<Borrow>> SearchBorrowAsync()
		{
			var query = await _db.Borrow.Include(x => x.Project).Include(x=>x.Equipments).ToListAsync();

			return query;
		}

		public async Task<List<Equipment>> SearchEquipmentForBorrowAsync(string projectName)
		{
			var equipment =  _db.Project.Where(x=>x.ProjectName==projectName).FirstOrDefault();
			return equipment.Equipments.ToList();
		}

		public async Task<Borrow> UpdateAsync(Borrow entity)
		{
			_db.Borrow.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
