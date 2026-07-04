using ECommerce.Domain.Common.Results;

namespace ECommerce.Domain.Entities.Category;

public static class CategoryErrors
{
    public static Error NameIsRequired =>
        Error.Validation("Category_Name_Required", "Category name is required.");

    public static Error ParentCategoryIdIsRequired =>
        Error.Validation("Category_ParentCategoryId_Required", "Parent category ID is required.");
}
