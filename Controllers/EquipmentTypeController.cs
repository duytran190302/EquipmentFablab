using AutoMapper;
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
	public class EquipmentTypeController : ControllerBase
	{
		private readonly IEquipmentTypeRepository _equipmentTypeRepository;
		private readonly IMapper _mapper;

		public EquipmentTypeController(IEquipmentTypeRepository equipmentTypeRepository, IMapper mapper) 
		{
			_equipmentTypeRepository = equipmentTypeRepository;
			_mapper = mapper;

		}

		[HttpGet()]
		public async Task<IActionResult> GetEquipmentType([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<EquipmentType> EquipmentTypeList;
				EquipmentTypeList = await _equipmentTypeRepository.GetAllAsync(pageSize: pageSize,
							pageNumber: pageNumber);
				if (!string.IsNullOrEmpty(search))
				{
					EquipmentTypeList = EquipmentTypeList.Where(e => e.Id.Contains(search));
					var EquipmentTypeListDTO = _mapper.Map<List<EquipmentTypeDTO>>(EquipmentTypeList);
					return Ok(EquipmentTypeListDTO);
				}
				else
				{
					var EquipmentTypeListDTO = _mapper.Map<List<EquipmentTypeDTO>>(EquipmentTypeList);
					return Ok(EquipmentTypeListDTO);
				}
			}
			catch
			{
				return NotFound();
			}

		}

		[HttpPost]
		public async Task<IActionResult> AddEquipmentType([FromBody] EquipmentTypeDTO equipmentTypeDTO)
		{
			try
			{
				if (equipmentTypeDTO == null)
				{
					return BadRequest();
				}
				if (await _equipmentTypeRepository.GetAsync(e => e.Id.ToLower() == equipmentTypeDTO.Id.ToLower()) != null)
				{
					return BadRequest("trung ten thiet bi");
				}

				EquipmentType equipmentType = _mapper.Map<EquipmentType>(equipmentTypeDTO);
				await _equipmentTypeRepository.UpdateAsync(equipmentType);
				return Ok(equipmentType);
			}
			catch
			{ return BadRequest(); }
		}


		[HttpDelete]
		public async Task<IActionResult> DeleteEquipmentType([FromQuery] string name)
		{
			if (name == null)
			{
				return BadRequest();
			}
			var equipmentType = await _equipmentTypeRepository.GetAsync(e => e.Id == name);
			if (equipmentType == null)
			{
				return NotFound();
			}
			await _equipmentTypeRepository.RemoveAsync(equipmentType);
			return Ok(equipmentType);
		}
	}
}
