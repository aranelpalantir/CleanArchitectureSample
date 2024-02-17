using System.Net.Http.Json;
using CleanArchSample.Api.FunctionalTests.Abstractions;
using CleanArchSample.Application.Features.Products.Queries.GetAllProducts;
using FluentAssertions;

namespace CleanArchSample.Api.FunctionalTests.Features;

public class ProductTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnOk_And_Products_WhenProductsDoExist()
    {
        //Arrange
        await Login();

        //Act
        var response =
            await HttpClient.GetFromJsonAsync<List<GetAllProductsQueryResponse>>("api/v1.0/Product/GetAllProducts");

        //Assert
        response.Should().HaveCountGreaterThan(0);
    }
}