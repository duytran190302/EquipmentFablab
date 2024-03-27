using AutoMapper;
using Fablab.Models.Domain;
using Fablab.Models.DTO;
using Fablab.Models.DTO.TagDTO;
using Fablab.Repository.Implementation;
using Fablab.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fablab.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TagController : ControllerBase
	{
		private readonly ITagRepository _tagRepository;
		private readonly IMapper _mapper;

		public TagController(ITagRepository tagRepository, IMapper mapper)
		{
			_tagRepository = tagRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetTags([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Tag> TagList;
				TagList = await _tagRepository.GetAllAsync(pageSize: pageSize,
							pageNumber: pageNumber);
				if (!string.IsNullOrEmpty(search))
				{
					TagList = TagList.Where(e => e.TagId.ToLower().Contains(search.ToLower()));
					var TagListDTO = _mapper.Map<List<TagDTO>>(TagList);
					return Ok(TagListDTO);
				}
				else
				{
					var TagListDTO = _mapper.Map<List<TagDTO>>(TagList);
					return Ok(TagListDTO);
				}
			}
			catch
			{
				return NotFound();
			}

		}

		[HttpPost]
		public async Task<IActionResult> PostTag([FromBody] TagDTO tagDTO)
		{
			try
			{
				if (tagDTO == null)
				{
					return BadRequest();
				}
				if (await _tagRepository.GetAsync(e => e.TagId == tagDTO.TagId) != null)
				{
					return BadRequest("trung ten tag");
				}

				Tag tag = _mapper.Map<Tag>(tagDTO);
				await _tagRepository.CreateAsync(tag);
				return Ok(tag);
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
			var tag = await _tagRepository.GetAsync(e => e.TagId == name);
			if (tag == null)
			{
				return NotFound();
			}
			await _tagRepository.RemoveAsync(tag);
			return Ok(tag);
		}

		[HttpPut]
		public async Task<ActionResult> PutTag([FromBody] TagDTO tagDTO)
		{
			try
			{
				if (tagDTO == null)
				{
					return BadRequest();
				}
				var supplier = await _tagRepository.GetAsync(e => e.TagId == tagDTO.TagId, tracked: false);
				if (supplier == null)
				{
					return NotFound();
				}

				Tag	 tag1 = _mapper.Map<Tag>(tagDTO);

				await _tagRepository.UpdateAsync(tag1);

				return Ok(tag1);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

		}
	}
}
