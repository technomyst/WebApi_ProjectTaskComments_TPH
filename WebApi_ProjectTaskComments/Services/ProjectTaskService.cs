using Microsoft.EntityFrameworkCore;
using WebApi_ProjectTaskComments.Models;
using WebApi_ProjectTaskComments.Services.Interfaces;

namespace WebApi_ProjectTaskComments.Services
{
    public class ProjectTaskService : IUniversalService<ProjectTask>
    {
        private readonly AppDbContext _dbContext;
        public ProjectTaskService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProjectTask Create(ProjectTask item)
        {
            if (_dbContext.ProjectTasks == null)
            {
                //return Problem("Entity set 'AppDbContext.ProjectTasks'  is null.");
                return null;
            }
            _dbContext.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        //public bool Delete(int id)
        //{
        //    var projectTask = _dbContext.ProjectTasks.Find(id);
        //    if (projectTask==null)
        //    {
        //        return false; 
        //    }
        //    projectTask.IsDeleted = true;
        //    var res = _dbContext.SaveChanges();
        //    if (res >= 1)
        //    { 
        //        return true; 
        //    }
        //}

        //будет ли работать? Из-за отслеживания
        // Разделение операции Delete на поиск GetById и Delete. Чтобы в метое контроллера
        // можно было выдать NotFound
        public bool Delete(ProjectTask item)
        {
            item.IsDeleted = true;
            var res=_dbContext.SaveChanges();
            if (res >= 1)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<ProjectTask> GetAll()
        {
            return _dbContext.ProjectTasks.ToList();
        }

        public IEnumerable<ProjectTask> GetAllByParentItemId(int parentItemId)
        {
            return _dbContext.ProjectTasks.Where(x=>x.ProjectId== parentItemId).ToList();
        }

        public ProjectTask? GetById(int id)
        {
            if (_dbContext.ProjectTasks == null)
            {
                return null;
            }

            var projectTask=_dbContext.ProjectTasks.Find(id);
            return projectTask;
        }

        public ProjectTask? Update(ProjectTask item)
        {
            int id = 0;
            try
            {
                id = item.Id;
                _dbContext.Update(item);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTaskExists(id))
                {
                    return null;//NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return item;
        }

        private bool ProjectTaskExists(int id)
        {
            return (_dbContext.ProjectTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public ProjectTask Update(int id,ProjectTask item)
        {
            _dbContext.Update(item);
            var res = _dbContext.SaveChanges();
            return item;
            
        }
    }
}
