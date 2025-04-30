using WebApi_ProjectTaskComments.Models.Dto.Projects;
using WebApi_ProjectTaskComments.Models.Dto.ProjectTasks;

namespace WebApi_ProjectTaskComments.Services.Interfaces
{
    public interface IProjectTaskServiceAsync
    {
        Task<IEnumerable<ProjectTaskDto>> GetAll();
        Task<IEnumerable<ProjectTaskDto>> GetByProjectId(int id);
        Task<ProjectTaskDto> GetById(int id);
        Task<ProjectTaskDto> Create(CreateProjectTaskDto createProjectDto);
        Task<ProjectTaskDto> Update(ProjectTaskDto projectDto);
        Task<bool> Delete(int id);
    }
}
