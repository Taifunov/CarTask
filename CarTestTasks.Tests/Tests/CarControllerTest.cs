using CarTestTask.Controllers;
using System;
using System.Threading.Tasks;
using CarTestTask.Services;
using Moq;
using Xunit;
using CarTestTask;
using CarTestTask.Tests;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using CarTestTask.Tests.Helpers;
using CarTestTask.Tests.DbContext;
using FluentAssertions;
using System.Net;

namespace CarTestTasks.Tests.Tests
{
    public class CarControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public CarControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(_factory));
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            }) 
                ?? throw new ArgumentNullException(nameof(_client));
        }

        [Fact]
        public async Task GetAllTest_ShouldReturnAllCars()
        {
            var mockRepository = new Mock<IInMemoryCarService>();
            var controller = new CarController(null, mockRepository.Object);
            var request = new
            {
                Url = "/api/car/GetAllTest"
            };
            // Act
            var response = await _client.GetAsync(request.Url);
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateOrUpdate_ShouldUpdateJustDescription()
        {
            var dbContext = DbContextMocker.GetCarsDbContext(nameof(CreateOrUpdate_ShouldUpdateJustDescription));
            // Arrange
            var request = new
            {
                Url = "/api/car/CreateOrUpdateForTest",
                Body = new
                {
                    Id = "5f8d5cd949413db2654df1db",
                    Description = "Bad car"
                }
            };

            // Act
            var response = await _client.PostAsync(request.Url, Utilities.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

           // Assert
           response.EnsureSuccessStatusCode();
           response.StatusCode.Should().Be(HttpStatusCode.OK);
           value.Should().Contain("Volvo v.33");
        }

        [Fact]
        public async Task CreateOrUpdate_DescriptionShouldNotBeNull()
        {
            // Arrange
            var request = new
            {
                Url = "/api/car/CreateOrUpdateForTest",
                Body = new
                {
                    Id = "5f8d5cd949413db2654df1db",
                    Name = "Volvo v.33"
                }
            };

            // Act
            var response = await _client.PostAsync(request.Url, Utilities.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            value.Should().Contain("Best car");
        }
        [Fact]
        public async Task CreateOrUpdate_ShouldCreateNewCar()
        {
            // Arrange
            var request = new
            {
                Url = "/api/car/CreateOrUpdateForTest",
                Body = new
                {
                    Name = "Zaz",
                    Description = "Not so interesting vehicle "
                }
            };

            // Act
            var response = await _client.PostAsync(request.Url, Utilities.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            value.Should().NotBeNullOrEmpty();
        }
    }
}