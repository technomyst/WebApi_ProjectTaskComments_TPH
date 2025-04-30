using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi_ProjectTaskComments.Exceptions;
using WebApi_ProjectTaskComments.Models;
using WebApi_ProjectTaskComments.Models.Dto.Projects;
using WebApi_ProjectTaskComments.Models.Enums;
using WebApi_ProjectTaskComments.Services.Interfaces;

namespace WebApi_ProjectTaskComments.Services
{
    public class ProjectServiceAsync : IProjectServiceAsync
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProjectServiceAsync(AppDbContext dbContext,IMapper mapper)
        {
            _dbContext= dbContext;
            _mapper = mapper;
        }
        public async Task<ProjectDto> Create(CreateProjectDto createProjectDto)
        {
            if (_dbContext.Projects == null)
            {
                //return Problem("Entity set 'AppDbContext.Projects'  is null.");
                return null;
            }
            //var project=_mapper.Map<Project>(createProjectDto);
            var project = new Project()
            {
                CreatedDate = DateTime.UtcNow,
                Description = createProjectDto.Description,
                Name = createProjectDto.Name,
                IsDeleted = false
            };

            var projectSaveResult = await _dbContext.Projects.AddAsync(project);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            var projectResult = projectSaveResult.Entity;

            return _mapper.Map<ProjectDto>(projectResult);
        }

        public async Task<bool> Delete(int id)
        {
            if (_dbContext.Projects == null)
            {
                //return NotFound();
                return false;
            }

            var project = await _dbContext.Projects.FindAsync(id);
            if (project == null)
            {
                //return NotFound();
                //return false;
                throw new EntityNotFoundException($"Project with id={id} not found");
            }

            project.IsDeleted = true;
            
            int res=await _dbContext.SaveChangesAsync();
            if (res < 1) 
            { 
                return false; 
            }
            return true;
        }

        public async Task<IEnumerable<ProjectDto>> GetAll()
        {
            //if (_context.Projects == null)
            //{
            //    return NotFound();
            //}
            var projects = await _dbContext.Projects.ToListAsync();
            var projectDtos = _mapper.Map<List<ProjectDto>>(projects);
            return projectDtos;
        }

        public async Task<ProjectFullInfoDto> GetById(int id)
        {
            //var projectFullInfoDto = await _dbContext.Projects.Where(p => p.Id == id)
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
            //todo: Сравнить производительность
            //var projectResult = projectFullInfoDto.FirstOrDefault();
            if (_dbContext.Projects == null)
            {
                return null;
            }

            var projectFullInfo = await _dbContext.Projects//.Where(p => p.Id == id)
           .Include(project => project.ProjectTasks!.OrderByDescending(x=>x.Id).Take(10)).ThenInclude(projectTask => projectTask.ProjectTaskComments!.OrderByDescending(x => x.Id).Take(10))//.AsSplitQuery()
           .Include(project => project.ProjectComments!.OrderByDescending(x => x.Id).Take(10))
           .AsSplitQuery().AsNoTracking()
           .FirstOrDefaultAsync(project => project.Id == id);
            //.ToListAsync();
            if (projectFullInfo == null)
            {
                //return null;
                throw new EntityNotFoundException($"Project with id={id} not found");
            }
            var projectFullInfoDto=_mapper.Map<ProjectFullInfoDto>(projectFullInfo);
            return projectFullInfoDto;
        }

        public async Task<ProjectDto> Update(UpdateProjectDto updateProjectDto)
        {
            //if (id != projectDto.Id)
            //{
            //    return null; //BadRequest();
            //}
            //_context.Entry(projectDto).State = EntityState.Modified;
            var project = _dbContext.Projects.FirstOrDefault(s => s.Id == updateProjectDto.Id);
            if (project == null)
            {
                //return null;// NotFound();
                throw new EntityNotFoundException($"Project with id={updateProjectDto.Id} not found");
            }

            
            project = _mapper.Map(updateProjectDto, project);
            //_context.Projects.Add(project);
            project.LastUpdateDate = DateTime.UtcNow;

            int res = await _dbContext.SaveChangesAsync();
            if (res < 1)
            {
                return null;
            }
            var projectDto=_mapper.Map<ProjectDto>(project);
            return projectDto;
            
        }
    }
}
