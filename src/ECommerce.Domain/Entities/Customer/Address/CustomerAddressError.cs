using ECommerce.Domain.Common.Results;

namespace ECommerce.Domain.Entities.Customer.Address;

public static class CustomerAddressError
{
    public static Error StreetRequired =>
        Error.Validation("Address_Street_Required", "Street field is required.");

    public static Error CityRequired =>
        Error.Validation("Address_City_Required", "City field is required.");

    public static Error CountryRequired =>
        Error.Validation("Address_Country_Required", "Country field is required.");
}
