using AutoMapper;
using WebApi_ProjectTaskComments.Models.Dto.Comments;

namespace WebApi_ProjectTaskComments.Models.MappingProfiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentDto, ProjectComment>();
            CreateMap<CommentDto, ProjectTaskComment>();
            CreateMap<ProjectComment, CommentDto>();
            CreateMap<ProjectTaskComment, CommentDto>();


        }
    }
}
