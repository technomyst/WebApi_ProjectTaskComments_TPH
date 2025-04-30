using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_ProjectTaskComments.Models;
using WebApi_ProjectTaskComments.Models.Dto.Projects;
using WebApi_ProjectTaskComments.Models.Enums;
using WebApi_ProjectTaskComments.Services.Interfaces;

namespace WebApi_ProjectTaskComments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        /*private readonly AppDbContext _context;*/

        private readonly IMapper _mapper;
        private readonly IProjectServiceAsync _projectService;
        public ProjectsController(/*AppDbContext context,*/ IMapper mapper,IProjectServiceAsync projectService)
        {
            /*_context = context;*/
            _mapper = mapper;
            _projectService = projectService;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {
            //if (_context.Projects == null)
            //{
            //    return NotFound();
            //}
            //var projects = await _context.Projects.ToListAsync();
            var projects = await _projectService.GetAll();
            if (projects == null)
            {
                return Problem("Проблема с базой данных");
            }
            return projects.ToList();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectFullInfoDto>> GetProject(int id)
        {
            var projectFullInfoDto = await _projectService.GetById(id);
            //return Ok(projectFullInfoDto);
            return projectFullInfoDto;

            //if (_context.Projects == null)
            //{
            //    return NotFound();
            //}

            //The warning is safe to ignore since the expression will be translated by EF core to an SQL Query, and won't be actually evaluated by the .NET runtime (so no NullReferenceException would be thrown).
            //so the !(null - forgiving) operator can be used to silence the warning as in Where(g => g.Organization!. /* rest of the expression here*/ )
            //var project1 = await _context.Projects.Where(p => p.Id == id)
            ////.Include(project => project.ProjectTasks!.Where(pt => pt.ProjectId == id))
            //.Include(project => project.ProjectTasks).ThenInclude(projectTask => projectTask.ProjectTaskComments)//.AsSplitQuery()
            //.Include(project => project.ProjectComments)
            //.ToListAsync();

            //var project1 =  await _context.Projects.Where(p => p.Id == id).Join(_context.ProjectTasks, p => p.Id, pt => pt.ProjectId, (p, pt) => p).ToListAsync();

            //var query = _context.Projects.Where(p => p.Id == id).Join(_context.ProjectTasks, p => p.Id, pt => pt.ProjectId, (p, pt) => p);
            //await query.Include(p => p.ProjectTasks).LoadAsync();
            //var project1 = await query.ToListAsync();

            //LEFT JOIN
            //var project1 = await _context.Projects.Where(p => p.Id == id).Include(p => p.ProjectTasks).ToListAsync();

            //var query =  _context.Projects.Where(p => p.Id == id)
            //    .Include(p => p.ProjectTasks).ThenInclude(pt => pt.ProjectTaskComments).Include(p => p.ProjectComments); 

            //var project1=await query.ToListAsync();



            //var project1 =
            //    from p in _context.Projects
            //    join pt in _context.ProjectTasks
            //    on p.Id equals pt.ProjectId //into ppt
            //    join com in _context.Comments
            //    on new { x1 = pt.Id, x2 = CommentSourceTypeEnum.ProjectTask }
            //    equals new { x1 = com.SourceId.Value, x2 = com.SourceType }
            //    //where com.SourceId != null
            //    select p;



            //var query = from p in _context.Projects
            //join pt in _context.ProjectTasks
            //on p.Id equals pt.ProjectId //into ppt
            //from com in _context.Comments.Where(com => com.SourceId == pt.Id && com.SourceType == CommentSourceTypeEnum.ProjectTask)
            //from comp in _context.Comments.Where(comp => comp.SourceId == p.Id && comp.SourceType == CommentSourceTypeEnum.Project).DefaultIfEmpty()
            //where p.Id==id
            //select p;

            //query.Include(x => x.ProjectTasks).ThenInclude(x=>x.ProjectTaskComments).Load();
            //query.Include(x => x.ProjectComments).Load();


            //var project1 = await query.ToListAsync();


            //join com in _context.Comments.Where()
            //on new { x1 = pt.Id, x2 = CommentSourceTypeEnum.ProjectTask }
            //equals new { x1 = com.SourceId, x2 = com.SourceType }
            //where com.SourceId != null


            //join ptc in _context.ProjectTaskComments 
            //on new { x1 = pt.Id, x2 = CommentSourceTypeEnum.ProjectTask } equals new { x1= ptc.Id, x2=ptc.SourceType }   
            //join pc in _context.ProjectComments
            //on new { x1 = p.Id, x2 = CommentSourceTypeEnum.ProjectTask } equals new { x1 = pc.Id, x2 = pc.SourceType }




            //from x in entity1
            //join y in entity2
            //on new { X1 = x.field1, X2 = x.field2 } equals new { X1 = y.field1, X2 = y.field2 }


            //if (project[0] == null)
            //{
            //    return NotFound();
            //}

            //Нормальный Inner join
            //var project1=await _context.Projects.Where(p=>p.Id==id).Join(_context.ProjectTasks, p => p.Id, pt => pt.ProjectId, (p, pt) => p)
            //    .ToListAsync();
            //      SELECT p."Id", p."Description", p."Name"
            //FROM "Projects" AS p
            //INNER JOIN "ProjectTasks" AS p0 ON p."Id" = p0."ProjectId"
            //WHERE p."Id" = @__id_0

            //var project1 = await _context.Projects
            //    .Where(p => p.Id == id)
            //.Join(_context.ProjectTasks, p => p.Id, pt => pt.ProjectId, (p, pt) => p)
            //.ToListAsync();
            //    .Include(p => p.ProjectTasks).ToListAsync();
            //      SELECT p."Name", p."Description", p."Id", t0."Id", t0."ProjectId", t0."Name", t0."Description", t0."Id0", t0."CommentText", t0."SourceId", t0."SourceType", t0."UserName", t0."SourceId0"
            //FROM "Projects" AS p
            //LEFT JOIN(
            //    SELECT p0."Id", p0."ProjectId", p0."Name", p0."Description", t."Id" AS "Id0", t."CommentText", t."SourceId", t."SourceType", t."UserName", t."SourceId0"
            //    FROM "ProjectTasks" AS p0
            //    LEFT JOIN(
            //        SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId" AS "SourceId0"
            //        FROM "Comments" AS c
            //        WHERE c."SourceType" = 2
            //    ) AS t ON p0."Id" = t."SourceId0"
            //) AS t0 ON p."Id" = t0."ProjectId"
            //ORDER BY p."Id", t0."Id"


            //      var project1 = await _context.Projects
            //      .Select(x => new Project()
            //      {   Name=x.Name, 
            //          Description=x.Description,
            //          Id=x.Id,
            //          ProjectTasks=x.ProjectTasks.Select(t=>new ProjectTask() 
            //                          { Id=t.Id,
            //                            ProjectId=t.ProjectId, 
            //                            Name=t.Name,
            //                            Description=t.Description,
            //                            ProjectTaskComments=t.ProjectTaskComments.Select(ptc=>ptc).ToList()
            //                          }).ToList()

            //      }

            //      ).ToListAsync();







            //.ThenInclude(pt=>pt.ProjectTaskComments);
            //project.Include(p => p.ProjectTasks);
            //.Join(_context.Comments.Where(com=>com.SourceType==CommentSourceTypeEnum.Project), p =>p.Id, com => com.SourceId, (p, pt) => p)
            //var project1 = await project.ToListAsync();


            //var query = (from p in _context.Projects
            //             join pt in _context.ProjectTasks
            //             on p.Id equals pt.ProjectId //into ppt
            //             from comt in _context.Comments.Where(comt => comt.SourceId == pt.Id && comt.SourceType == CommentSourceTypeEnum.ProjectTask).DefaultIfEmpty()
            //             from comp in _context.Comments.Where(comp => comp.SourceId == p.Id && comp.SourceType == CommentSourceTypeEnum.Project).DefaultIfEmpty()
            //             where p.Id == id
            //             select p)//.Include(p => p.ProjectTasks).ThenInclude(pt => pt.ProjectTaskComments).Include(p => p.ProjectComments);
            //var project1 = await query.ToListAsync();



            //         await _context.Projects
            //.Where(p => p.Id == id/* Insert Criteria */)
            //.SelectMany(x => x.ProjectTasks.SelectMany(c => c.ProjectTaskComments))
            //.ToListAsync();
            //         var parents = await _context.Projects
            // .Include(x => x.ProjectTasks)
            // .Where(p => p.Id == id /* Insert Same Criteria */)
            // .ToListAsync();

            //var project1 = await _context.Projects.Where(p => p.Id == id).Select(p => new
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Desciption = p.Description,
            //    Tasks = p.ProjectTasks.Select(pt =>
            //         new
            //         {
            //             Id = pt.Id,
            //             Name = pt.Name,
            //             Description = pt.Description,
            //             Comments = pt.ProjectTaskComments.Select(ptc => ptc)
            //         }),
            //    Comments = p.ProjectComments.Select(pc => pc)
            //}).ToListAsync();

            //Безотложная (Eager) загрузка
            // var project1 = await _context.Projects
            //.Include(project => project.ProjectTasks).ThenInclude(projectTask => projectTask.ProjectTaskComments)//.AsSplitQuery()
            //.Include(project => project.ProjectComments)
            //.Where(p => p.Id == id)
            //.FirstOrDefaultAsync();            

            //  info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //Executed DbCommand(124ms) [Parameters=[@__id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //SELECT t."Id", t."Description", t."Name", t0."Id", t0."Description", t0."Name", t0."ProjectId", t0."Id0", t0."CommentText", t0."SourceId", t0."SourceType", t0."UserName", t0."SourceId0", t2."Id", t2."CommentText", t2."SourceId", t2."SourceType", t2."UserName", t2."SourceId0"
            //FROM(
            //    SELECT p."Id", p."Description", p."Name"
            //    FROM "Projects" AS p
            //    WHERE p."Id" = @__id_0
            //    LIMIT 1
            //) AS t
            //LEFT JOIN(
            //    SELECT p0."Id", p0."Description", p0."Name", p0."ProjectId", t1."Id" AS "Id0", t1."CommentText", t1."SourceId", t1."SourceType", t1."UserName", t1."SourceId0"
            //    FROM "ProjectTasks" AS p0
            //    LEFT JOIN(
            //        SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId" AS "SourceId0"
            //        FROM "Comments" AS c
            //        WHERE c."SourceType" = 2
            //    ) AS t1 ON p0."Id" = t1."SourceId0"
            //) AS t0 ON t."Id" = t0."ProjectId"
            //LEFT JOIN(
            //    SELECT c0."Id", c0."CommentText", c0."SourceId", c0."SourceType", c0."UserName", c0."SourceId" AS "SourceId0"
            //    FROM "Comments" AS c0
            //    WHERE c0."SourceType" = 1
            //) AS t2 ON t."Id" = t2."SourceId0"
            //ORDER BY t."Id", t0."Id", t0."Id0"

            // var project = await _context.Projects
            //.Include(project => project.ProjectTasks).ThenInclude(projectTask => projectTask.ProjectTaskComments)//.AsSplitQuery()
            //.Include(project => project.ProjectComments)
            //.Where(p => p.Id == id)
            //.ToListAsync();

            //  var project1 = project[0];

            //  info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //Executed DbCommand(117ms) [Parameters=[@__id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //SELECT p."Id", p."Description", p."Name", t0."Id", t0."Description", t0."Name", t0."ProjectId", t0."Id0", t0."CommentText", t0."SourceId", t0."SourceType", t0."UserName", t0."SourceId0", t1."Id", t1."CommentText", t1."SourceId", t1."SourceType", t1."UserName", t1."SourceId0"
            //FROM "Projects" AS p
            //LEFT JOIN(
            //    SELECT p0."Id", p0."Description", p0."Name", p0."ProjectId", t."Id" AS "Id0", t."CommentText", t."SourceId", t."SourceType", t."UserName", t."SourceId0"
            //    FROM "ProjectTasks" AS p0
            //    LEFT JOIN(
            //        SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId" AS "SourceId0"
            //        FROM "Comments" AS c
            //        WHERE c."SourceType" = 2
            //    ) AS t ON p0."Id" = t."SourceId0"
            //) AS t0 ON p."Id" = t0."ProjectId"
            //LEFT JOIN(
            //    SELECT c0."Id", c0."CommentText", c0."SourceId", c0."SourceType", c0."UserName", c0."SourceId" AS "SourceId0"
            //    FROM "Comments" AS c0
            //    WHERE c0."SourceType" = 1
            //) AS t1 ON p."Id" = t1."SourceId0"
            //WHERE p."Id" = @__id_0
            //ORDER BY p."Id", t0."Id", t0."Id0"

            // var project1 = await _context.Projects
            //.Include(project => project.ProjectTasks).ThenInclude(projectTask => projectTask.ProjectTaskComments)//.AsSplitQuery()
            //.Include(project => project.ProjectComments)
            //.Where(p => p.Id == id)
            //.SingleOrDefaultAsync();

            //  info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //Executed DbCommand(109ms) [Parameters=[@__id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //SELECT t."Id", t."Description", t."Name", t0."Id", t0."Description", t0."Name", t0."ProjectId", t0."Id0", t0."CommentText", t0."SourceId", t0."SourceType", t0."UserName", t0."SourceId0", t2."Id", t2."CommentText", t2."SourceId", t2."SourceType", t2."UserName", t2."SourceId0"
            //FROM(
            //    SELECT p."Id", p."Description", p."Name"
            //    FROM "Projects" AS p
            //    WHERE p."Id" = @__id_0
            //    LIMIT 2
            //) AS t
            //LEFT JOIN(
            //    SELECT p0."Id", p0."Description", p0."Name", p0."ProjectId", t1."Id" AS "Id0", t1."CommentText", t1."SourceId", t1."SourceType", t1."UserName", t1."SourceId0"
            //    FROM "ProjectTasks" AS p0
            //    LEFT JOIN(
            //        SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId" AS "SourceId0"
            //        FROM "Comments" AS c
            //        WHERE c."SourceType" = 2
            //    ) AS t1 ON p0."Id" = t1."SourceId0"
            //) AS t0 ON t."Id" = t0."ProjectId"
            //LEFT JOIN(
            //    SELECT c0."Id", c0."CommentText", c0."SourceId", c0."SourceType", c0."UserName", c0."SourceId" AS "SourceId0"
            //    FROM "Comments" AS c0
            //    WHERE c0."SourceType" = 1
            //) AS t2 ON t."Id" = t2."SourceId0"
            //ORDER BY t."Id", t0."Id", t0."Id0"

            // var project1 = await _context.Projects
            //.Include(project => project.ProjectTasks).ThenInclude(projectTask => projectTask.ProjectTaskComments)//.AsSplitQuery()
            //.Include(project => project.ProjectComments)
            //.Where(p => p.Id == id)
            //.SingleOrDefaultAsync();

            //var project1 =  _context.Projects.Single(p => p.Id == id);
            // _context.Entry(project1)
            //    .Collection(b => b.ProjectTasks)
            //    .Load();
            //foreach (var ptask in project1.ProjectTasks)
            //{
            //     _context.Entry(ptask).Collection(pt => pt.ProjectTaskComments).Load();
            //}
            // _context.Entry(project1).Collection(p => p.ProjectComments).Load();

            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(106ms) [Parameters=[@__id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name"
            //      FROM "Projects" AS p
            //      WHERE p."Id" = @__id_0
            //      LIMIT 2
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name", p."ProjectId"
            //      FROM "ProjectTasks" AS p
            //      WHERE p."ProjectId" = @__p_0
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId"
            //      FROM "Comments" AS c
            //      WHERE(c."SourceType" = 2) AND(c."SourceId" = @__p_0)
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId"
            //      FROM "Comments" AS c
            //      WHERE(c."SourceType" = 1) AND(c."SourceId" = @__p_0)

            //var project1 = await _context.Projects.SingleAsync(p => p.Id == id);
            //await _context.Entry(project1)
            //    .Collection(b => b.ProjectTasks)
            //    .LoadAsync();
            //foreach (var ptask in project1.ProjectTasks)
            //{
            //    await _context.Entry(ptask).Collection(pt => pt.ProjectTaskComments).LoadAsync();
            //}
            //await _context.Entry(project1).Collection(p => p.ProjectComments).LoadAsync();

            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(106ms) [Parameters=[@__id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name"
            //      FROM "Projects" AS p
            //      WHERE p."Id" = @__id_0
            //      LIMIT 2
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(6ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name", p."ProjectId"
            //      FROM "ProjectTasks" AS p
            //      WHERE p."ProjectId" = @__p_0
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId"
            //      FROM "Comments" AS c
            //      WHERE(c."SourceType" = 2) AND(c."SourceId" = @__p_0)
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId"
            //      FROM "Comments" AS c
            //      WHERE(c."SourceType" = 1) AND(c."SourceId" = @__p_0)


            //var project1 =  _context.Projects.Single(p => p.Id == id);
            // _context.Entry(project1)
            //    .Collection(b => b.ProjectTasks)
            //    .Query().Include(p => p.ProjectTaskComments)
            //    .Load();
            // _context.Entry(project1)
            //    .Collection(b => b.ProjectComments).Load();
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(116ms) [Parameters=[@__id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name"
            //      FROM "Projects" AS p
            //      WHERE p."Id" = @__id_0
            //      LIMIT 2
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(2ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name", p."ProjectId", t."Id", t."CommentText", t."SourceId", t."SourceType", t."UserName", t."SourceId0"
            //      FROM "ProjectTasks" AS p
            //      LEFT JOIN(
            //          SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId" AS "SourceId0"
            //          FROM "Comments" AS c
            //          WHERE c."SourceType" = 2
            //      ) AS t ON p."Id" = t."SourceId0"
            //      WHERE p."ProjectId" = @__p_0
            //      ORDER BY p."Id"
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId"
            //      FROM "Comments" AS c
            //      WHERE(c."SourceType" = 1) AND(c."SourceId" = @__p_0)

            //var project1 = await _context.Projects.SingleAsync(p => p.Id == id);
            //await _context.Entry(project1)
            //    .Collection(b => b.ProjectTasks)
            //    .Query().Include(p => p.ProjectTaskComments)
            //    .LoadAsync();
            //await _context.Entry(project1)
            //    .Collection(b => b.ProjectComments).LoadAsync();

            //        info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(109ms) [Parameters=[@__id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name"
            //      FROM "Projects" AS p
            //      WHERE p."Id" = @__id_0
            //      LIMIT 2
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(8ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name", p."ProjectId", t."Id", t."CommentText", t."SourceId", t."SourceType", t."UserName", t."SourceId0"
            //      FROM "ProjectTasks" AS p
            //      LEFT JOIN(
            //          SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId" AS "SourceId0"
            //          FROM "Comments" AS c
            //          WHERE c."SourceType" = 2
            //      ) AS t ON p."Id" = t."SourceId0"
            //      WHERE p."ProjectId" = @__p_0
            //      ORDER BY p."Id"
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId"
            //      FROM "Comments" AS c
            //      WHERE(c."SourceType" = 1) AND(c."SourceId" = @__p_0)

            //var project1 = _context.Projects.Single(p => p.Id == id);
            //_context.Entry(project1)
            //   .Collection(b => b.ProjectTasks)
            //   .Query().Include(p => p.ProjectTaskComments).AsSplitQuery()
            //   .Load();
            //_context.Entry(project1)
            //   .Collection(b => b.ProjectComments).Load();

            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(104ms) [Parameters=[@__id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name"
            //      FROM "Projects" AS p
            //      WHERE p."Id" = @__id_0
            //      LIMIT 2
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name", p."ProjectId"
            //      FROM "ProjectTasks" AS p
            //      WHERE p."ProjectId" = @__p_0
            //      ORDER BY p."Id"
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT t."Id", t."CommentText", t."SourceId", t."SourceType", t."UserName", t."SourceId0", p."Id"
            //      FROM "ProjectTasks" AS p
            //      INNER JOIN(
            //          SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId" AS "SourceId0"
            //          FROM "Comments" AS c
            //          WHERE c."SourceType" = 2
            //      ) AS t ON p."Id" = t."SourceId0"
            //      WHERE p."ProjectId" = @__p_0
            //      ORDER BY p."Id"
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(0ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId"
            //      FROM "Comments" AS c
            //      WHERE(c."SourceType" = 1) AND(c."SourceId" = @__p_0)

            //var project1 = _context.Projects.Single(p => p.Id == id);
            //_context.Entry(project1)
            //    .Collection(b => b.ProjectComments).Load();

            //_context.Entry(project1)
            //    .Collection(p=>p.ProjectTasks)
            //    .Query()
            //    .Include(p => p.ProjectTaskComments).AsSplitQuery()
            //    .Load();
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //            Executed DbCommand(110ms) [Parameters=[@__id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name"
            //      FROM "Projects" AS p
            //      WHERE p."Id" = @__id_0
            //      LIMIT 2
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(3ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId"
            //      FROM "Comments" AS c
            //      WHERE(c."SourceType" = 1) AND(c."SourceId" = @__p_0)
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(2ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Description", p."Name", p."ProjectId"
            //      FROM "ProjectTasks" AS p
            //      WHERE p."ProjectId" = @__p_0
            //      ORDER BY p."Id"
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[@__p_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT t."Id", t."CommentText", t."SourceId", t."SourceType", t."UserName", t."SourceId0", p."Id"
            //      FROM "ProjectTasks" AS p
            //      INNER JOIN(
            //          SELECT c."Id", c."CommentText", c."SourceId", c."SourceType", c."UserName", c."SourceId" AS "SourceId0"
            //          FROM "Comments" AS c
            //          WHERE c."SourceType" = 2
            //      ) AS t ON p."Id" = t."SourceId0"
            //      WHERE p."ProjectId" = @__p_0
            //      ORDER BY p."Id"




            //Проекция 1 - возвращаем только ID зависимых сущностей
            //var project = await _context.Projects
            //    .Select(p => new ProjectDto
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Description = p.Description,
            //        ProjectTasksIds = p.ProjectTasks.Select(pt => pt.Id).ToList(),
            //        CommentsIds = p.ProjectComments.Select(pc => pc.Id).ToList()
            //    }).ToListAsync();
            //var project1 = project.FirstOrDefault();

            //        warn: Microsoft.EntityFrameworkCore.Query[20504]
            //      Compiling a query which loads related collections for more than one collection navigation, either via 'Include' or through projection, but no 'QuerySplittingBehavior' has been configured.By default, Entity Framework will use 'QuerySplittingBehavior.SingleQuery', which can potentially result in slow query performance.See https://go.microsoft.com/fwlink/?linkid=2134277 for more information. To identify the query that's triggering this warning call 'ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))'.
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(96ms)[Parameters =[], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Name", p."Description", p0."Id", t."Id"
            //      FROM "Projects" AS p
            //      LEFT JOIN "ProjectTasks" AS p0 ON p."Id" = p0."ProjectId"
            //      LEFT JOIN(
            //          SELECT c."Id", c."SourceId"
            //          FROM "Comments" AS c
            //          WHERE c."SourceType" = 1
            //      ) AS t ON p."Id" = t."SourceId"
            //      ORDER BY p."Id", p0."Id"


            //Проекция 2 - возвращаем целиком сущности
            //var project = await _context.Projects
            //    .Where(p => p.Id == id)
            //    .Select(p => new ProjectDto
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Description = p.Description,
            //        ProjectTasks = p.ProjectTasks.Select(pt =>
            //            new ProjectTaskDto
            //            {
            //                Id = pt.Id,
            //                Name = pt.Name,
            //                Description = pt.Description,
            //                ProjectId = pt.ProjectId,
            //                Comments = pt.ProjectTaskComments.Select(ptc =>
            //                  new CommentDto
            //                  {
            //                      Id = ptc.Id,
            //                      UserName = ptc.UserName,
            //                      SourceId = pt.Id,
            //                      SourceType = CommentSourceTypeEnum.ProjectTask,
            //                      CommentText = ptc.CommentText
            //                  }).ToList(),

            //            }).ToList(),
            //        Comments = p.ProjectComments.Select(pc => new CommentDto
            //        {
            //            Id = pc.Id,
            //            UserName = pc.UserName,
            //            SourceId = p.Id,
            //            SourceType = CommentSourceTypeEnum.Project,
            //            CommentText = pc.CommentText
            //        }).ToList()
            //    }).AsSingleQuery().ToListAsync();
            //var project1 = project.FirstOrDefault();
            //warn: Microsoft.EntityFrameworkCore.Query[20504]
            //      Compiling a query which loads related collections for more than one collection navigation, either via 'Include' or through projection, but no 'QuerySplittingBehavior' has been configured.By default, Entity Framework will use 'QuerySplittingBehavior.SingleQuery', which can potentially result in slow query performance.See https://go.microsoft.com/fwlink/?linkid=2134277 for more information. To identify the query that's triggering this warning call 'ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))'.
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(119ms)[Parameters =[@__id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Name", p."Description", t0."Id", t0."Name", t0."Description", t0."ProjectId", t0."Id0", t0."UserName", t0."SourceId", t0."SourceType", t0."CommentText", t1."Id", t1."UserName", t1."SourceId", t1."SourceType", t1."CommentText"
            //      FROM "Projects" AS p
            //      LEFT JOIN(
            //          SELECT p0."Id", p0."Name", p0."Description", p0."ProjectId", t."Id" AS "Id0", t."UserName", t."SourceId", t."SourceType", t."CommentText"
            //          FROM "ProjectTasks" AS p0
            //          LEFT JOIN LATERAL(
            //              SELECT c."Id", c."UserName", p0."Id" AS "SourceId", 2 AS "SourceType", c."CommentText"
            //              FROM "Comments" AS c
            //              WHERE(c."SourceType" = 2) AND(p0."Id" = c."SourceId")
            //          ) AS t ON TRUE
            //      ) AS t0 ON p."Id" = t0."ProjectId"
            //      LEFT JOIN LATERAL(
            //          SELECT c0."Id", c0."UserName", p."Id" AS "SourceId", 1 AS "SourceType", c0."CommentText"
            //          FROM "Comments" AS c0
            //          WHERE(c0."SourceType" = 1) AND(p."Id" = c0."SourceId")
            //      ) AS t1 ON TRUE
            //      WHERE p."Id" = @__id_0
            //      ORDER BY p."Id", t0."Id", t0."Id0"

            //Проекция 3
            //var projectList = await _context.Projects
            //    .Select(p => new ProjectDto()
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Description = p.Description
            //    }).ToListAsync();
            //var project = projectList.FirstOrDefault();

            //var projectTasks = await _context.ProjectTasks
            //    .Where(pt => pt.ProjectId == project.Id)
            //    .Select(pt => new ProjectTaskDto
            //    {
            //        Id = pt.Id,
            //        Name = pt.Name,
            //        Description = pt.Description,
            //        ProjectId = pt.ProjectId
            //    }).ToListAsync();


            //foreach (var pt in projectTasks)
            //{
            //    pt.Comments = await _context.Comments
            //    .Where(c => c.SourceType == CommentSourceTypeEnum.ProjectTask && c.SourceId == pt.Id)
            //    .Select(pc => new CommentDto()
            //    {
            //        Id = pc.Id,
            //        UserName = pc.UserName,
            //        SourceId = pc.Id,
            //        SourceType = CommentSourceTypeEnum.Project,
            //        CommentText = pc.CommentText
            //    }).ToListAsync();
            //}

            //var projectComments = await _context.Comments
            //    .Where(c => c.SourceType == CommentSourceTypeEnum.Project && c.SourceId == project.Id)
            //    .Select(pc => new CommentDto()
            //    {
            //        Id = pc.Id,
            //        UserName = pc.UserName,
            //        SourceId = pc.Id,
            //        SourceType = CommentSourceTypeEnum.Project,
            //        CommentText = pc.CommentText
            //    }).ToListAsync();

            //project.Comments = projectComments;
            //project.ProjectTasks = projectTasks;
            //var project1 = project;

            //            Entity Framework Core 6.0.35 initialized 'AppDbContext' using provider 'Npgsql.EntityFrameworkCore.PostgreSQL:6.0.29+1cc46a0695f052ba0f1ade7046d577882d351f39' with options: None
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(104ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Name", p."Description"
            //      FROM "Projects" AS p
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(23ms) [Parameters=[@__project_Id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Name", p."Description", p."ProjectId"
            //      FROM "ProjectTasks" AS p
            //      WHERE p."ProjectId" = @__project_Id_0
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(6ms) [Parameters=[@__pt_Id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT c."Id", c."UserName", 1 AS "SourceType", c."CommentText"
            //      FROM "Comments" AS c
            //      WHERE(c."SourceType" = 2) AND(c."SourceId" = @__pt_Id_0)
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(9ms) [Parameters=[@__project_Id_0 = '?'(DbType = Int32)], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT c."Id", c."UserName", 1 AS "SourceType", c."CommentText"
            //      FROM "Comments" AS c
            //      WHERE(c."SourceType" = 1) AND(c."SourceId" = @__project_Id_0)

            //Проекция 4 - возвращаем целиком сущности. AsSingleQuery()
            //ЛУЧШИЙ ВАРИАНТ???
            //var project = await _context.Projects.Where(p=>p.Id==id)
            //    .Select(p => new ProjectFullInfoDto
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Description = p.Description,
            //        ProjectTasks = p.ProjectTasks.Select(pt =>
            //            new ProjectTaskDto
            //            {
            //                Id = pt.Id,
            //                Name = pt.Name,
            //                Description = pt.Description,
            //                ProjectId = pt.ProjectId,
            //                Comments = pt.ProjectTaskComments.Select(ptc =>
            //                  new CommentDto
            //                  {
            //                      Id = ptc.Id,
            //                      UserName = ptc.UserName,
            //                      SourceId = pt.Id,
            //                      SourceType = CommentSourceTypeEnum.ProjectTask,
            //                      CommentText = ptc.CommentText
            //                  }).ToList(),

            //            }).ToList(),
            //        Comments = p.ProjectComments.Select(pc => new CommentDto
            //        {
            //            Id = pc.Id,
            //            UserName = pc.UserName,
            //            SourceId = p.Id,
            //            SourceType = CommentSourceTypeEnum.Project,
            //            CommentText = pc.CommentText
            //        }).ToList()
            //    }).AsSplitQuery().AsNoTracking().ToListAsync();
            //var projectResult = project.FirstOrDefault();


            //info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
            //            Entity Framework Core 6.0.35 initialized 'AppDbContext' using provider 'Npgsql.EntityFrameworkCore.PostgreSQL:6.0.29+1cc46a0695f052ba0f1ade7046d577882d351f39' with options: None
            //      info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(93ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p."Id", p."Name", p."Description"
            //      FROM "Projects" AS p
            //      ORDER BY p."Id"
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(6ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT p0."Id", p0."Name", p0."Description", p0."ProjectId", p."Id"
            //      FROM "Projects" AS p
            //      INNER JOIN "ProjectTasks" AS p0 ON p."Id" = p0."ProjectId"
            //      ORDER BY p."Id", p0."Id"
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(3ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT t."Id", t."UserName", t."SourceId", t."SourceType", t."CommentText", p."Id", p0."Id"
            //      FROM "Projects" AS p
            //      INNER JOIN "ProjectTasks" AS p0 ON p."Id" = p0."ProjectId"
            //      JOIN LATERAL(
            //          SELECT c."Id", c."UserName", p0."Id" AS "SourceId", 2 AS "SourceType", c."CommentText"
            //          FROM "Comments" AS c
            //          WHERE(c."SourceType" = 2) AND(p0."Id" = c."SourceId")
            //      ) AS t ON TRUE
            //      ORDER BY p."Id", p0."Id"
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(1ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT t."Id", t."UserName", t."SourceId", t."SourceType", t."CommentText", p."Id"
            //      FROM "Projects" AS p
            //      JOIN LATERAL(
            //          SELECT c."Id", c."UserName", p."Id" AS "SourceId", 1 AS "SourceType", c."CommentText"
            //          FROM "Comments" AS c
            //          WHERE(c."SourceType" = 1) AND(p."Id" = c."SourceId")
            //      ) AS t ON TRUE
            //      ORDER BY p."Id"


            //return projectResult;//project1.FirstOrDefault();




        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id,UpdateProjectDto updateProjectDto)
        {
            if (id != updateProjectDto.Id)
            {
                return BadRequest();
            }

            //_context.Entry(projectDto).State = EntityState.Modified;
            //var project = _context.Projects.FirstOrDefault(s => s.Id.Equals(id));
            //if (project == null)
            //{
            //    return NotFound();
            //}


            //project = _mapper.Map(projectDto, project);
            ////_context.Projects.Add(project);
            //project.LastUpdateDate = DateTime.UtcNow;
            //await _context.SaveChangesAsync();
            //return NoContent();

            var updateProjectResultDto = await _projectService.Update(updateProjectDto);
            if (updateProjectResultDto == null)
            { 
                return Problem();
            }
            return NoContent();




            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ProjectExists(id))
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

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectDto createProjectDto)
        {
            //if (_context.Projects == null)
            //{
            //    return Problem("Entity set 'AppDbContext.Projects'  is null.");
            //}
            //_context.Projects.Add(project);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetProject", new { id = project.Id }, project);

            var projectDto= await _projectService.Create(createProjectDto);
            return CreatedAtAction("GetProject", new { id = projectDto.Id }, projectDto);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            //if (_context.Projects == null)
            //{
            //    return NotFound();
            //}
            //var project = await _context.Projects.FindAsync(id);
            //if (project == null)
            //{
            //    return NotFound();
            //}

            ////_context.Projects.Remove(project);
            //project.IsDeleted = true;
            ////_context.Projects.Remove(project);
            //await _context.SaveChangesAsync();

            //return NoContent();

            var result=await _projectService.Delete(id);
            if (!result)
            {
                return Problem();
            }
            return NoContent();

        }

        //private bool ProjectExists(int id)
        //{
        //    return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
