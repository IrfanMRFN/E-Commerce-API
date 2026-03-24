using ECommerce.Application.Features.Products.Commands.DeductStock;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentAssertions;
using Moq;

namespace ECommerce.Tests.Application.Products.Commands;

public class DeductStockCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly DeductStockCommandHandler _handler;

    public DeductStockCommandHandlerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _handler = new DeductStockCommandHandler(_mockProductRepository.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldDeductStockAndUpdate()
    {
        // Arrange
        var product = new Product("Test", "Desc", 10m, 50);

        _mockProductRepository.Setup(repo =>
            repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        var command = new DeductStockCommand(product.Id, 10);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        product.StockQuantity.Should().Be(40);
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

        var command = new DeductStockCommand(Guid.NewGuid(), 10);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
        _mockProductRepository.Verify(repo =>
            repo.UpdateAsync(It.IsAny<Product>()),
            Times.Never);
    }
}
