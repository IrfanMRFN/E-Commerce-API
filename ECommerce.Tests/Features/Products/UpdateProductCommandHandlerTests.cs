using ECommerce.Application.Features.Products.Commands.UpdateProduct;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentAssertions;
using Moq;

namespace ECommerce.Tests.Features.Products;

public class UpdateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductCommandHandlerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _handler = new UpdateProductCommandHandler(_mockProductRepository.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldUpdateProduct()
    {
        // Arrange
        var product = new Product("Test", "Desc", 10m, 5);

        _mockProductRepository.Setup(repo =>
            repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        var command = new UpdateProductCommand(product.Id, "New Name", "New Desc", 15m, 10);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockProductRepository.Verify(repo =>
            repo.UpdateAsync(It.Is<Product>(p =>
                p.Name == command.Name &&
                p.Description == command.Description &&
                p.Price == command.Price &&
                p.StockQuantity == command.StockQuantity)),
                Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidCommand_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _mockProductRepository.Setup(repo =>
            repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product?)null);

        var command = new UpdateProductCommand(Guid.NewGuid(), "New Name", "New Desc", 15m, 10);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
        _mockProductRepository.Verify(repo =>
            repo.UpdateAsync(It.IsAny<Product>()),
            Times.Never);
    }
}
