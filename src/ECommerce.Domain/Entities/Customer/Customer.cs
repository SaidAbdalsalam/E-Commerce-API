using System.Net.Mail;
using ECommerce.Domain.Common;
using ECommerce.Domain.Common.Results;
using ECommerce.Domain.Entities.Customer.Address;
using ECommerce.Domain.Entities.Customer.Events;

namespace ECommerce.Domain.Entities.Customer;

public sealed class Customer : AuditableEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<CustomerAddress> _addresses = [];
    public IReadOnlyList<CustomerAddress> Addresses => _addresses.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Customer() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Customer(string firstName, string lastName, string email, bool isActive)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        IsActive = isActive;
    }

    public static Result<Customer> Create(
        string firstName,
        string lastName,
        string email,
        bool isActive
    )
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return CustomerErrors.FirstNameIsRequired;
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return CustomerErrors.LastNameIsRequired;
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            return CustomerErrors.EmailIsRequired;
        }
        if (!CheckEmailFormat(email))
        {
            return CustomerErrors.EmailInvalid;
        }
        var customer = new Customer(firstName.Trim(), lastName.Trim(), email.Trim(), isActive);

        customer.AddDomainEvent(new CustomerRegistered(Guid.NewGuid(), email.Trim()));

        return customer;
    }

    public Result<Updated> UpdateProfile(
        string firstName,
        string lastName,
        string email,
        bool isActive
    )
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return CustomerErrors.FirstNameIsRequired;
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return CustomerErrors.LastNameIsRequired;
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            return CustomerErrors.EmailIsRequired;
        }
        if (!CheckEmailFormat(email))
        {
            return CustomerErrors.EmailInvalid;
        }

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        IsActive = isActive;

        AddDomainEvent(new CustomerProfileUpdated(Id, FirstName, LastName));
        return Result.Updated;
    }

    public Result<Updated> Deactivate()
    {
        if (!IsActive)
            return CustomerErrors.AlreadyInactive;

        IsActive = false;
        AddDomainEvent(new CustomerDeactivated(Id));
        return Result.Updated;
    }

    public Result<Updated> Activate()
    {
        if (IsActive)
            return CustomerErrors.AlreadyActive;

        IsActive = true;
        AddDomainEvent(new CustomerActivated(Id));
        return Result.Updated;
    }

    public Result<Updated> AddAddress(
        string label,
        string street,
        string city,
        string postalCode,
        string country,
        bool isDefault
    )
    {
        var addressResult = CustomerAddress.Create(
            Id,
            label,
            street,
            city,
            postalCode,
            country,
            isDefault
        );
        if (addressResult.IsError)
            return addressResult.TopError;

        if (_addresses.Any(a => a.IsEqualTo(city, street, postalCode)))
        {
            return CustomerErrors.AddressIsAlreadyExists;
        }

        var newAddress = addressResult.Value;

        if (isDefault)
            EnsureOnlyOneDefaultAddress();
        else if (_addresses.Count == 0)
            newAddress.SetAsDefault(true);

        _addresses.Add(newAddress);
        AddDomainEvent(new CustomerAddressAdded(Id, newAddress.Id));
        return Result.Updated;
    }

    public Result<Updated> UpdateAddress(
        Guid addressId,
        string label,
        string street,
        string city,
        string postalCode,
        string country,
        bool isDefault
    )
    {
        var existingAddress = _addresses.FirstOrDefault(a => a.Id == addressId);
        if (existingAddress is null)
        {
            return CustomerErrors.AddressNotFound;
        }

        if (_addresses.Any(a => a.Id != addressId && a.IsEqualTo(city, street, postalCode)))
        {
            return CustomerErrors.AddressIsAlreadyExists;
        }

        bool wasDefault = existingAddress.IsDefault;

        var updateResult = existingAddress.Update(
            label,
            street,
            city,
            postalCode,
            country,
            isDefault
        );
        if (updateResult.IsError)
            return updateResult.TopError;

        if (isDefault)
        {
            EnsureOnlyOneDefaultAddress(addressId);
        }
        else if (wasDefault && _addresses.Count > 1)
        {
            existingAddress.SetAsDefault(true);
        }

        AddDomainEvent(new CustomerAddressUpdated(Id, existingAddress.Id));
        return Result.Updated;
    }

    public Result<Updated> SetDefaultAddress(Guid addressId)
    {
        var targetAddress = _addresses.FirstOrDefault(a => a.Id == addressId);
        if (targetAddress is null)
        {
            return CustomerErrors.AddressNotFound;
        }

        EnsureOnlyOneDefaultAddress(addressId);
        AddDomainEvent(new CustomerDefaultAddressChanged(Id, targetAddress.Id));
        return Result.Updated;
    }

    public Result<Deleted> RemoveAddress(Guid addressId)
    {
        var addressToRemove = _addresses.FirstOrDefault(a => a.Id == addressId);
        if (addressToRemove is null)
        {
            return CustomerErrors.AddressNotFound;
        }

        _addresses.Remove(addressToRemove);

        if (addressToRemove.IsDefault && _addresses.Count > 0)
        {
            _addresses.First().SetAsDefault(true);
            AddDomainEvent(new CustomerDefaultAddressChanged(Id, _addresses.First().Id));
        }
        AddDomainEvent(new CustomerAddressRemoved(Id, addressToRemove.Id));
        return Result.Deleted;
    }

    private void EnsureOnlyOneDefaultAddress(Guid? exceptionAddressId = null)
    {
        foreach (var address in _addresses)
        {
            if (exceptionAddressId.HasValue && address.Id == exceptionAddressId.Value)
            {
                address.SetAsDefault(true);
            }
            else
            {
                address.SetAsDefault(false);
            }
        }
    }

    private static bool CheckEmailFormat(string email)
    {
        try
        {
            _ = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
