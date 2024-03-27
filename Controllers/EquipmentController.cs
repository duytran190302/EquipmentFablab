using AutoMapper;
using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO;
using Fablab.Repository.Implementation;
using Fablab.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Fablab.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EquipmentController : ControllerBase
	{
		private readonly DataContext _dataContext;

		private readonly ILocationRepository _locationRepository;
		private readonly IEquipmentTypeRepository _equipmentTypeRepository;
		private readonly ISupplierRepository _supplierRepository;
		private readonly IEquipmentRepository _equipmentRepository;
		private readonly IMapper _mapper;
		public EquipmentController(IEquipmentRepository equipmentRepository, IMapper mapper,
			ILocationRepository locationRepository,
			IEquipmentTypeRepository equipmentTypeRepository,
			ISupplierRepository supplierRepository, DataContext dataContext)
		{
			_equipmentRepository = equipmentRepository;
			_mapper = mapper;
		
			_locationRepository = locationRepository;
			_equipmentTypeRepository = equipmentTypeRepository;
			_supplierRepository = supplierRepository;
			_dataContext = dataContext;

		}

		[HttpGet]
		public async Task<IActionResult> GetEquipments([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Equipment> equipmentList;
				equipmentList= await _equipmentRepository.GetAllEquipmentAsync(pageSize: pageSize,
							pageNumber: pageNumber);

				if (!string.IsNullOrEmpty(search))
				{
					equipmentList = equipmentList.Where(e => e.EquipmentName.Contains(search));
					var equipmentListDTO = _mapper.Map<List<EquipmentDTO>>(equipmentList);
					return Ok(equipmentListDTO);
				}
				else
				{
					var equipmentListDTO = _mapper.Map<List<EquipmentDTO>>(equipmentList);
					return Ok(equipmentListDTO);
				}
			}
			catch
			{
				return NotFound();
			}

		}
		[HttpGet("Search")]
		public async Task<IActionResult> SearchEquipments(
			[FromQuery] string? equipmentId,
			[FromQuery] string? equipmentName,
			[FromQuery] string? yearOfSupply,
			[FromQuery] string? codeOfManager,
			[FromQuery] string? projectName,
			[FromQuery] string? equipmentTypeId,
			[FromQuery] string? equipmentTypeName,
			[FromQuery] Category? Category,

			[FromQuery] string? borrow,

			[FromQuery] EquipmentStatus? equipmentStatus,


			int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Equipment> equipmentList;
				var equipmentListFromBorrow= new List<Equipment>();
				equipmentList = await _equipmentRepository.SearchEquipmentAsync();

				if (!string.IsNullOrEmpty(equipmentId))
				{equipmentList = equipmentList.Where(e => e.EquipmentId==equipmentId);}
				if (!string.IsNullOrEmpty(equipmentName))
				{ equipmentList = equipmentList.Where(e=>e.EquipmentName==equipmentName); }
				if (yearOfSupply != null)
				{ equipmentList = equipmentList.Where(e => e.YearOfSupply.Year.ToString() == yearOfSupply); }
				if (!string.IsNullOrEmpty(codeOfManager))
				{ equipmentList = equipmentList.Where(e => e.CodeOfManager == codeOfManager); }
				if (!string.IsNullOrEmpty(equipmentTypeId))
				{ equipmentList = equipmentList.Where(e=>e.EquipmentType.EquipmentTypeId==equipmentTypeId); }
				if (!string.IsNullOrEmpty(equipmentTypeName))
				{ equipmentList = equipmentList.Where(e=>e.EquipmentType.EquipmentTypeName==equipmentTypeName); }
				if (equipmentStatus!= null)
				{ equipmentList = equipmentList.Where(e => e.Status == equipmentStatus); }
				if (Category != null)
				{ equipmentList = equipmentList.Where(e => e.EquipmentType.Category == Category); }


				if (!string.IsNullOrEmpty(borrow))
				{
					foreach (var equipment in equipmentList)
					{
						if (equipment.Borrows.FirstOrDefault(x => x.BorrowId == borrow) != null)
						{
							equipmentListFromBorrow.Add(equipment);
						}
					}
					equipmentList = equipmentListFromBorrow;
				}

				equipmentList =equipmentList.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
				var equipmentListDTO = _mapper.Map<List<EquipmentDTO>>(equipmentList);
				return Ok(equipmentListDTO);

			}
			catch
			{
				return NotFound();
			}

		}
		[HttpGet("Search1")]
		public async Task<IActionResult> SearchEquipment1s(
	[FromQuery] string? equipmentId,
	[FromQuery] string? equipmentName,
	[FromQuery] string? YearOfSupply,
	[FromQuery] string? CodeOfManager,

	[FromQuery] string? equipmentTypeId,
	[FromQuery] string? equipmentTypeName,
	[FromQuery] Category? Category,

	[FromQuery] string? borrow,

	[FromQuery] EquipmentStatus? equipmentStatus,


	int pageSize = 1000, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Equipment> equipmentList;
				var equipmentListFromBorrow = new List<Equipment>();
				equipmentList = await _equipmentRepository.SearchEquipmentAsync1();

				if (!string.IsNullOrEmpty(equipmentId))
				{ equipmentList = equipmentList.Where(e => e.EquipmentId.ToLower().Contains(equipmentTypeId.ToLower())); }
				if (!string.IsNullOrEmpty(equipmentName))
				{ equipmentList = equipmentList.Where(e => e.EquipmentName.ToLower().Contains(equipmentName.ToLower())); }
				if (YearOfSupply != null)
				{ equipmentList = equipmentList.Where(e => e.YearOfSupply.Year.ToString() == YearOfSupply); }
				if (!string.IsNullOrEmpty(CodeOfManager))
				{ equipmentList = equipmentList.Where(e => e.CodeOfManager == CodeOfManager); }
				if (!string.IsNullOrEmpty(equipmentTypeId))
				{ equipmentList = equipmentList.Where(e => e.EquipmentType.EquipmentTypeId == equipmentTypeId); }
				if (!string.IsNullOrEmpty(equipmentTypeName))
				{ equipmentList = equipmentList.Where(e => e.EquipmentType.EquipmentTypeName == equipmentTypeName); }
				if (equipmentStatus != null)
				{ equipmentList = equipmentList.Where(e => e.Status == equipmentStatus); }
				if (Category != null)
				{ equipmentList = equipmentList.Where(e => e.EquipmentType.Category == Category); }


				if (!string.IsNullOrEmpty(borrow))
				{
					foreach (var equipment in equipmentList)
					{
						if (equipment.Borrows.FirstOrDefault(x => x.BorrowId == borrow) != null)
						{
							equipmentListFromBorrow.Add(equipment);
						}
					}
					equipmentList = equipmentListFromBorrow;
				}

				equipmentList = equipmentList.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
				var equipmentListDTO = _mapper.Map<List<SearchEquipmentDTO>>(equipmentList);
				return Ok(equipmentListDTO);

			}
			catch
			{
				return NotFound();
			}

		}


		[HttpPost]
		public async Task<IActionResult> PostEquipment([FromBody] PostEquipmentDTO postEquipmentDTO)
		{
			try
			{
				if (postEquipmentDTO == null)
				{
					return BadRequest();
				}
				if (await _equipmentRepository.GetAsync(e => e.EquipmentId.ToLower() == postEquipmentDTO.EquipmentId.ToLower()) != null)
				{
					return BadRequest("trung id equipment");
				}


				var equipment =await _equipmentRepository.PostEquipmentAsync(postEquipmentDTO);

				return Ok(equipment);
			}
			catch (Exception ex)
			{ return BadRequest(ex); }
		}



		[HttpDelete]
		public async Task<IActionResult> DeleteEquipment([FromQuery] string name)
		{
			if (name == null)
			{
				return BadRequest();
			}
			var equipment = await _equipmentRepository.GetAsync(e => e.EquipmentId == name);
			if (equipment == null)
			{
				return NotFound();
			}
			await _equipmentRepository.RemoveAsync(equipment);
			return Ok(equipment);
		}

		[HttpPut]
		public async Task<ActionResult> PutEquipment([FromBody] PostEquipmentDTO equipmentDTO)
		{
			try
			{
				if (equipmentDTO == null)
				{
					return BadRequest();
				}
				var equipment = await _equipmentRepository.GetAsync(e => e.EquipmentId == equipmentDTO.EquipmentId, tracked: false);
				if (equipment == null)
				{
					return NotFound();
				}

				//Equipment equipment1 = _mapper.Map<Equipment>(equipmentDTO);

				var a = await _equipmentRepository.UpdateAsync(equipmentDTO);
				Equipment equipment1 = _mapper.Map<Equipment>(a);

				return Ok(equipment1);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}



		}

	}
}
