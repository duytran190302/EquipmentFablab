using AutoMapper;
using Fablab.Models.Domain;
using Fablab.Models.DTO.EquipmentTypeDTO;
using Fablab.Models.DTO.PictureDTO;
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
	public class PictureController : ControllerBase
	{
		private readonly IPictureRepository _pictureRepository;
		private readonly IMapper _mapper;

		public PictureController(IPictureRepository pictureRepository, IMapper mapper)
		{
			_pictureRepository = pictureRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetPictures([FromQuery] string? equipmentTypeId)
		{
			try
			{
				IEnumerable<Picture> PictureList;
				PictureList = await _pictureRepository.GetPictureAsync(equipmentTypeId);
				if (PictureList == null)
				{

					return NotFound();
				}
				else
				{
					var picDTO = _mapper.Map<List<PictureDTO>>(PictureList);
					return Ok(picDTO);
				}
			}
			catch
			{
				return BadRequest();
			}

		}

		[HttpPost]
		public async Task<IActionResult> PostTag([FromBody] PictureDTO_Byte pictureDTO)
		{
			try
			{
				if (pictureDTO == null)
				{
					return BadRequest();
				}

				byte[] byteArray = Convert.FromBase64String(pictureDTO.FileData);
				//Picture picture = _mapper.Map<Picture>(pictureDTO);
				var picture = new Picture()
				{
					PictureId= Guid.NewGuid(),
					EquipmentTypeId = pictureDTO.EquipmentTypeId,
					FileData = byteArray,
				};
				await _pictureRepository.CreateAsync(picture);
				return Ok(picture);
			}
			catch
			{ return BadRequest(); }
		}



		[HttpDelete]
		public async Task<IActionResult> DeleteTag([FromQuery] string name)
		{
			if (name == null)
			{
				return BadRequest();
			}
			var picture = await _pictureRepository.GetAsync(e => e.EquipmentTypeId == name);
			if (picture == null)
			{
				return NotFound();
			}
			await _pictureRepository.RemoveAsync(picture);
			return Ok(picture);
		}

		[HttpPut]
		public async Task<ActionResult> PutTag([FromBody] PictureDTO pictureDTO)
		{
			try
			{
				if (pictureDTO == null)
				{
					return BadRequest();
				}
				var picture = await _pictureRepository.GetAsync(e => e.EquipmentTypeId == pictureDTO.EquipmentTypeId, tracked: false);
				if (picture == null)
				{
					return NotFound();
				}

				Picture picture1 = _mapper.Map<Picture>(pictureDTO);

				await _pictureRepository.UpdateAsync(picture1);

				return Ok(picture1);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

		}
	}
}
