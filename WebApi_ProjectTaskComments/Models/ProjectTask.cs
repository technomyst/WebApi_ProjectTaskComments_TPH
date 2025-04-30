namespace WebApi_ProjectTaskComments.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? CreatedDate {get;set;}
        public DateTime? LastUpdateDate { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public List<ProjectTaskComment>? ProjectTaskComments { get; set; }
        //public List<Comment>? Comment { get; set; }
        
    }
}
