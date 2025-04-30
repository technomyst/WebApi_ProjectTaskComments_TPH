using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi_ProjectTaskComments.Exceptions;
using WebApi_ProjectTaskComments.Models;
using WebApi_ProjectTaskComments.Models.Dto.Projects;
using WebApi_ProjectTaskComments.Models.Dto.ProjectTasks;
using WebApi_ProjectTaskComments.Services.Interfaces;

namespace WebApi_ProjectTaskComments.Services
{
    public class ProjectTaskServiceAsync : IProjectTaskServiceAsync
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProjectTaskServiceAsync(AppDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ProjectTaskDto> Create(CreateProjectTaskDto createProjectTaskDto)
        {
            //ProjectTask projectTask = _mapper.Map<ProjectTask>(createProjectDto);
            //todo:проверить результаты AddAsync и SaveChangesAsync. И когда присваивается сущности ее Id
            //Этот метод является асинхронным, чтобы разрешить специальным генераторам значений, таким как microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo, асинхронно обращаться к базе данных.Во всех остальных случаях следует использовать неасинхронный метод.
            //var AddedEntity =await _dbContext.ProjectTasks.AddAsync(projectTask);
            //int resOnSaveChanges= await _dbContext.SaveChangesAsync();


            //if (resOnSaveChanges < 1)
            //{
            //    throw new SaveEntityException("Problem on Adding Entity to Database");
            //}
            ////todo:проверить, есть ли Id
            //var projectTaskDto =_mapper.Map<ProjectTaskDto>(AddedEntity.Entity);
            //return projectTaskDto;

            if (_dbContext.ProjectTasks == null)
            {
                //return Problem("Entity set 'AppDbContext.Projects'  is null.");
                return null;
            }
            ProjectTask projectTask = _mapper.Map<ProjectTask>(createProjectTaskDto);

            var projectTaskSaveResult = _dbContext.ProjectTasks.Add(projectTask);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            var projectTaskResult = projectTaskSaveResult.Entity;

            return _mapper.Map<ProjectTaskDto>(projectTaskResult);
        }

        public async Task<bool> Delete(int id)
        {
            var projectTask=await _dbContext.ProjectTasks.FindAsync(id);
            
            if (projectTask == null)
            {
                throw new EntityNotFoundException($"Сущность c id={id} не найдена");
            }
            projectTask.IsDeleted = true;
            int res=await _dbContext.SaveChangesAsync();
            if (res < 1)
            {
                throw new SaveEntityException($"Ошибка сохранения");
            }
            return true;
        }

        public Task<IEnumerable<ProjectTaskDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProjectTaskDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetByProjectId(int projectId)
        {
            //if (_dbContext.ProjectTasks == null)
            //{
            //    return null;
            //}

            var projectTasks = await _dbContext.ProjectTasks.Where(projectTask => projectTask.ProjectId == projectId)
                .Include(projectTask => projectTask.ProjectTaskComments!.OrderByDescending(x => x.Id).Take(1))
           .AsSplitQuery().AsNoTracking()
           .ToListAsync();
            
            var projectTaskDtos = _mapper.Map<List<ProjectTaskDto>>(projectTasks);
            return projectTaskDtos;
        }

        public Task<ProjectTaskDto> Update(ProjectTaskDto projectDto)
        {
            throw new NotImplementedException();
        }
    }
}
