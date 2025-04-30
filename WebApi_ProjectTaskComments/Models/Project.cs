namespace WebApi_ProjectTaskComments.Models
{
    public class Project
    {
        public int Id { get; set; }        
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public List<ProjectTask>? ProjectTasks { get;set;}
        public List<ProjectComment>? ProjectComments { get; set; }
        //public List<Comment>? Comments { get; set; }
    }
}
