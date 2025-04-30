using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_ProjectTaskComments.Models
{
    public class ProjectComment : Comment
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}
