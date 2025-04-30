using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi_ProjectTaskComments.Controllers;
using WebApi_ProjectTaskComments.Models;
using WebApi_ProjectTaskComments.Models.Dto.Projects;
using WebApi_ProjectTaskComments.Models.MappingProfiles;
using WebApi_ProjectTaskComments.Services.Interfaces;
using Xunit;

namespace WebApi_ProjectTaskComments.Tests.ControllersTests
{

    public class ProjectControllerTest
    {

        private static IMapper _mapper;

        public ProjectControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ProjectProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        public List<ProjectDto> GetAllProjects()
        { 
            var projectList=new List<ProjectDto>();
            projectList.Add(new ProjectDto()
            { 
                 Id = 1,
                 CreatedDate = DateTime.Now,
                 Description = "description",
                 //IsDeleted = false,
                 LastUpdateDate = DateTime.Now,
                 Name = "name",
                 //ProjectComments = new List<ProjectComment>(),
                // ProjectTasks = new List<ProjectTask>()
            });
            projectList.Add(new ProjectDto()
            {
                Id = 2,
                CreatedDate = DateTime.Now,
                Description = "description2",
                //IsDeleted = false,
                LastUpdateDate = DateTime.Now,
                Name = "name2",
                //ProjectComments = new List<ProjectComment>(),
                //ProjectTasks = new List<ProjectTask>()
            });

            return projectList;
        }

        [Fact]
        public async Task ProjectControllerTest_GetAll_ReturnAll() 
        {
            //Arrange
            var mockService = new Mock<IProjectServiceAsync>();

            mockService.Setup(service => service.GetAll()).ReturnsAsync(GetAllProjects());


            var projectsController = new ProjectsController(_mapper, mockService.Object);

            //Act
            //var result = (OkObjectResult)await projectsController.GetProjects();
            var result = await projectsController.GetProjects();
            //Assert
            //var okResult = Assert.IsType<OkObjectResult>(result);
            //var returnValue = Assert.IsType<List<ProjectDto>>(okResult.Value);
            var returnValue = Assert.IsType<List<ProjectDto>>(result.Value);
            var project = returnValue.FirstOrDefault();
            Assert.Equal(1, project.Id);
        }
        [Fact]
        public async Task ProjectControllerTest_GetAll_ReturnProblemIfEmpty()
        {
            //Arrange
            var mockService = new Mock<IProjectServiceAsync>();

            mockService.Setup(service => service.GetAll()).ReturnsAsync(()=>null);


            var projectsController = new ProjectsController(_mapper, mockService.Object);

            //Act
            //var result = (OkObjectResult)await projectsController.GetProjects();
            var result = await projectsController.GetProjects();
            //Assert
            var problemResult=Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(problemResult.StatusCode, StatusCodes.Status500InternalServerError);
           
        }

    }
}
