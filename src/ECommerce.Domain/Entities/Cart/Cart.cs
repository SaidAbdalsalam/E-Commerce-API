using ECommerce.Domain.Common;
using ECommerce.Domain.Common.Results;
using ECommerce.Domain.Entities.Cart.CartItems;
using ECommerce.Domain.Entities.Cart.Events;

namespace ECommerce.Domain.Entities.Cart;

public sealed class Cart : AuditableEntity
{
    public Guid CustomerId { get; private set; }
    private readonly List<CartItem> _items = [];
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    private Cart() { }

    private Cart(Guid customerId)
    {
        CustomerId = customerId;
    }

    public static Result<Cart> Create(Guid customerId)
    {
        if (customerId == Guid.Empty)
        {
            return CartErrors.CustomerIdIsRequired;
        }

        var cart = new Cart(customerId);
        cart.AddDomainEvent(new CartCreated(cart.Id, cart.CustomerId));
        return cart;
    }

    public Result<Updated> AddItem(Guid productId, int quantity, int stockQuantity)
    {
        if (quantity <= 0)
            return CartItemErrors.QuantityMustBeGreaterThanZero;

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

        int newTotalQuantity = (existingItem?.Quantity ?? 0) + quantity;

        if (newTotalQuantity > stockQuantity)
        {
            return CartErrors.ExceedsStockQuantity;
        }

        if (existingItem is not null)
        {
            existingItem.UpdateQuantity(newTotalQuantity);
        }
        else
        {
            var itemResult = CartItem.Create(productId, Id, quantity);
            if (itemResult.IsError)
                return itemResult.TopError;

            _items.Add(itemResult.Value);
        }
        AddDomainEvent(new CartItemAdded(Id, productId, quantity));

        return Result.Updated;
    }

    public Result<Updated> UpdateItemQuantity(Guid productId, int newQuantity, int stockQuantity)
    {
        if (newQuantity <= 0)
            return CartItemErrors.QuantityMustBeGreaterThanZero;

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
        if (existingItem is null)
        {
            return CartErrors.ItemNotFound;
        }

        if (newQuantity > stockQuantity)
        {
            return CartErrors.ExceedsStockQuantity;
        }

        existingItem.UpdateQuantity(newQuantity);
        AddDomainEvent(new CartItemQuantityUpdated(Id, productId, newQuantity));
        return Result.Updated;
    }

    public Result<Deleted> RemoveItem(Guid productId)
    {
        var itemToRemove = _items.FirstOrDefault(i => i.ProductId == productId);
        if (itemToRemove is null)
        {
            return CartErrors.ItemNotFound;
        }

        _items.Remove(itemToRemove);
        AddDomainEvent(new CartItemRemoved(Id, productId));
        return Result.Deleted;
    }

    public Result<Updated> Clear()
    {
        _items.Clear();
        AddDomainEvent(new CartCleared(Id));
        return Result.Updated;
    }
}
