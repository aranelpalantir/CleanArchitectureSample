using CleanArchSample.Application.Features.Auth.Commands.Login;
using CleanArchSample.Application.Features.Auth.Commands.Register;
using CleanArchSample.Persistence.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CleanArchSample.Api.FunctionalTests.Abstractions;

public class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    protected HttpClient HttpClient { get; init; }
    protected readonly ISender Sender;
    protected readonly AppDbContext DbContext;
    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
        var scope = factory.Services.CreateScope();
        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    protected async Task Login()
    {
        var registerRequest = new RegisterCommandRequest
        {
            Email = "emailemailemail@emailemailemail.com",
            FullName = "FullName",
            Password = "123456",
            ConfirmPassword = "123456"
        };
        await HttpClient.PostAsJsonAsync("api/v1.0/Auth/Register", registerRequest);

        var loginRequest = new LoginCommandRequest
        {
            Email = "emailemailemail@emailemailemail.com",
            Password = "123456"
        };
        var loginResponse = await HttpClient.PostAsJsonAsync("api/v1.0/Auth/Login", loginRequest);
        if (loginResponse.IsSuccessStatusCode)
        {
            var loginCommandResponse = await loginResponse.Content.ReadFromJsonAsync<LoginCommandResponse>();
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginCommandResponse.Token);
        }
    }
}