using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentAssertions;
using Moq;

namespace ECommerce.Tests.Features.Products;

public class GetAllProductsQueryHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly GetAllProductsQueryHandler _handler;

    public GetAllProductsQueryHandlerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _handler = new GetAllProductsQueryHandler(_mockProductRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product("Keyboard", "Mechanical", 100m, 10),
            new Product("Mouse", "Wireless", 50m, 20)
        };

        _mockProductRepository.Setup(repo =>
            repo.GetAllAsync())
            .ReturnsAsync(products);

        var query = new GetAllProductsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(2);
        result.Should().Contain(p => p.Name == "Keyboard");
        result.Should().Contain(p => p.Name == "Mouse");
    }
}
