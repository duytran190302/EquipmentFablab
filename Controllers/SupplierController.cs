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
	public class SupplierController : ControllerBase
	{
		private readonly ISupplierRepository _supplierRepository;
		private readonly IMapper _mapper;

		public SupplierController(ISupplierRepository supplierRepository, IMapper mapper) 
		{
			_supplierRepository=supplierRepository;
			_mapper = mapper;
		}

		[HttpGet("Locations")]
		public async Task<IActionResult> GetLocations([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Supplier> SupplierList;
				SupplierList = await _supplierRepository.GetAllAsync(pageSize: pageSize,
							pageNumber: pageNumber);
				if (!string.IsNullOrEmpty(search))
				{
					SupplierList = SupplierList.Where(e => e.SupplierName.Contains(search));
					var SupplierListDTO = _mapper.Map<List<Supplier>>(SupplierList);
					return Ok(SupplierListDTO);
				}
				else
				{
					var SupplierListDTO = _mapper.Map<List<Supplier>>(SupplierList);
					return Ok(SupplierListDTO);
				}
			}
			catch
			{
				return NotFound();
			}

		}

		[HttpPost]
		public async Task<IActionResult> AddLocation([FromBody] SupplierDTO supplierDTO)
		{
			try
			{
				if (supplierDTO == null)
				{
					return BadRequest();
				}
				if (await _supplierRepository.GetAsync(e => e.SupplierName.ToLower() == supplierDTO.SupplierName.ToLower()) != null)
				{
					return BadRequest("trung ten location");
				}

				Supplier supplier = _mapper.Map<Supplier>(supplierDTO);
				await _supplierRepository.UpdateAsync(supplier);
				return Ok(supplier);
			}
			catch
			{ return BadRequest(); }
		}



		[HttpDelete]
		public async Task<IActionResult> DeleteSupplier([FromQuery] string name)
		{
			if (name == null)
			{
				return BadRequest();
			}
			var supplier = await _supplierRepository.GetAsync(e => e.SupplierName == name);
			if (supplier == null)
			{
				return NotFound();
			}
			await _supplierRepository.RemoveAsync(supplier);
			return Ok(supplier);
		}


	}
}
