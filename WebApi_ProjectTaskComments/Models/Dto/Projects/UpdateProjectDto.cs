namespace WebApi_ProjectTaskComments.Models.Dto.Projects
{
    public class UpdateProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime? LastUpdateDate { get; set; }
    }
}
