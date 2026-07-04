using ECommerce.Domain.Common;
using ECommerce.Domain.Common.Results;

namespace ECommerce.Domain.Entities.Customer.Address;

public sealed class CustomerAddress : AuditableEntity
{
    public Guid CustomerId { get; private set; }
    public string Label { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public bool IsDefault { get; private set; }

    private CustomerAddress() { }

    private CustomerAddress(
        Guid customerId,
        string label,
        string street,
        string city,
        string postalCode,
        string country,
        bool isDefault
    )
    {
        CustomerId = customerId;
        Label = string.IsNullOrWhiteSpace(label) ? "Address" : label.Trim();
        Street = street.Trim();
        City = city.Trim();
        PostalCode = postalCode.Trim();
        Country = country.Trim();
        IsDefault = isDefault;
    }

    internal static Result<CustomerAddress> Create(
        Guid customerId,
        string label,
        string street,
        string city,
        string postalCode,
        string country,
        bool isDefault
    )
    {
        if (string.IsNullOrWhiteSpace(street))
            return CustomerAddressError.StreetRequired;
        if (string.IsNullOrWhiteSpace(city))
            return CustomerAddressError.CityRequired;
        if (string.IsNullOrWhiteSpace(country))
            return CustomerAddressError.CountryRequired;

        return new CustomerAddress(customerId, label, street, city, postalCode, country, isDefault);
    }

    internal Result<Updated> Update(
        string label,
        string street,
        string city,
        string postalCode,
        string country,
        bool isDefault
    )
    {
        if (string.IsNullOrWhiteSpace(street))
            return CustomerAddressError.StreetRequired;
        if (string.IsNullOrWhiteSpace(city))
            return CustomerAddressError.CityRequired;
        if (string.IsNullOrWhiteSpace(country))
            return CustomerAddressError.CountryRequired;

        Label = string.IsNullOrWhiteSpace(label) ? "Address" : label.Trim();
        Street = street.Trim();
        City = city.Trim();
        PostalCode = postalCode.Trim();
        Country = country.Trim();
        IsDefault = isDefault;

        return Result.Updated;
    }

    internal void SetAsDefault(bool isDefault)
    {
        IsDefault = isDefault;
    }

    public bool IsEqualTo(string city, string street, string postalCode)
    {
        return City.Equals(city, StringComparison.OrdinalIgnoreCase)
            && Street.Equals(street, StringComparison.OrdinalIgnoreCase)
            && PostalCode.Equals(postalCode, StringComparison.OrdinalIgnoreCase);
    }
}
