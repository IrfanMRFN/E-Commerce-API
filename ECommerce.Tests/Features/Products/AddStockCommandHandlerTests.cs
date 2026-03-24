using ECommerce.Application.Features.Products.Commands.AddStock;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentAssertions;
using Moq;

namespace ECommerce.Tests.Features.Products;

public class AddStockCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly AddStockCommandHandler _handler;

    public AddStockCommandHandlerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _handler = new AddStockCommandHandler(_mockProductRepository.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldAddStockAndUpdate()
    {
        // Arrange
        var product = new Product("Test", "Desc", 10m, 50);

        _mockProductRepository.Setup(repo =>
            repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        var command = new AddStockCommand(product.Id, 10);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        product.StockQuantity.Should().Be(60);
        _mockProductRepository.Verify(repo =>
            repo.UpdateAsync(product),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidCommand_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _mockProductRepository.Setup(repo =>
            repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product?)null);
            
        var command = new AddStockCommand(Guid.NewGuid(), 10);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
        _mockProductRepository.Verify(repo =>
            repo.UpdateAsync(It.IsAny<Product>()),
            Times.Never);
    }
}
