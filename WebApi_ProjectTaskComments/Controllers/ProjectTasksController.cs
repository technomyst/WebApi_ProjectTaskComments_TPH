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
    public class ProjectTasksController : ControllerBase
    {
        //private readonly AppDbContext _context;
        //private readonly IMapper _mapper;

        //public ProjectTasksController(AppDbContext context, IMapper mapper)
        //{
        //    _context = context;
        //    _mapper = mapper;
        //}

        private readonly IProjectTaskServiceAsync _projectTaskServiceAsync;
        public ProjectTasksController(IProjectTaskServiceAsync projectTaskServiceAsync)
        { 
            _projectTaskServiceAsync = projectTaskServiceAsync;
        }

        // GET: api/ProjectTasks
        [HttpGet("Project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectTaskDto>>> GetProjectTasks(int projectId)
        {
            //if (_context.ProjectTasks == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.ProjectTasks.Where(x=>x.ProjectId==projectId).ToListAsync();


            //return new ActionResult<IEnumerable<GrantProgram>>(await _grants.GetGrants());
            //return new(await _grants.GetGrants());
            //return Ok ( await _grants.GetGrants() ); 
            var projectTaskList =await _projectTaskServiceAsync.GetByProjectId(projectId);
            return Ok(projectTaskList);
        }

        // GET: api/ProjectTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTaskDto>> GetProjectTask(int id)
        {
          //if (_context.ProjectTasks == null)
          //{
          //    return NotFound();
          //}
          //  var projectTask = await _context.ProjectTasks.FindAsync(id);

          //  if (projectTask == null)
          //  {
          //      return NotFound();
          //  }

          //  return projectTask;

            var projectTaskDto=await _projectTaskServiceAsync.GetById(id);
            
            return Ok(projectTaskDto);

        }

        // PUT: api/ProjectTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTask(int id, ProjectTaskDto projectTaskDto)
        {
            if (id != projectTaskDto.Id)
            {
                return BadRequest();
            }

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

            await _projectTaskServiceAsync.Update(projectTaskDto);
            return NoContent();
        }

        // POST: api/ProjectTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectTaskDto>> PostProjectTask(CreateProjectTaskDto projectTaskDto)
        {
          //if (_context.ProjectTasks == null)
          //{
          //    return Problem("Entity set 'AppDbContext.ProjectTasks'  is null.");
          //}

          //  var projectTask=_mapper.Map<ProjectTask>(projectTaskDto);

          //  _context.ProjectTasks.Add(projectTask);
          //  await _context.SaveChangesAsync();

          //  return CreatedAtAction("GetProjectTask", new { id = projectTask.Id }, projectTask);
         
            var createdProjectTaskDto =await _projectTaskServiceAsync.Create(projectTaskDto);
            return CreatedAtAction("GetProjectTask", new { id = createdProjectTaskDto.Id }, createdProjectTaskDto);

        }

        // DELETE: api/ProjectTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTask(int id)
        {
            //if (_context.ProjectTasks == null)
            //{
            //    return NotFound();
            //}
            //var projectTask = await _context.ProjectTasks.FindAsync(id);
            //if (projectTask == null)
            //{
            //    return NotFound();
            //}

            ////_context.ProjectTasks.Remove(projectTask);
            //projectTask.IsDeleted = true;
            //await _context.SaveChangesAsync();

            //return NoContent();

            var res=await _projectTaskServiceAsync.Delete(id);
            return NoContent();
        }

        //private bool ProjectTaskExists(int id)
        //{
        //    return (_context.ProjectTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
