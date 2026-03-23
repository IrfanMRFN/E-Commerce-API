using ECommerce.Domain.Entities;
using FluentAssertions;

namespace ECommerce.Tests.Entities;

public class ProductTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateProduct()
    {
        // Arrange
        var name = "Test Product";
        var description = "Test Description";
        var price = 99.99m;
        var stock = 10;

        // Act
        var product = new Product(name, description, price, stock);

        // Assert
        product.Name.Should().Be(name);
        product.Description.Should().Be(description);
        product.Price.Should().Be(price);
        product.StockQuantity.Should().Be(stock);
        product.Id.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("", "Valid Desc", 10, 5)]               // Invalid: Empty Name
    [InlineData("Valid Name", "Valid Desc", 0, 5)]      // Invalid: Zero Price
    [InlineData("Valid Name", "Valid Desc", -1, 5)]     // Invalid: Negative Price
    [InlineData("Valid Name", "Valid Desc", 10, -5)]    // Invalid: Negative Stock
    public void Constructor_WithInvalidData_ShouldThrowArgumentException(string name, string description, decimal price, int stock)
    {
        // Arrange & Act
        Action act = () => new Product(name, description, price, stock);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DeductStock_WithValidQuantity_ShouldDecreaseStock()
    {
        // Arrange
        var product = new Product("Test", "Desc", 10m, 50);
        var amountToDeduct = 10;

        // Act
        product.DeductStock(amountToDeduct);

        // Assert
        product.StockQuantity.Should().Be(40);
    }

    [Fact]
    public void DeductStock_WithQuantityGreaterThanStock_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var product = new Product("Test", "Desc", 10m, 10);
        var amountToDeduct = 15;

        // Act
        Action act = () => product.DeductStock(amountToDeduct);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void DeductStock_WithInvalidQuantity_ShouldThrowArgumentException(int quantity)
    {
        // Arrange
        var product = new Product("Test", "Desc", 10m, 50);

        // Act
        Action act = () => product.DeductStock(quantity);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void AddStock_WithValidQuantity_ShouldIncreaseStock()
    {
        // Arrange
        var product = new Product("Test", "Desc", 10m, 50);
        var amountToAdd = 20;

        // Act
        product.AddStock(amountToAdd);

        // Assert
        product.StockQuantity.Should().Be(70);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void AddStock_WithInvalidQuantity_ShouldThrowArgumentException(int quantity)
    {
        // Arrange
        var product = new Product("Test", "Desc", 10m, 50);

        // Act
        Action act = () => product.AddStock(quantity);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateProperties()
    {
        // Arrange
        var product = new Product("Old Name", "Old Desc", 10m, 5);

        // Act
        product.Update("New Name", "New Desc", 20m, 15);

        // Assert
        product.Name.Should().Be("New Name");
        product.Description.Should().Be("New Desc");
        product.Price.Should().Be(20m);
        product.StockQuantity.Should().Be(15);
    }

    [Theory]
    [InlineData("", "Valid Desc", 10, 5)]               // Invalid: Empty Name
    [InlineData("Valid Name", "Valid Desc", 0, 5)]      // Invalid: Zero Price
    [InlineData("Valid Name", "Valid Desc", -1, 5)]     // Invalid: Negative Price
    [InlineData("Valid Name", "Valid Desc", 10, -5)]    // Invalid: Negative Stock
    public void Update_WithInvalidData_ShouldThrowArgumentException(string name, string description, decimal price, int stock)
    {
        // Arrange
        var product = new Product("Valid Name", "Desc", 10m, 5);

        // Act
        Action act = () => product.Update(name, description, price, stock);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}
