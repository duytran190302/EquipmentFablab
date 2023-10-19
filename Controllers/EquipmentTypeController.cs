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

		[HttpGet]
		public async Task<IActionResult> GetEquipmentTypes([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<EquipmentType> equipmentTypesList;
				equipmentTypesList = await _equipmentTypeRepository.GetAllAsync(pageSize: pageSize,
							pageNumber: pageNumber);
				if (!string.IsNullOrEmpty(search))
				{
					equipmentTypesList = equipmentTypesList.Where(e => e.EquipmentTypeId.Contains(search));
					var equipmentTypesListDTO = _mapper.Map<List<EquipmentType>>(equipmentTypesList);
					return Ok(equipmentTypesListDTO);
				}
				else
				{
					var equipmentTypesListDTO = _mapper.Map<List<EquipmentTypeDTO>>(equipmentTypesList);
					return Ok(equipmentTypesListDTO);
				}
			}
			catch
			{
				return NotFound();
			}

		}

		[HttpPost]
		public async Task<IActionResult> PostEquipmentType([FromBody] EquipmentTypeDTO equipmentTypeDTO  )
		{
			try
			{
				if (equipmentTypeDTO == null)
				{
					return BadRequest();
				}
				if (await _equipmentTypeRepository.GetAsync(e => e.EquipmentTypeId.ToLower() == equipmentTypeDTO.EquipmentTypeId.ToLower()) != null)
				{
					return BadRequest("trung ten equipmenttype");
				}

				EquipmentType equipmentType = _mapper.Map<EquipmentType>(equipmentTypeDTO);
				await _equipmentTypeRepository.CreateAsync(equipmentType);
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
			var equipmentType = await _equipmentTypeRepository.GetAsync(e => e.EquipmentTypeId == name);
			if (equipmentType == null)
			{
				return NotFound();
			}
			await _equipmentTypeRepository.RemoveAsync(equipmentType);
			return Ok(equipmentType);
		}

		[HttpPut]
		public async Task<ActionResult> PutEquipmentType([FromBody] EquipmentTypeDTO equipmentTypeDTO)
		{
			try
			{
				if (equipmentTypeDTO == null)
				{
					return BadRequest();
				}
				var equipmentType = await _equipmentTypeRepository.GetAsync(e => e.EquipmentTypeId == equipmentTypeDTO.EquipmentTypeId);
				if (equipmentType == null)
				{
					return NotFound();
				}

				EquipmentType equipmentType1 = _mapper.Map<EquipmentType>(equipmentTypeDTO);

				await _equipmentTypeRepository.UpdateAsync(equipmentType1);

				return Ok(equipmentType1);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

		}
	}
}
