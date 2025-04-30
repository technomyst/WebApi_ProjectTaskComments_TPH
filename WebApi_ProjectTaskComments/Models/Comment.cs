using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApi_ProjectTaskComments.Models.Enums;

namespace WebApi_ProjectTaskComments.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentText { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CommentSourceTypeEnum SourceType { get; set; }
        
        public int? SourceId { get; set; } 
    }
}
