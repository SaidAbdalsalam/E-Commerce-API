using ECommerce.Domain.Common.Results;

namespace ECommerce.Domain.Entities.Cart.CartItems;

public static class CartItemErrors
{
    public static Error ProductIdIsRequired =>
        Error.Validation("CartItem_ProductId_Required", "ProductId is required.");

    public static Error CartIdIsRequired =>
        Error.Validation("CartItem_CartId_Required", "CartId is required.");

    public static Error QuantityMustBeGreaterThanZero =>
        Error.Validation("CartItem_Quantity_Invalid", "Quantity must be greater than zero.");
}
