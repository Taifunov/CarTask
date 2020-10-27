using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using CarTestTask.Tests.Helpers;
using FluentAssertions;
using CarTestTask.Models;
using Newtonsoft.Json;

namespace CarTestTask.Tests.Tests
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
            var request = new
            {
                Url = "/api/car/GetAllTest"
            };
            // Act
            var response = await _client.GetAsync(request.Url);
            var value = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<Car>>(value);
            // Assert
            result.Count.Should().Be(8);
        }

        [Fact]
        public async Task CreateOrUpdate_ShouldUpdateJustDescription()
        {
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


            var result = JsonConvert.DeserializeObject<Car>(value);

            // Assert
           result.Name.Should().BeEquivalentTo("Volvo v.33");
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

            var result = JsonConvert.DeserializeObject<Car>(value);

            // Assert
            result.Description.Should().BeEquivalentTo("Best car");
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

            var result = JsonConvert.DeserializeObject<Car>(value);
            // Assert
            result.Id.Should().NotBeNullOrEmpty();
        }
    }
}