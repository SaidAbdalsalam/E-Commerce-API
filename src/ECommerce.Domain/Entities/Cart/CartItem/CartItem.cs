using ECommerce.Domain.Common;
using ECommerce.Domain.Common.Results;

namespace ECommerce.Domain.Entities.Cart.CartItems;

public sealed class CartItem : AuditableEntity
{
    public Guid ProductId { get; private set; }
    public Guid CartId { get; private set; }
    public int Quantity { get; private set; }

    private CartItem() { }

    private CartItem(Guid productId, Guid cartId, int quantity)
    {
        ProductId = productId;
        CartId = cartId;
        Quantity = quantity;
    }

    internal static Result<CartItem> Create(Guid productId, Guid cartId, int quantity)
    {
        if (productId == Guid.Empty)
        {
            return CartItemErrors.ProductIdIsRequired;
        }

        if (cartId == Guid.Empty)
        {
            return CartItemErrors.CartIdIsRequired;
        }

        if (quantity <= 0)
        {
            return CartItemErrors.QuantityMustBeGreaterThanZero;
        }

        return new CartItem(productId, cartId, quantity);
    }

    internal void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}
