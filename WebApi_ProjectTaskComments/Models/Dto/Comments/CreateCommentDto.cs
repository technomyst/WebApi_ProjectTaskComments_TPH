using System.Text.Json.Serialization;
using WebApi_ProjectTaskComments.Models.Enums;

namespace WebApi_ProjectTaskComments.Models.Dto.Comments
{
    public class CreateCommentDto
    {
        public int Id { get; set; }
        public string CommentText { get; set; } = null!;
        public string UserName { get; set; } = null!;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CommentSourceTypeEnum SourceType { get; set; }
        public int? SourceId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
