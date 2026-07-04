namespace ECommerce.Domain.Common.Results;

public enum ErrorKind
{
    Failure,
    NotFound,
    Validation,
    Conflict,
    Unauthorized,
    Forbidden,
    Internal,
}
