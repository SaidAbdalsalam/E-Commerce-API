using ECommerce.Domain.Common.Results;

namespace ECommerce.Domain.Entities.Cart;

public static class CartErrors
{
    public static Error CustomerIdIsRequired =>
        Error.Validation("Cart_CustomerId_Required", "CustomerId is required.");

    public static Error ItemNotFound =>
        Error.NotFound("Cart_Item_NotFound", "Requested item was not found in the cart.");

    public static Error ExceedsStockQuantity =>
        Error.Conflict(
            "Cart_Exceeds_Stock",
            "Cannot add more items than currently available in stock."
        );
}
