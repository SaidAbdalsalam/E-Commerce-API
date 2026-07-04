using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities.Product.Events;

public sealed class ProductCreated : DomainEvent
{
    public Guid ProductId { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }

    public ProductCreated(Guid productId, string name, decimal price)
    {
        ProductId = productId;
        Name = name;
        Price = price;
    }
}

public sealed class ProductUpdated : DomainEvent
{
    public Guid ProductId { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }

    public ProductUpdated(Guid productId, string name, decimal price)
    {
        ProductId = productId;
        Name = name;
        Price = price;
    }
}

public sealed class ProductDeleted : DomainEvent
{
    public Guid ProductId { get; init; }

    public ProductDeleted(Guid productId) => ProductId = productId;
}

public sealed class ProductStockUpdated : DomainEvent
{
    public Guid ProductId { get; init; }
    public int StockQuantity { get; init; }

    public ProductStockUpdated(Guid productId, int stockQuantity)
    {
        ProductId = productId;
        StockQuantity = stockQuantity;
    }
}

public sealed class ProductActivated : DomainEvent
{
    public Guid ProductId { get; init; }

    public ProductActivated(Guid productId) => ProductId = productId;
}

public sealed class ProductDeactivated : DomainEvent
{
    public Guid ProductId { get; init; }

    public ProductDeactivated(Guid productId) => ProductId = productId;
}
