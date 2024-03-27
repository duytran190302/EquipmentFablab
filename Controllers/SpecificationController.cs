using AutoMapper;
using Fablab.Models.Domain;
using Fablab.Models.DTO.SpecificationDTO;
using Fablab.Models.DTO.TagDTO;
using Fablab.Repository.Implementation;
using Fablab.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fablab.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SpecificationController : ControllerBase
	{
		private readonly ISpecificationRepository _specificationRepository;
		private readonly IMapper _mapper;

		public SpecificationController(ISpecificationRepository specificationRepository, IMapper mapper)
		{
			_specificationRepository = specificationRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetSpecfications([FromQuery] string equipmentTypeId)
		{
			try
			{
				IEnumerable<Specification> SpecificationList;
				SpecificationList = await _specificationRepository.GetSpecificationAsync(equipmentTypeId);
				if (SpecificationList==null)
				{

					return NotFound();
				}
				else
				{
					var SpecificationListDTO = _mapper.Map<List<SpecificationDTO>>(SpecificationList);
					return Ok(SpecificationListDTO);
				}
			}
			catch
			{
				return BadRequest();
			}

		}

		[HttpPost]
		public async Task<IActionResult> PostSpecification([FromBody] PostSpecificationDTO postSpecificationDTO)
		{
			try
			{
				//if (postSpecificationDTO == null)
				//{
				//	return BadRequest();
				//}
				//IEnumerable<Specification> SpecificationList;
				//SpecificationList = await _specificationRepository.GetSpecificationAsync(postSpecificationDTO.EquipmentTypeId);
				//foreach (var specification in SpecificationList)
				//{
				//	if(specification.Name==postSpecificationDTO.Name)
				//	{
				//		return BadRequest("trung spec");
				//	}	
				//}
				var postSpec = await _specificationRepository.PostSpecificationAsync(postSpecificationDTO);
				SpecificationDTO postSpecDTO= _mapper.Map<SpecificationDTO>(postSpec);


				return Ok(postSpecDTO);
			}
			catch
			{ return BadRequest(); }
		}



		[HttpDelete]
		public async Task<IActionResult> DeleteSpecification([FromQuery] string name, string equipmentTypeId)
		{
			if (name == null)
			{
				return BadRequest();
			}
			bool haveSpec = false;
			IEnumerable<Specification> SpecificationList;
			SpecificationList = await _specificationRepository.GetSpecificationAsync(equipmentTypeId);
			foreach (var specification in SpecificationList)
			{
				if (specification.Name == name)
				{
					haveSpec = true;
				}
			}
			if (haveSpec== false) 
			{
				return NotFound();
			}
			var Spec = await _specificationRepository.DeleteSpecificationAsync(name: name, equipmentTypeId: equipmentTypeId);
			SpecificationDTO SpecDTO = _mapper.Map<SpecificationDTO>(Spec);
			return Ok(SpecDTO);
		}

		[HttpPut]
		public async Task<ActionResult> PutSpecification([FromBody] PostSpecificationDTO SpecificationDTO)
		{
			try
			{
				if (SpecificationDTO == null)
				{
					return BadRequest();
				}
				var postSpec = await _specificationRepository.PutSpecificationAsync(SpecificationDTO);
				SpecificationDTO postSpecDTO = _mapper.Map<SpecificationDTO>(postSpec);


				return Ok(postSpecDTO);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

		}
	}
}
