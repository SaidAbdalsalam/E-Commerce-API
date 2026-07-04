using ECommerce.Domain.Common;
using ECommerce.Domain.Common.Results;
using ECommerce.Domain.Entities.Category.Events;

namespace ECommerce.Domain.Entities.Category;

public sealed class Category : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public Guid? ParentCategoryId { get; private set; }

    private Category() { }

    private Category(string name, string? description, Guid? parentCategoryId)
    {
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
    }

    public static Result<Category> Create(string name, string? description, Guid? parentCategoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return CategoryErrors.NameIsRequired;
        }

        var category = new Category(name.Trim(), description?.Trim(), parentCategoryId);

        category.AddDomainEvent(new CategoryCreated(category.Id, category.Name));
        return category;
    }

    public Result<Updated> Update(string name, string? description, Guid? parentCategoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return CategoryErrors.NameIsRequired;
        }

        Name = name.Trim();
        Description = description?.Trim();
        ParentCategoryId = parentCategoryId;

        AddDomainEvent(new CategoryUpdated(Id, Name));
        return Result.Updated;
    }
}
