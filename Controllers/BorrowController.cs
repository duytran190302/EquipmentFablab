using AutoMapper;
using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO;
using Fablab.Repository.Implementation;
using Fablab.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace Fablab.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BorrowController : ControllerBase
	{
		private readonly IBorrowRepository _borrowRepository;
		private readonly IMapper _mapper;
		private readonly IEquipmentRepository _equipmentRepository;
		private readonly IProjectRepository _projectRepository;
		private readonly DataContext _dataContext;

		public BorrowController(IBorrowRepository borrowRepository, IMapper mapper,DataContext dataContext, IEquipmentRepository equipmentRepository, IProjectRepository projectRepository) 
		{
			_borrowRepository = borrowRepository;
			_mapper = mapper;
			_equipmentRepository = equipmentRepository;
			_projectRepository = projectRepository;
			_dataContext= dataContext;

		}

		[HttpGet("Search")]
		public async Task<IActionResult> SearchBorrow(
			[FromQuery] string? borrowId,
			[FromQuery] string? borrower,
			[FromQuery] int? borrowMonth,
			[FromQuery] bool? returned,
			[FromQuery] bool? onSide,


			[FromQuery] string? projectName,
			[FromQuery] string? equipment,

			int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Borrow> borrowList;
				var borrowListFromEquipment= new List<Borrow>();

				borrowList = await _borrowRepository.SearchBorrowAsync();

				if (!string.IsNullOrEmpty(borrowId))
				{ borrowList = borrowList.Where(e => e.BorrowId == borrowId); }
				if (!string.IsNullOrEmpty(borrower))
				{ borrowList = borrowList.Where(e => e.Borrower == borrower); }
				if (borrowMonth != null)
				{ borrowList = borrowList.Where(e => e.BorrowedDate.Month == borrowMonth); }
				if (returned != null)
				{ 
					if (returned== true) 
					{
						borrowList = borrowList.Where(e => e.RealReturnedDate != null);
					}
					else { borrowList = borrowList.Where(e => e.RealReturnedDate == null); }
				}
				if (onSide != null)
				{ borrowList = borrowList.Where(e => e.OnSide == onSide); }
				// tim theo ten du an
				if (!string.IsNullOrEmpty(projectName))
				{ borrowList = borrowList.Where(e => e.Project.ProjectName == projectName); }
				// tim theo ten equipment

				if (!string.IsNullOrEmpty(equipment))
				{
					foreach (var borrow in borrowList)
					{
						if (borrow.Equipments.FirstOrDefault(x => x.EquipmentId == equipment) != null)
						{
							borrowListFromEquipment.Add(borrow);
						}
					}
					borrowList= borrowListFromEquipment;
				}



				borrowList = borrowList.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
				var borrowListDTO = _mapper.Map<List<BorrowDTO>>(borrowList);
				return Ok(borrowListDTO);

			}
			catch
			{
				return NotFound();
			}

		}


		[HttpGet]
		public async Task<IActionResult> GetBorrows([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Borrow> borrowList;
				borrowList = await _borrowRepository.GetAllBorrowAsync(pageSize: pageSize,
							pageNumber: pageNumber);

				if (!string.IsNullOrEmpty(search))
				{
					borrowList = borrowList.Where(e => e.BorrowId.Contains(search));
					var borrowListDTO = _mapper.Map<List<BorrowDTO>>(borrowList);
					return Ok(borrowListDTO);
				}
				else
				{
					var borrowListDTO = _mapper.Map<List<GetBorrowDTO>>(borrowList);
					return Ok(borrowListDTO);
				}
			}
			catch
			{
				return NotFound();
			}

		}

		// Viết thêm get cac Equipment đẻ borrow : kiểm tra dự án duyệt chưa để gửi ds theiets bị thuộc dự án đó
		[HttpGet("Equipment")]
		public async Task<IActionResult> GetEquipmentForBorrows([FromQuery] string project, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				List<Equipment> equipments = await _borrowRepository.SearchEquipmentForBorrowAsync(project);
				equipments = equipments.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
				var equipmentListDTO = _mapper.Map<List<EquipmentDTO>>(equipments);
				return Ok(equipmentListDTO);

			}
			catch
			{
				return NotFound();
			}

		}


		[HttpPost("Borrow")]
		public async Task<IActionResult> PostBorrow([FromBody] PostBorrowDTO postBorrowDTO)
		{
			try
			{

				if (postBorrowDTO == null)
				{
					return BadRequest();
				}
				var existProject = await _projectRepository.GetAsync(e => e.ProjectName == postBorrowDTO.ProjectName,tracked: false);
				if (existProject.Approved == true)
				{
					//Borrow borrow = _mapper.Map<Borrow>(postBorrowDTO);

					
  
					 var borrow = await _borrowRepository.PostBorrowAsync(postBorrowDTO);
					await _borrowRepository.ChangeEquipmentOfBorrowAsync(borrow, true);

					if (borrow.Equipments != null) { return Ok(); }
					else { return NotFound(); }
					
				}
				return BadRequest("chua duoc phe duyet");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("Return")]
		public async Task<ActionResult> ReturnBorrow([FromBody] ReturnBorrowDTO returnBorrowDTO)
		{
			try
			{
				if (returnBorrowDTO.BorrowId == null)
				{
					return BadRequest();
				}
				var borrow = await _borrowRepository.GetBorrowByNameAsync(returnBorrowDTO.BorrowId);
				if (borrow == null)
				{
					return NotFound();
				}
				borrow.RealReturnedDate = returnBorrowDTO.RealReturnedDate;


				await _borrowRepository.UpdateAsync(borrow);

				await _borrowRepository.ChangeEquipmentOfBorrowAsync(borrow, false);

				return Ok(borrow.BorrowId);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}



		}
		[HttpPut]
		public async Task<ActionResult> PutBorrow([FromBody] PutBorrowDTO putBorrowDTO)
		{
			try
			{
				if (putBorrowDTO == null)
				{
					return BadRequest();
				}
				//var borrow = await _borrowRepository.GetAsync(e => e.BorrowId == putBorrowDTO.BorrowId);
				//if (borrow == null)
				//{
				//	return NotFound();
				//}

				Borrow borrow1 = _mapper.Map<Borrow>(putBorrowDTO);

				await _borrowRepository.UpdateAsync(borrow1);

				var borrow2 = await _borrowRepository.GetAsync(e => e.BorrowId == putBorrowDTO.BorrowId);
				var borrow3 = _mapper.Map<BorrowDTO>(borrow2);
				return Ok(borrow3);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}



		}
		[HttpDelete]
		public async Task<IActionResult> DeleteBorrow([FromQuery] string name)
		{
			if (name == null)
			{
				return BadRequest();
			}
			var borrow = await _borrowRepository.GetAsync(e => e.BorrowId == name);
			if (borrow == null)
			{
				return NotFound();
			}
			await _borrowRepository.RemoveAsync(borrow);
			return Ok(borrow);
		}




	}
}
