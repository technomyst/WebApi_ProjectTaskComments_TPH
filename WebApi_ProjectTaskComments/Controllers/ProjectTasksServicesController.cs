using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_ProjectTaskComments.Models;
using WebApi_ProjectTaskComments.Models.Dto.ProjectTasks;
using WebApi_ProjectTaskComments.Services.Interfaces;

namespace WebApi_ProjectTaskComments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksServicesController : ControllerBase
    {
        private readonly IUniversalService<ProjectTask> _projectTaskService;
        private readonly IMapper _mapper;

        public ProjectTasksServicesController(IUniversalService<ProjectTask> projectTaskService, IMapper mapper)
        {
            _projectTaskService = projectTaskService;
            _mapper = mapper;
        }

        // GET: api/ProjectTasks
        [HttpGet("Project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetProjectTasks(int projectId)
        {
            //var projectTasks= await Task.Run(() => _projectTaskService.GetAllByParentItemId(projectId)); 
            //var projectTasks =await GetProjectTasksFromServiceAsync(projectId);
            var t = new Task<IEnumerable<ProjectTask>>(() => _projectTaskService.GetAllByParentItemId(projectId));
            var projectTasks = await t;
            
            if (projectTasks == null)
            {
                return NotFound();
            }
            return Ok(projectTasks);
        }

        //private Task<IEnumerable<ProjectTask>> GetProjectTasksFromServiceAsync(int projectId)
        //{
        //    var t =new Task<IEnumerable<ProjectTask>>(()=> _projectTaskService.GetAllByParentItemId(projectId));
        //    return t;

        //}

        // GET: api/ProjectTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetProjectTask(int id)
        {

            Task<ProjectTask> projectTaskTask = new Task<ProjectTask>(() => _projectTaskService.GetById(id));
            var projectTask = await projectTaskTask;

            if (projectTask == null)
            {
                return NotFound();
            }

            return projectTask;
        }

        // PUT: api/ProjectTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTask(int id, ProjectTask projectTask)
        {
            if (id != projectTask.Id)
            {
                return BadRequest();
            }

            Task<ProjectTask?> projectTaskTask = new Task<ProjectTask?>(() => _projectTaskService.Update(projectTask));
            var projectTaskUpdated = await projectTaskTask;

            if (projectTaskUpdated == null)
            {
                return Problem();
            }
            return NoContent();

            //_context.Entry(projectTask).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ProjectTaskExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/ProjectTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectTask?>> PostProjectTask(CreateProjectTaskDto projectTaskDto)
        {
            

          //if (_context.ProjectTasks == null)
          //{
          //    return Problem("Entity set 'AppDbContext.ProjectTasks'  is null.");
          //}

            var projectTask=_mapper.Map<ProjectTask>(projectTaskDto);

            _projectTaskService.Create(projectTask);

            //_context.ProjectTasks.Add(projectTask);
            //await _context.SaveChangesAsync();

            Task<ProjectTask?> projectTaskTask = new Task<ProjectTask?>(() => _projectTaskService.Create(projectTask));
            var projectTaskCreated = await projectTaskTask;

            return CreatedAtAction("GetProjectTask", new { id = projectTask.Id }, projectTask);
        }

        // DELETE: api/ProjectTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTask(int id)
        {
            
            Task<ProjectTask> projectTaskTaskFind = new Task<ProjectTask>(() => _projectTaskService.GetById(id));
            var projectTask = await projectTaskTaskFind;

            if (projectTask == null)
            {
                return NotFound();
            }

           

            Task<bool> projectTaskTaskDelete = new Task<bool>(() => _projectTaskService.Delete(projectTask));
            var result = await projectTaskTaskDelete;

            
            if (result)
            { 
                return NoContent(); 
            }

            return Problem();

            //if (_context.ProjectTasks == null)
            //{
            //    return NotFound();
            //}
            //var projectTask = await _context.ProjectTasks.FindAsync(id);
            //if (projectTask == null)
            //{
            //    return NotFound();
            //}
            //_context.ProjectTasks.Remove(projectTask);
            //projectTask.IsDeleted = true;
            //await _context.SaveChangesAsync();
        }

        //private bool ProjectTaskExists(int id)
        //{
        //    return (_context.ProjectTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
