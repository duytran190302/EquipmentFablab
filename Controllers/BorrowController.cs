using AutoMapper;
using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO;
using Fablab.Repository.Implementation;
using Fablab.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fablab.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BorrowController : ControllerBase
	{
		private readonly IBorrowRepository _borrowRepository;
		private readonly IMapper _mapper;
		private readonly DataContext _dataContext;

		public BorrowController(IBorrowRepository borrowRepository, IMapper mapper, DataContext dataContext) 
		{
			_borrowRepository = borrowRepository;
			_mapper = mapper;
			_dataContext= dataContext;
		}



		[HttpPost]
		public async Task<IActionResult> AddBorrow([FromBody] BorrowDTO borrowDTO)
		{
	

			if (borrowDTO == null)
			{
				return BadRequest();
			}
			if ( _dataContext.Project.Where(x=>x.ProjectName==borrowDTO.BorrowProject).FirstOrDefault().Approved == true)
			{
				Borrow borrow = _mapper.Map<Borrow>(borrowDTO);
				borrow.Project= _dataContext.Project.FirstOrDefault(x=> x.ProjectName==borrowDTO.BorrowProject);


				List<Equipment> equip = new List<Equipment>();

				foreach (var EquipBorrow in borrowDTO.BorrowEquipment)
				{
					if (_dataContext.Equipment.Where(x => x.EquipmentName == EquipBorrow.ToString()).FirstOrDefault().Status.ToString() == "Active")
					{
						var eq = _dataContext.Equipment.FirstOrDefault(x=>x.EquipmentName== EquipBorrow.ToString());
						equip.Add(eq);
					}
				}
				//borrow.equipmentBorrows. = equip;
				await _borrowRepository.CreateAsync(borrow);
				await _borrowRepository.UpdateAsync(borrow);
				return Ok(borrow);

			}	
				return BadRequest("chua duoc phe duyet");

		}




		}
}
