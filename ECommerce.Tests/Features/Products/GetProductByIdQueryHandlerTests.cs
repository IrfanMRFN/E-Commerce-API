using ECommerce.Application.Features.Products.Queries.GetProductById;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentAssertions;
using Moq;

namespace ECommerce.Tests.Features.Products;

public class GetProductByIdQueryHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdQueryHandlerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _handler = new GetProductByIdQueryHandler(_mockProductRepository.Object);
    }

    [Fact]
    public async Task Handle_WithValidId_ShouldReturnProduct()
    {
        // Arrange
        var product = new Product("Test Product", "Test Desc", 99.99m, 10);

        _mockProductRepository.Setup(repo =>
            repo.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        var query = new GetProductByIdQuery(product.Id);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(product.Id);
        result.Name.Should().Be(product.Name);
        result.Description.Should().Be(product.Description);
        result.Price.Should().Be(product.Price);
        result.StockQuantity.Should().Be(product.StockQuantity);
    }

    [Fact]
    public async Task Handle_WithInvalidId_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _mockProductRepository.Setup(repo =>
            repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product?)null);
            
        var query = new GetProductByIdQuery(Guid.NewGuid());

        // Act
        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}