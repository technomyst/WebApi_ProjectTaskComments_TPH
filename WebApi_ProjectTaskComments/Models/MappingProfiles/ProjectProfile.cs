using AutoMapper;
using WebApi_ProjectTaskComments.Models.Dto.Projects;

namespace WebApi_ProjectTaskComments.Models.MappingProfiles
{
    public class ProjectProfile:Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDto, Project>();
            CreateMap<Project, ProjectDto>();
            CreateMap<UpdateProjectDto, Project>().ForMember(x=>x.LastUpdateDate,opt=>opt.Ignore());
            CreateMap<Project, UpdateProjectDto>();
            CreateMap<Project, ProjectFullInfoDto>();
            CreateMap<ProjectFullInfoDto, Project>();
        }
    }
}
