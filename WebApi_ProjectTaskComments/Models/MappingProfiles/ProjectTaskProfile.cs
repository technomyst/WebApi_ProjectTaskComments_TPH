using AutoMapper;
using WebApi_ProjectTaskComments.Models.Dto.ProjectTasks;

namespace WebApi_ProjectTaskComments.Models.MappingProfiles
{
    public class ProjectTaskProfile:Profile
    {
        public ProjectTaskProfile()
        {
            CreateMap<CreateProjectTaskDto, ProjectTask>();
            //CreateMap<ProjectTaskDto, ProjectTask>();
            CreateMap<ProjectTask, ProjectTaskDto>().ForMember(
                dest=>dest.CommentsIds,
                opt => opt.MapFrom(src => src.ProjectTaskComments == null ? null : src.ProjectTaskComments.Select(ptc => ptc.Id))
                //opt=>opt.MapFrom(src=> src.ProjectTaskComments == null? null : src.ProjectTaskComments.Select(ptc=>ptc.Id).OrderByDescending(x=>x).Take(10))
                )
                .ForMember(
                dest => dest.Comments,
                opt=> opt.MapFrom(src => src.ProjectTaskComments == null ? null : src.ProjectTaskComments)
                );
        }
        
    }
}
