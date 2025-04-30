namespace WebApi_ProjectTaskComments.Models.Dto.ProjectTasks
{
    public class CreateProjectTaskDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
    }
}
