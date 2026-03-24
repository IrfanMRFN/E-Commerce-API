using ECommerce.Application.Features.Products.Commands.DeleteProduct;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentAssertions;
using Moq;

namespace ECommerce.Tests.Features.Products;

public class DeleteProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandHandlerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _handler = new DeleteProductCommandHandler(_mockProductRepository.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldDeleteProduct()
    {
        // Arrange
        var product = new Product("Test", "Desc", 10m, 5);

        _mockProductRepository.Setup(repo =>
            repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        var command = new DeleteProductCommand(product.Id);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockProductRepository.Verify(repo =>
            repo.DeleteAsync(product.Id),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidCommand_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _mockProductRepository.Setup(repo =>
            repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product?)null);

        var command = new DeleteProductCommand(Guid.NewGuid());

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
        _mockProductRepository.Verify(repo =>
            repo.DeleteAsync(It.IsAny<Guid>()),
            Times.Never);
    }
}
