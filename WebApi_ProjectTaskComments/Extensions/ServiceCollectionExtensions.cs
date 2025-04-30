using WebApi_ProjectTaskComments.Services;
using WebApi_ProjectTaskComments.Services.Interfaces;

namespace WebApi_ProjectTaskComments.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProjectServiceAsync,ProjectServiceAsync>();
            builder.Services.AddScoped<IProjectTaskServiceAsync, ProjectTaskServiceAsync>();

            return builder;
        }
    }
}
