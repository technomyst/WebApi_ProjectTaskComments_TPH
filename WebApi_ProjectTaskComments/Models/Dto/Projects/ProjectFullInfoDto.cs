using WebApi_ProjectTaskComments.Models.Dto.Comments;
using WebApi_ProjectTaskComments.Models.Dto.ProjectTasks;

namespace WebApi_ProjectTaskComments.Models.Dto.Projects
{
    public class ProjectFullInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public List<ProjectTaskDto>? ProjectTasks { get; set; }
        public List<CommentDto>? Comments { get; set; }
        public List<int>? ProjectTasksIds { get; set; }
        public List<int>? CommentsIds { get; set; }
    }
}
