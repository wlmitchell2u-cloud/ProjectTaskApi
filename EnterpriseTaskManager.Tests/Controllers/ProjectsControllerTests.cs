using EnterpriseTaskManager.Api.Controllers;
using EnterpriseTaskManager.Api.Hubs;
using EnterpriseTaskManager.Application.DTOs.Projects;
using EnterpriseTaskManager.Application.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;

namespace EnterpriseTaskManager.Tests.Controllers
{
    [TestFixture]
    public class ProjectsControllerTests
    {
        private Mock<IProjectService> _mockService = new();
        private ProjectsController _controller;
        private Mock<IHubContext<ProjectHub>> _mockHubContext = new();
        private Mock<IHubClients> _mockClients = new();
        private Mock<IClientProxy> _mockClientProxy = new();

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IProjectService>();

            // Mock HubContext and nested objects
            _mockHubContext = new Mock<IHubContext<ProjectHub>>();
            _mockClients = new Mock<IHubClients>();
            _mockClientProxy = new Mock<IClientProxy>();

            // Wire up HubContext -> Clients -> All
            _mockHubContext.Setup(h => h.Clients).Returns(_mockClients.Object);
            _mockClients.Setup(c => c.All).Returns(_mockClientProxy.Object);

            _mockClientProxy
                .Setup(proxy => proxy.SendCoreAsync(
                    It.IsAny<string>(),
                    It.IsAny<object[]>(),
                    default
                ))
                .Returns(Task.CompletedTask);

            _controller = new ProjectsController(_mockService.Object, _mockHubContext.Object);
        }

        [Test]
        public async Task GetAll_ReturnsOk_WhenProjectsExists()
        {
            //Arrange
            var projects = new List<ProjectDto>
            {
                new ProjectDto { Id = 1, Name = "Test Project 1", Description = "Test Description 1" },
                new ProjectDto { Id = 2, Name = "Test Project 2", Description = "Test Description 2" },
                new ProjectDto { Id = 3, Name = "Test Project 3", Description = "Test Description 3" },
            };

            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(projects);

            //Act
            var result = await _controller.GetAll();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedProjects = okResult.Value.Should().BeOfType<List<ProjectDto>>().Subject;

            returnedProjects[0].Id.Should().Be(1);
            returnedProjects.Count.Should().Be(3);

            _mockService.Verify(s => s.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetById_ReturnsOk_WhenProjectExists()
        {
            //Arrange
            var project = new ProjectDto
            {
                Id = 1,
                Name = "Test Project",
                Description = "Test Description",
            };

            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(project);

            //Act
            var result = await _controller.GetById(1);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedProject = okResult.Value.Should().BeOfType<ProjectDto>().Subject;

            returnedProject.Id.Should().Be(1);
            returnedProject.Name.Should().Be("Test Project");
            returnedProject.Description.Should().Be("Test Description");

            _mockService.Verify(s => s.GetByIdAsync(1), Times.Once);
        }

        [Test]
        public async Task GetById_ReturnsNotFound_WhenProjectDoesNotExist()
        {
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((ProjectDto?)null);

            //Act
            var result = await _controller.GetById(1);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            _mockService.Verify(s => s.GetByIdAsync(1), Times.Once);
        }

        [Test]
        public async Task Create_ReturnsCreatedAtAction_WhenProjectIsCreated()
        {
            ///Arrange
            ///Arrange - create input dto
            var createDto = new CreateProjectDto
            {
                Name = "Create Test",
                Description = "Creating a new Project",
                OwnerId = 1
            };

            var projectedResponseDto = new ProjectDto
            {
                Id = 1,
                Name = "Create Test",
                Description = "Creating a new Project",
                OwnerId = 1
            };

            _mockService.Setup(s => s.CreateAsync(createDto)).ReturnsAsync(projectedResponseDto);

            ///Act
            var result = await _controller.Create(createDto);

            ///Assert
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;

            createdResult.ActionName.Should().Be("GetById");
            createdResult.RouteValues.Should().NotBeNull();
            createdResult.RouteValues["id"].Should().Be(projectedResponseDto.Id);

            var returnedProject = createdResult.Value.Should().BeOfType<ProjectDto>().Subject;

            returnedProject.Id.Should().Be(1);
            returnedProject.Name.Should().Be("Create Test");
            returnedProject.Description.Should().Be("Creating a new Project");
            returnedProject.OwnerId.Should().Be(1);

            _mockService.Verify(s => s.CreateAsync(createDto), Times.Once);
        }

        [Test]
        public async Task Update_ReturnsOkResult_WhenProjectIsUpdated()
        {
            ///Arrange
            ///Arrange - update input dto
            var updateDto = new CreateProjectDto
            {
                Name = "Update Test",
                Description = "Updating an existing Project",
                OwnerId = 1
            };

            var projectedResponseDto = new ProjectDto
            {
                Id = 1,
                Name = "Update Test",
                Description = "Updating an existing Project",
                OwnerId = 1
            };

            _mockService.Setup(s => s.UpdateAsync(1, updateDto)).ReturnsAsync(projectedResponseDto);

            ///Act
            var result = await _controller.Update(1, updateDto);

            ///Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            _mockService.Verify(s => s.UpdateAsync(1, updateDto), Times.Once);
        }

        [Test]
        public async Task Delete_ReturnsNoContent_WhenProjectIsDeleted()
        {
            ///Arrange
            ///Look at the return type from the service method
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            ///Act
            ///Look at the return type from the controller delete method
            var result = await _controller.Delete(1);

            ///Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            _mockService.Verify(s => s.DeleteAsync(1), Times.Once);
        }

        [Test]
        public async Task Delete_ReturnsNotFound_WhenProjectDoesNotExist()
        {
            ///Arrange
            ///Look at the return type from the service method when the project does NOT exist
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(false);

            ///Act
            ///Look at the return type from the controller delete method when service returns false
            var result = await _controller.Delete(1);

            ///Assert
            result.Should().BeOfType<NotFoundResult>();
            _mockService.Verify(s => s.DeleteAsync(1), Times.Once);
        }
    }
}
