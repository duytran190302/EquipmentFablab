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

			IEnumerable<Equipment> EquipList;
			EquipList = await _equipmentRepository.GetAllAsync(pageSize: pageSize,
						pageNumber: pageNumber);
			if (!string.IsNullOrEmpty(search))
			{
				EquipList = EquipList.Where(e => e.EquipmentName.ToLower().Contains(search));
				var EquipListDTO = _mapper.Map<List<EquipmentDTO>>(EquipList);
				return Ok(EquipListDTO);
			}
			else
				return NotFound();

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
				equipmentDTO.Supplier.SupplierName = null;
			}
			if (_dataContext.Location.Any(s => s.LocationID == equipmentDTO.Location.LocationID))
			{
				equipmentDTO.Location.LocationID = null;
			}
			if (_dataContext.EquipmentTypes.Any(s => s.Id == equipmentDTO.EquipmentType.Id))
			{
				equipmentDTO.EquipmentType.Id = null;
			}


			Equipment equipment = _mapper.Map<Equipment>(equipmentDTO);
			equipment.EquipmentId = Guid.NewGuid();
			await _equipmentRepository.CreateAsync(equipment);

			equipment.Supplier = ES;
			equipment.EquipmentType = ET;
			equipment.Location = EL;
			await _equipmentRepository.UpdateAsync(equipment);

			return Ok(equipment);
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
		public async Task<IActionResult> UpdateEquipment([FromBody] EquipmentDTO equipmentDTO )
		{
			if(equipmentDTO == null)
			{
				return BadRequest();
			}
			Equipment equipment = _mapper.Map<Equipment>(equipmentDTO);
			await _equipmentRepository.UpdateAsync(equipment);
			return Ok(equipment);
		}

	}
}
