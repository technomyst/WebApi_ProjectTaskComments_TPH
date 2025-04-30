

using WebApi_ProjectTaskComments.Models;

namespace WebApi_ProjectTaskComments.Services.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        Project GetById(int id);
        Project Create(Project project);
        Project Update(Project project);
        bool Delete(int id);
    }
}
