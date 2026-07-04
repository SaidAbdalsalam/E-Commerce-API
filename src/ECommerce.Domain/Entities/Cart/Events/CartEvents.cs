using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities.Cart.Events;

public sealed class CartCreated : DomainEvent
{
    public Guid CartId { get; init; }
    public Guid CustomerId { get; init; }

    public CartCreated(Guid cartId, Guid customerId)
    {
        CartId = cartId;
        CustomerId = customerId;
    }
}

public sealed class CartItemAdded : DomainEvent
{
    public Guid CartId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }

    public CartItemAdded(Guid cartId, Guid productId, int quantity)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }
}

public sealed class CartItemQuantityUpdated : DomainEvent
{
    public Guid CartId { get; init; }
    public Guid ProductId { get; init; }
    public int NewQuantity { get; init; }

    public CartItemQuantityUpdated(Guid cartId, Guid productId, int newQuantity)
    {
        CartId = cartId;
        ProductId = productId;
        NewQuantity = newQuantity;
    }
}

public sealed class CartItemRemoved : DomainEvent
{
    public Guid CartId { get; init; }
    public Guid ProductId { get; init; }

    public CartItemRemoved(Guid cartId, Guid productId)
    {
        CartId = cartId;
        ProductId = productId;
    }
}

public sealed class CartCleared : DomainEvent
{
    public Guid CartId { get; init; }

    public CartCleared(Guid cartId)
    {
        CartId = cartId;
    }
}
