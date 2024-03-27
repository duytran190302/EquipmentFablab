using AutoMapper;
using Fablab.Models.Domain;
using Fablab.Models.DTO;
using Fablab.Models.DTO.EquipmentTypeDTO;
using Fablab.Models.DTO.PictureDTO;
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
		private readonly IPictureRepository _pictureRepository;
		private readonly ISpecificationRepository _specificationRepository;
		private readonly IUnitOfWork _unitOfWork;
		public EquipmentTypeController(IEquipmentTypeRepository equipmentTypeRepository, 
			IMapper mapper, 
			IPictureRepository pictureRepository,
			ISpecificationRepository specificationRepository,
			IUnitOfWork unitOfWork) 
		{
			_equipmentTypeRepository = equipmentTypeRepository;
			_mapper = mapper;
			_pictureRepository = pictureRepository;
			_specificationRepository = specificationRepository;
			_unitOfWork = unitOfWork;
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
					var equipmentTypesListDTO = _mapper.Map<List<EquipmentType>>(equipmentTypesList);
					return Ok(equipmentTypesListDTO);
				}
			}
			catch
			{
				return NotFound();
			}

		}

		[HttpGet("Search")]
		public async Task<IActionResult> SearchEquipmentTypes(

			[FromQuery] string? equipmentTypeId,
			[FromQuery] string? equipmentTypeName,
			[FromQuery] Category? category,
			[FromQuery] List<string>? tagId,


			int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<EquipmentType> equipmentList;
				//var equipmentListFromBorrow = new List<EquipmentType>();
				equipmentList = await _equipmentTypeRepository.SearchEquipmenTypeAsync();

				if (!string.IsNullOrEmpty(equipmentTypeId))
				{ equipmentList = equipmentList.Where(e => e.EquipmentTypeId.ToLower().Contains(equipmentTypeId.ToLower())).ToList(); }


				if (!string.IsNullOrEmpty(equipmentTypeName))
				{ equipmentList = equipmentList.Where(e => e.EquipmentTypeName.ToLower().Contains(equipmentTypeName.ToLower())).ToList(); }

				if (category != null)
				{ equipmentList = equipmentList.Where(e => e.Category == category).ToList(); }


				//var Tag2 = equipmentList.Where(x => equipmentList.Contains(tagId)).ToList();
				if (tagId != null)
				{ equipmentList = equipmentList.Where(e => e.Tags.Any(tagId => e.Tags.Contains(tagId))); }




				equipmentList = equipmentList.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
				//var equipmentListDTO = _mapper.Map<List<EquipmentDTO>>(equipmentList);
				if (equipmentList != null)
				{
					return Ok(equipmentList);
				}
				
				else return NotFound();

			}
			catch
			{
				return NotFound();
			}

		}

		[HttpGet("Information")]
		public async Task<IActionResult> InfEquipmentTypes([FromQuery] string equipmentTypeId)
		{
			try
			{

				if (!string.IsNullOrEmpty(equipmentTypeId))
				{
					var Inf = await _equipmentTypeRepository.InfEquipmentTypeAsync(equipmentTypeId);
					return Ok(Inf);
				}
				else
				{
					return NotFound();
				}
			}
			catch
			{
				return BadRequest();
			}

		}


		[HttpPost]
		public async Task<IActionResult> PostEquipmentType([FromBody] EquipmentType_Post equipmentTypeDTO  )
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

				//EquipmentType equipmentType = _mapper.Map<EquipmentType>(equipmentTypeDTO);
				var a = await _equipmentTypeRepository.PostEquipmentTypeAsync(equipmentTypeDTO);
				return Ok(a);
			}
			catch
			{ return BadRequest(); }
		}

		[HttpPost("Detail")]
		public async Task<IActionResult> PostEquipmentTypeDetail([FromBody] EquipmentTypePostAll equipmentTypeDTO)
		{
			try
			{
				if (equipmentTypeDTO == null)
				{
					return BadRequest("null");
				}
				if (await _equipmentTypeRepository.GetAsync(e => e.EquipmentTypeId.ToLower() == equipmentTypeDTO.EquipmentTypeId.ToLower()) != null)
				{
					return BadRequest("trung ten equipmenttype");
				}

				var a = new EquipmentType_Post()
				{
					EquipmentTypeId= equipmentTypeDTO.EquipmentTypeId,
					EquipmentTypeName= equipmentTypeDTO.EquipmentTypeName,
					Category=equipmentTypeDTO.Category,
					Description=equipmentTypeDTO.Description,
					Tags=equipmentTypeDTO.Tags,
				};

				var b = await _equipmentTypeRepository.PostEquipmentTypeAsync(a);

				foreach (var item in equipmentTypeDTO.Pictures)
				{
					byte[] FileData_Byte = Convert.FromBase64String(item.FileData);
					var picture = new Picture()
					{
						PictureId = Guid.NewGuid(),
						EquipmentTypeId = equipmentTypeDTO.EquipmentTypeId,
						FileData = FileData_Byte,
					};
					 _unitOfWork.pictureRepos.Add(picture);
					//await _pictureRepository.CreateNotracking(picture);
				}
				var result= await _unitOfWork.CompleteAsync();

				foreach (var item in equipmentTypeDTO.Specifications)
				{
					var spec = new Specification()
					{
						EquipmentTypeId = equipmentTypeDTO.EquipmentTypeId,
						Name = item.Name,
						Value	= item.Value,
						Unit = item.Unit,
					};
					await _specificationRepository.CreateAsync(spec);
				}

				//var a = await _equipmentTypeRepository.PostEquipmentTypeAsync(equipmentTypeDTO);
				return Ok(b);
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
		public async Task<ActionResult> PutEquipmentType([FromBody] EquipmentTypeDTO2 equipmentTypeDTO)
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
