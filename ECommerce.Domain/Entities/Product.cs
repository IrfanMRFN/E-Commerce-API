namespace ECommerce.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }

    private Product() {}

    public Product(string name, string description, decimal price, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.");
        
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        if (stockQuantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative.");

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    // --- Domain behaviors ---

    public void Update(string name, string description, decimal price, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.");

        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        if (stockQuantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative.");

        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public void DeductStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity to deduct must be greater than zero.");

        if (StockQuantity < quantity)
            throw new InvalidOperationException($"Insufficient stock for product {Name}. Available: {StockQuantity}");

        StockQuantity -= quantity;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity to add must be greater than zero.");

        StockQuantity += quantity;
    }
}
