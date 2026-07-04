using ECommerce.Domain.Common.Results;

namespace ECommerce.Domain.Entities.Customer;

public static class CustomerErrors
{
    public static Error IdIsRequired =>
        Error.Validation("Customer_Id_Required", "Customer ID is required.");

    public static Error FirstNameIsRequired =>
        Error.Validation("Customer_FirstName_Required", "First name is required.");

    public static Error LastNameIsRequired =>
        Error.Validation("Customer_LastName_Required", "Last name is required.");

    public static Error EmailIsRequired =>
        Error.Validation("Customer_Email_Required", "Email address is required.");

    public static Error EmailInvalid =>
        Error.Validation("Customer_Email_Invalid", "Provided email address format is invalid.");

    public static Error AddressFieldsRequired =>
        Error.Validation(
            "Customer_Address_FieldsRequired",
            "Street, City, and Country fields are required."
        );

    public static Error AddressNotFound =>
        Error.NotFound(
            "Customer_Address_NotFound",
            "The requested address was not found for this customer."
        );

    public static Error AddressIsAlreadyExists =>
        Error.Conflict(
            "Customer_Address_AlreadyExists",
            "This identical address has already been saved in your profile."
        );
    public static Error AlreadyInactive =>
        Error.Conflict(
            "Customer_Account_AlreadyInactive",
            "This customer account is already deactivated."
        );

    public static Error AlreadyActive =>
        Error.Conflict(
            "Customer_Account_AlreadyActive",
            "This customer account is already active."
        );
}
