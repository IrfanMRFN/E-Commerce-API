using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentAssertions;
using Moq;

namespace ECommerce.Tests.Features.Products;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        // Setup the Mock Repository
        _mockProductRepository = new Mock<IProductRepository>();

        // Pass the mock object into the handler
        _handler = new CreateProductCommandHandler(_mockProductRepository.Object);
    }

    [Fact]
    public async Task Handle_WithvalidCommand_ShouldAddProductAndReturnId()
    {
        // Arrange
        var command = new CreateProductCommand("Test Product", "Test Desc", 10m, 5);

        // Act
        var resultId = await _handler.Handle(command, CancellationToken.None);

        // Assert
        resultId.Should().NotBeEmpty();

        _mockProductRepository.Verify(repo =>
            repo.AddAsync(It.Is<Product>(p =>
                p.Name == command.Name && 
                p.Description == command.Description &&
                p.Price == command.Price && 
                p.StockQuantity == command.StockQuantity)),
            Times.Once);
    }
}
