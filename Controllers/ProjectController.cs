using AutoMapper;
using Fablab.Data;
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
	public class ProjectController : ControllerBase
	{
		private readonly IProjectRepository _projectRepository;
		private readonly IMapper _mapper;
		private readonly DataContext _dataContext;

		public ProjectController(IProjectRepository projectRepository, IMapper mapper, DataContext dataContext)
		{
			_projectRepository = projectRepository;
			_mapper = mapper;
			_dataContext = dataContext;
		}


		[HttpGet]
		public async Task<IActionResult> GetProjectByName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return BadRequest();
			}


			var projects = await _projectRepository.GetProjectByNameAsync(name);
			var equipmentDTOs = _mapper.Map<List<ProjectDTO>>(projects);
			if (equipmentDTOs == null)
			{
				return NotFound();
			}
			return Ok(equipmentDTOs);
		}

		[HttpGet("AllProject")]
		public async Task<IActionResult> GetAllProjects([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Project> ProjectList;
				ProjectList = await _projectRepository.GetAllProjectAsync(pageSize: pageSize,
							pageNumber: pageNumber);
				if (!string.IsNullOrEmpty(search))
				{
					ProjectList = ProjectList.Where(e => e.ProjectName.Contains(search));
					var ProjectListDTO = _mapper.Map<List<EquipmentDTO>>(ProjectList);
					return Ok(ProjectListDTO);
				}
				else
				{
					var ProjectListDTO = _mapper.Map<List<ProjectDTO>>(ProjectList);
					return Ok(ProjectListDTO);
				}
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProject([FromQuery] string name)
		{
			if (name == null)
			{
				return BadRequest();
			}
			var equipment = await _projectRepository.GetAsync(e => e.ProjectName == name);
			if (equipment == null)
			{
				return NotFound();
			}
			await _projectRepository.RemoveAsync(equipment);
			return Ok(equipment);
		}


		[HttpPut]
		public async Task<IActionResult> UpdateProject(string name, [FromBody] UpdateProject updateProject)
		{
			try
			{
				if (updateProject == null || name != updateProject.ProjectName)
				{
					return BadRequest();
				}
				var project = _dataContext.Project.FirstOrDefault(x => x.ProjectName == name);
				project.StartDate = updateProject.StartDate;
				project.EndDate = updateProject.EndDate;
				project.Approved = updateProject.Approved;
				project.Description = updateProject.Description;

				await _projectRepository.UpdateAsync(project);
				return Ok(project);
			}
			catch
			{
				return BadRequest();
			}

		}

		[HttpPost]
		public async Task<IActionResult> AddProject([FromBody] AddProjectDTO addProjectDTO)
		{
			if (await _projectRepository.GetAsync(e => e.ProjectName.ToLower() == addProjectDTO.ProjectName.ToLower()) != null)
			{
				return BadRequest("trung ten du an");
			}
			if (addProjectDTO == null)
			{
				return BadRequest(addProjectDTO);
			}

			Project project = _mapper.Map<Project>(addProjectDTO);
			project.Approved = false;
			await _projectRepository.CreateAsync(project);
			return Ok(project);
		}
	}
}