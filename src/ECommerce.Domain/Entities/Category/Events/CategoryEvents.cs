using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities.Category.Events;

public sealed class CategoryCreated : DomainEvent
{
    public Guid CategoryId { get; init; }
    public string Name { get; init; }

    public CategoryCreated(Guid categoryId, string name)
    {
        CategoryId = categoryId;
        Name = name;
    }
}

public sealed class CategoryUpdated : DomainEvent
{
    public Guid CategoryId { get; init; }
    public string Name { get; init; }

    public CategoryUpdated(Guid categoryId, string name)
    {
        CategoryId = categoryId;
        Name = name;
    }
}

public sealed class CategoryDeleted : DomainEvent
{
    public Guid CategoryId { get; init; }

    public CategoryDeleted(Guid categoryId) => CategoryId = categoryId;
}
