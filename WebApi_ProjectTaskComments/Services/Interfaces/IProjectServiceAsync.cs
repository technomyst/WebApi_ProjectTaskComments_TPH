using WebApi_ProjectTaskComments.Models;
using WebApi_ProjectTaskComments.Models.Dto.Projects;

namespace WebApi_ProjectTaskComments.Services.Interfaces
{
    public interface IProjectServiceAsync
    {
        Task<IEnumerable<ProjectDto>> GetAll();
        Task<ProjectFullInfoDto> GetById(int id);
        Task<ProjectDto> Create(CreateProjectDto createProjectDto);
        Task<ProjectDto> Update(UpdateProjectDto updateProjectDto);
        Task<bool> Delete(int id);
    }
}
