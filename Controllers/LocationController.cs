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
	public class LocationController : ControllerBase
	{
		private readonly ILocationRepository _locationRepository;
		private readonly IMapper _mapper;
		public LocationController(ILocationRepository locationRepository, IMapper mapper)
		{
			_locationRepository = locationRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetLocations([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Location> LocationList;
				LocationList = await _locationRepository.GetAllAsync(pageSize: pageSize,
							pageNumber: pageNumber);
				if (!string.IsNullOrEmpty(search))
				{
					LocationList = LocationList.Where(e => e.LocationID.Contains(search));
					var LocationListDTO = _mapper.Map<List<LocationDTO>>(LocationList);
					return Ok(LocationListDTO);
				}
				else
				{
					var LocationListDTO = _mapper.Map<List<LocationDTO>>(LocationList);
					return Ok(LocationListDTO);
				}
			}
			catch
			{
				return NotFound();
			}

		}

		[HttpPost]
		public async Task<IActionResult> AddLocation([FromBody] LocationDTO locationDTO)
		{
			try
			{
				if (locationDTO == null)
				{
					return BadRequest();
				}
				if (await _locationRepository.GetAsync(e => e.LocationID.ToLower() == locationDTO.LocationID.ToLower()) != null)
				{
					return BadRequest("trung ten thiet bi");
				}

				Location location = _mapper.Map<Location>(locationDTO);
				await _locationRepository.UpdateAsync(location);
				return Ok(location);
			}
			catch
			{ return BadRequest(); }
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteLocation([FromQuery] string name)
		{
			if (name == null)
			{
				return BadRequest();
			}
			var location = await _locationRepository.GetAsync(e => e.LocationID == name);
			if (location == null)
			{
				return NotFound();
			}
			await _locationRepository.RemoveAsync(location);
			return Ok(location);
		}


	}



}
