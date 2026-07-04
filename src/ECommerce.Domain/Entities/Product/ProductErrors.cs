using ECommerce.Domain.Common.Results;

namespace ECommerce.Domain.Entities.Product;

public static class ProductErrors
{
    public static Error NameIsRequired =>
        Error.Validation("Product_NameIsRequired", "Product name is required.");

    public static Error PriceCannotBeNegative =>
        Error.Validation("Product_PriceCannotBeNegative", "Product price cannot be negative.");

    public static Error StockQuantityCannotBeNegative =>
        Error.Validation(
            "Product_StockQuantityCannotBeNegative",
            "Product stock quantity cannot be negative."
        );

    public static Error ProductAlreadyActive =>
        Error.Conflict("Product_AlreadyActive", "Product is already active.");

    public static Error ProductNotActive =>
        Error.Conflict("Product_NotActive", "Product is not active.");

    public static Error CategoryIdIsRequired =>
        Error.Validation("Product_CategoryIdIsRequired", "Product category ID is required.");

    public static Error InvalidQuantity =>
        Error.Validation("Product_InvalidQuantity", "Product quantity is invalid.");

    public static Error OutOfStock =>
        Error.Conflict("Product_OutOfStock", "Product is out of stock.");
}
