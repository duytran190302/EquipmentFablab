using AutoMapper;
using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO;
using Fablab.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fablab.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EquipmentController : ControllerBase
	{
		private readonly DataContext _dataContext;
		private readonly IEquipmentRepository _equipmentRepository;
		private readonly IMapper _mapper;
		public EquipmentController(IEquipmentRepository equipmentRepository, IMapper mapper, DataContext dataContext)
		{
			_equipmentRepository = equipmentRepository;
			_mapper = mapper;
			_dataContext = dataContext;
		}

		[HttpGet("Equipments")]
		public async Task<IActionResult> GetEquipments([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Equipment> EquipList;
				EquipList = await _equipmentRepository.GetAllAsync(pageSize: pageSize,
							pageNumber: pageNumber);
				if (!string.IsNullOrEmpty(search))
				{
					EquipList = EquipList.Where(e => e.EquipmentName.Contains(search));
					var EquipListDTO = _mapper.Map<List<EquipmentDTO>>(EquipList);
					return Ok(EquipListDTO);
				}
				else
				{
					var EquipListDTO = _mapper.Map<List<EquipmentDTO>>(EquipList);
					return Ok(EquipListDTO);
				}
			}
				catch
			{
				return BadRequest();
			}

		}

		[HttpGet("AllEquipment")]
		public async Task<IActionResult> GetAllEquipments([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Equipment> EquipList;
				EquipList = await _equipmentRepository.GetAllEquipmentAsync(pageSize: pageSize,
							pageNumber: pageNumber);
				if (!string.IsNullOrEmpty(search))
				{
					EquipList = EquipList.Where(e => e.EquipmentName.Contains(search));
					var EquipListDTO = _mapper.Map<List<EquipmentDTO>>(EquipList);
					return Ok(EquipListDTO);
				}
				else
				{
					var EquipListDTO = _mapper.Map<List<EquipmentDTO>>(EquipList);
					return Ok(EquipListDTO);
				}
			}
			catch
			{
				return BadRequest();
			}

		}

		[HttpGet("Equipment")]
		public async Task<IActionResult> GetEquipment(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return BadRequest();
			}
			var Equip = await _equipmentRepository.GetAsync(e => e.EquipmentName == name);
			var EquipDTO = _mapper.Map<EquipmentDTO>(Equip);
			if (EquipDTO == null)
			{
				return NotFound();
			}
			return Ok(EquipDTO);
		}

		[HttpGet("EquipmentByName")]
		public async Task<IActionResult> GetEquipmentByName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return BadRequest();
			}


			var Equip = await _equipmentRepository.GetEquipmentByNameAsync(name);
			var EquipDTO = _mapper.Map<List<EquipmentDTO>>(Equip);
			if (EquipDTO == null)
			{
				return NotFound();
			}
			return Ok(EquipDTO);
		}


		[HttpPost]
		public async Task<IActionResult> AddEquipment([FromBody] EquipmentDTO equipmentDTO)
		{
			if (await _equipmentRepository.GetAsync(e => e.EquipmentName.ToLower() == equipmentDTO.EquipmentName.ToLower()) != null)
			{
				return BadRequest("trung ten thiet bi");
			}
			if (equipmentDTO == null)
			{
				return BadRequest(equipmentDTO);
			}

			// thêm xong update cái giá trị  S E L
			var ES = equipmentDTO.Supplier;
			var ET = equipmentDTO.EquipmentType;
			var EL = equipmentDTO.Location;
			if (_dataContext.Suppliers.Any(s => s.SupplierName == equipmentDTO.Supplier.SupplierName))
			{
				equipmentDTO.Supplier = null;
			}
			if (_dataContext.Location.Any(s => s.LocationID == equipmentDTO.Location.LocationID))
			{
				equipmentDTO.Location = null;
			}
			if (_dataContext.EquipmentTypes.Any(s => s.Id == equipmentDTO.EquipmentType.Id))
			{
				equipmentDTO.EquipmentType = null;
			}


			Equipment equipment = _mapper.Map<Equipment>(equipmentDTO);
			equipment.EquipmentId = Guid.NewGuid();
			await _equipmentRepository.CreateAsync(equipment);


			try
			{
				equipment.Supplier = _dataContext.Suppliers.FirstOrDefault(s=> s.SupplierName==ES.SupplierName);
				equipment.EquipmentType = _dataContext.EquipmentTypes.FirstOrDefault(s => s.Id == ET.Id);
				equipment.Location = _dataContext.Location.FirstOrDefault(s => s.LocationID == EL.LocationID);
				await _equipmentRepository.UpdateAsync(equipment);
				return Ok(equipment);
			}
			catch
			{
				return BadRequest();
			}

			
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteEquipment([FromQuery] string name)
		{
			if (name == null)
			{
				return BadRequest();
			}
			var equipment = await _equipmentRepository.GetAsync(e => e.EquipmentName == name);
			if (equipment == null)
			{
				return NotFound();
			}
			await _equipmentRepository.RemoveAsync(equipment);
			return Ok(equipment);
		}


		[HttpPut]
		public async Task<IActionResult> UpdateEquipment(string name,[FromBody] UpdateEquip equipmentDTO )
		{
			if(equipmentDTO == null || name != equipmentDTO.EquipmentName)
			{
				return BadRequest();
			}
			var equip = _dataContext.Equipment.FirstOrDefault(x=>x.EquipmentName== name);
			equip.YearOfSupply = equipmentDTO.YearOfSupply;
			equip.CodeOfManager = equipmentDTO.CodeOfManager;
			equip.Status= equipmentDTO.Status;
			await _equipmentRepository.UpdateAsync(equip);
			return Ok(equip);
		}

	}
}
