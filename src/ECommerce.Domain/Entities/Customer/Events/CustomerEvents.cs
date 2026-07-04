using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities.Customer.Events;

public sealed class CustomerRegistered : DomainEvent
{
    public Guid CustomerId { get; init; }
    public string Email { get; init; }

    public CustomerRegistered(Guid customerId, string email)
    {
        CustomerId = customerId;
        Email = email;
    }
}

public sealed class CustomerProfileUpdated : DomainEvent
{
    public Guid CustomerId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }

    public CustomerProfileUpdated(Guid customerId, string firstName, string lastName)
    {
        CustomerId = customerId;
        FirstName = firstName;
        LastName = lastName;
    }
}

public sealed class CustomerDeactivated : DomainEvent
{
    public Guid CustomerId { get; init; }

    public CustomerDeactivated(Guid customerId) => CustomerId = customerId;
}

public sealed class CustomerActivated : DomainEvent
{
    public Guid CustomerId { get; init; }

    public CustomerActivated(Guid customerId) => CustomerId = customerId;
}

public sealed class CustomerAddressAdded : DomainEvent
{
    public Guid CustomerId { get; init; }
    public Guid AddressId { get; init; }

    public CustomerAddressAdded(Guid customerId, Guid addressId)
    {
        CustomerId = customerId;
        AddressId = addressId;
    }
}

public sealed class CustomerAddressUpdated : DomainEvent
{
    public Guid CustomerId { get; init; }
    public Guid AddressId { get; init; }

    public CustomerAddressUpdated(Guid customerId, Guid addressId)
    {
        CustomerId = customerId;
        AddressId = addressId;
    }
}

public sealed class CustomerDefaultAddressChanged : DomainEvent
{
    public Guid CustomerId { get; init; }
    public Guid NewDefaultAddressId { get; init; }

    public CustomerDefaultAddressChanged(Guid customerId, Guid newDefaultAddressId)
    {
        CustomerId = customerId;
        NewDefaultAddressId = newDefaultAddressId;
    }
}

public sealed class CustomerAddressRemoved : DomainEvent
{
    public Guid CustomerId { get; init; }
    public Guid AddressId { get; init; }

    public CustomerAddressRemoved(Guid customerId, Guid addressId)
    {
        CustomerId = customerId;
        AddressId = addressId;
    }
}
