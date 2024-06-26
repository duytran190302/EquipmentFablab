﻿using AutoMapper;
using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO;
using Fablab.Models.DTO.ProjectFolder;
using Fablab.Repository.Implementation;
using Fablab.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;

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


		[HttpGet("Search")]
		public async Task<IActionResult> SearchProject(
			[FromQuery] string? projectName,
			[FromQuery] DateTime? date,
			[FromQuery] bool? approved,

			[FromQuery] string? borrowId,

			int pageSize = 0, int pageNumber = 1)
		{
			try
			{
				IEnumerable<Project> projectList;
				projectList = await _projectRepository.SearchProjectAsync();

				if (!string.IsNullOrEmpty(projectName))
				{ projectList = projectList.Where(e => e.ProjectName == projectName); }
				if (date!= null)
				{ projectList = projectList.Where(e => e.StartDate <= date).Where(e=> e.EndDate >= date); }
				if (approved != null)
				{ projectList = projectList.Where(e => e.Approved == approved); }
				
				// tim borrow thuoc project nao
				if (!string.IsNullOrEmpty(borrowId))
				{ 
					foreach (var project in projectList)
					{
						if (project.Borrows.FirstOrDefault(x=>x.BorrowId==borrowId)!=null)
						{
							projectList = new List<Project>() { project };
							break;
						}	


						//var a = project.Borrows.Where(x=>x.BorrowId == borrowId);
						//if (a!=null) 
						//{
						//	projectList = projectList.Where(x => x.Borrows == a);
						//	break;
						//}
					}
				}
				projectList = projectList.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

				foreach (var project in projectList)
				{
					foreach ( var i in project.Equipments)
					{
						i.Borrows = null;

						i.EquipmentType = null;
					}
					foreach (var i in project.Borrows)
					{
						i.Project = null;
						i.Equipments = null;
						 
					}
				}
				return Ok(projectList);
				//var projectListDTO = _mapper.Map<List<SearchProjectDTO>>(projectList);


			}
			catch
			{
				return NotFound();
			}

		}
		[HttpGet("Equipment")]
		public async Task<IActionResult> GetSuppliers([FromQuery] string projectName)
		{
			try
			{
				IEnumerable<Equipment> equipments;
				equipments = await _projectRepository.SearchEquipmentAsync(projectName);
				var equipmentListDTO = _mapper.Map<List<EquipmentDTO>>(equipments);
				return Ok(equipmentListDTO);

			}
			catch
			{
				return NotFound();
			}

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
					var ProjectListDTO = _mapper.Map<List<ProjectDTO>>(ProjectList);
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
		public async Task<IActionResult> UpdateProject( [FromBody] UpdateProject updateProject)
		{
			try
			{
				if (await _projectRepository.GetAsync(e => e.ProjectName.ToLower() == updateProject.ProjectName.ToLower()) == null)
				{
					return BadRequest("khong co du an");
				}
				if (updateProject == null )
				{
					return BadRequest();
				}
				var project = _dataContext.Project.FirstOrDefault(x => x.ProjectName == updateProject.ProjectName);
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
		public async Task<IActionResult> PostProject([FromBody] PostProjectDTO postProjectDTO)
		{
			if (await _projectRepository.GetAsync(e => e.ProjectName.ToLower() == postProjectDTO.ProjectName.ToLower()) != null)
			{
				return BadRequest("trung ten du an");
			}
			if (postProjectDTO == null)
			{
				return BadRequest(postProjectDTO);
			}

			Project project = _mapper.Map<Project>(postProjectDTO);
			project.Approved = false;
			await _projectRepository.CreateAsync(project);
			return Ok(project);
		}

		[HttpPost("Init")]
		public async Task<IActionResult> PostProjectWithEquipment([FromBody] PostProjectDTO2 postProjectDTO)
		{
			if (await _projectRepository.GetAsync(e => e.ProjectName.ToLower() == postProjectDTO.ProjectName.ToLower()) != null)
			{
				return BadRequest("trung ten du an");
			}
			if (postProjectDTO == null)
			{
				return BadRequest(postProjectDTO);
			}

			await _projectRepository.PostProjectAsync(postProjectDTO);
			return Ok(postProjectDTO);
		}

		[HttpPut("Approve")]
		public async Task<IActionResult> ApproveProject([FromBody] ApproveProject approveProject)
		{
			if (await _projectRepository.GetAsync(e => e.ProjectName.ToLower() == approveProject.ProjectName.ToLower()) == null)
			{
				return BadRequest("khong co du an");
			}
			if (approveProject == null)
			{
				return BadRequest(approveProject);
			}

			var project = _dataContext.Project.FirstOrDefault(x => x.ProjectName == approveProject.ProjectName);
			project.Approved = approveProject.Approved;
			await _projectRepository.UpdateAsync(project);
			return Ok(project);
		}

		[HttpPut("End")]
		public async Task<IActionResult> EndProject([FromBody] EndProjectDTO endProject)
		{
			if (await _projectRepository.GetAsync(e => e.ProjectName.ToLower() == endProject.ProjectName.ToLower()) == null)
			{
				return BadRequest("khong co du an");
			}
			if (endProject == null)
			{
				return BadRequest(endProject);
			}

			var project = _dataContext.Project.FirstOrDefault(x => x.ProjectName == endProject.ProjectName);
			project.RealEndDate = endProject.RealEndDate;
			project.Equipments = null;
			await _projectRepository.UpdateAsync(project);
			return Ok(project);
		}

	}
}