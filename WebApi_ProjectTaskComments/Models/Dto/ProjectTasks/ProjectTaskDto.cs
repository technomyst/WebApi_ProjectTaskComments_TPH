using WebApi_ProjectTaskComments.Models.Dto.Comments;

namespace WebApi_ProjectTaskComments.Models.Dto.ProjectTasks
{
    public class ProjectTaskDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
        public List<int>? CommentsIds { get; set; }
        public List<CommentDto>? Comments { get; set; }

    }
}
