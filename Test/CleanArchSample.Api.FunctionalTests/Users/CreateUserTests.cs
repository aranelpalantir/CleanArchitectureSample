using System.Net;
using System.Net.Http.Json;
using CleanArchSample.Api.FunctionalTests.Abstractions;
using CleanArchSample.Application.Features.Auth.Commands.Register;
using CleanArchSample.Domain.Entities;
using FluentAssertions;

namespace CleanArchSample.Api.FunctionalTests.Users;

public class CreateUserTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnBadRequest_WhenEmailIsMissing()
    {
        //Arrange
        var request = new RegisterCommandRequest
        {
            Email = "",
            FullName = "FullName",
            Password = "123456",
            ConfirmPassword = "123456"
        };

        //Act
        var response = await HttpClient.PostAsJsonAsync("api/v1.0/Auth/Register", request);

        //Assert

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task Should_ReturnBadRequest_WhenFullNameIsMissing()
    {
        //Arrange
        var request = new RegisterCommandRequest
        {
            Email = "email@email.com",
            FullName = "",
            Password = "123456",
            ConfirmPassword = "123456"
        };

        //Act
        var response = await HttpClient.PostAsJsonAsync("api/v1.0/Auth/Register", request);

        //Assert

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task Should_ReturnBadRequest_WhenPasswordIsMissing()
    {
        //Arrange
        var request = new RegisterCommandRequest
        {
            Email = "email@email.com",
            FullName = "",
            Password = "",
            ConfirmPassword = "123456"
        };

        //Act
        var response = await HttpClient.PostAsJsonAsync("api/v1.0/Auth/Register", request);

        //Assert

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task Should_ReturnBadRequest_WhenConfirmPasswordIsMissing()
    {
        //Arrange
        var request = new RegisterCommandRequest
        {
            Email = "email@email.com",
            FullName = "FullName",
            Password = "123456",
            ConfirmPassword = ""
        };

        //Act
        var response = await HttpClient.PostAsJsonAsync("api/v1.0/Auth/Register", request);

        //Assert

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task Should_ReturnBadRequest_WhenConfirmPasswordIsNotBeSamePassword()
    {
        //Arrange
        var request = new RegisterCommandRequest
        {
            Email = "email@email.com",
            FullName = "FullName",
            Password = "123456",
            ConfirmPassword = "123"
        };

        //Act
        var response = await HttpClient.PostAsJsonAsync("api/v1.0/Auth/Register", request);

        //Assert

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task Should_ReturnConflict_WhenUserExists()
    {
        //Arrange
        var registerCommand = new RegisterCommandRequest
        {
            Email = "email123@email.com",
            FullName = "FullName",
            Password = "123456",
            ConfirmPassword = "123456"
        };
        await HttpClient.PostAsJsonAsync("api/v1.0/Auth/Register", registerCommand);

        var request = new RegisterCommandRequest
        {
            Email = "email123@email.com",
            FullName = "FullName",
            Password = "123456",
            ConfirmPassword = "123456"
        };

        //Act
        var response = await HttpClient.PostAsJsonAsync("api/v1.0/Auth/Register", request);

        //Assert

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
    [Fact]
    public async Task Should_ReturnCreated_WhenUserCreated()
    {
        //Arrange
        var request = new RegisterCommandRequest
        {
            Email = "email@email.com",
            FullName = "FullName",
            Password = "123456",
            ConfirmPassword = "123456"
        };

        //Act
        var response = await HttpClient.PostAsJsonAsync("api/v1.0/Auth/Register", request);

        //Assert

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}