namespace WebApi_ProjectTaskComments.Models
{
    public class ProjectTaskComment:Comment
    {
        
        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; } = null!;

    }
}
