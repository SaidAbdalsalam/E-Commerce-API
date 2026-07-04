using ECommerce.Domain.Common;
using ECommerce.Domain.Common.Results;
using ECommerce.Domain.Entities.Product.Events;

namespace ECommerce.Domain.Entities.Product;

public sealed class Product : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public Guid CategoryId { get; private set; }
    public bool IsActive { get; private set; }

    private Product() { }

    private Product(
        string name,
        string description,
        decimal price,
        int stockQuantity,
        Guid categoryId,
        bool isActive
    )
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        CategoryId = categoryId;
        IsActive = isActive;
    }

    public static Result<Product> Create(
        string name,
        string description,
        decimal price,
        int stockQuantity,
        Guid categoryId,
        bool isActive
    )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return ProductErrors.NameIsRequired;
        }
        if (price < 0)
        {
            return ProductErrors.PriceCannotBeNegative;
        }
        if (stockQuantity < 0)
        {
            return ProductErrors.StockQuantityCannotBeNegative;
        }
        if (categoryId == Guid.Empty)
        {
            return ProductErrors.CategoryIdIsRequired;
        }

        var product = new Product(
            name.Trim(),
            description.Trim(),
            price,
            stockQuantity,
            categoryId,
            isActive
        );

        product.AddDomainEvent(new ProductCreated(product.Id, product.Name, product.Price));
        return product;
    }

    public Result<Updated> Update(
        string name,
        string description,
        decimal price,
        int stockQuantity,
        Guid categoryId,
        bool isActive
    )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return ProductErrors.NameIsRequired;
        }
        if (price < 0)
        {
            return ProductErrors.PriceCannotBeNegative;
        }
        if (stockQuantity < 0)
        {
            return ProductErrors.StockQuantityCannotBeNegative;
        }
        if (categoryId == Guid.Empty)
        {
            return ProductErrors.CategoryIdIsRequired;
        }

        Name = name.Trim();
        Description = description.Trim();
        Price = price;
        StockQuantity = stockQuantity;
        CategoryId = categoryId;
        IsActive = isActive;
        AddDomainEvent(new ProductUpdated(Id, Name, Price));
        return Result.Updated;
    }

    public Result<Updated> ReduceStock(int quantity)
    {
        if (quantity <= 0)
        {
            return ProductErrors.InvalidQuantity;
        }

        if (quantity > StockQuantity)
        {
            return ProductErrors.OutOfStock;
        }

        StockQuantity -= quantity;

        AddDomainEvent(new ProductStockUpdated(Id, StockQuantity));
        return Result.Updated;
    }

    public Result<Updated> Activate()
    {
        if (IsActive)
        {
            return ProductErrors.ProductAlreadyActive;
        }

        IsActive = true;
        AddDomainEvent(new ProductActivated(Id));
        return Result.Updated;
    }

    public Result<Updated> Deactivate()
    {
        if (!IsActive)
        {
            return ProductErrors.ProductNotActive;
        }

        IsActive = false;
        AddDomainEvent(new ProductDeactivated(Id));
        return Result.Updated;
    }
}
