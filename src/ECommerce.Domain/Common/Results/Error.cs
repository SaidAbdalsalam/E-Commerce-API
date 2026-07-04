namespace ECommerce.Domain.Common.Results;

public readonly record struct Error
{
    public string Code { get; init; }

    public string Message { get; init; }

    public ErrorKind Kind { get; init; }

    public Error(string code, string message, ErrorKind kind)
    {
        Code = code;
        Message = message;
        Kind = kind;
    }

    public static Error Failure(
        string code = nameof(Failure),
        string message = "General failure"
    ) => new(code, message, ErrorKind.Failure);

    public static Error NotFound(
        string code = nameof(NotFound),
        string message = "Resource not found"
    ) => new(code, message, ErrorKind.NotFound);

    public static Error Validation(
        string code = nameof(Validation),
        string message = "Validation failed"
    ) => new(code, message, ErrorKind.Validation);

    public static Error Conflict(
        string code = nameof(Conflict),
        string message = "Conflict detected"
    ) => new(code, message, ErrorKind.Conflict);

    public static Error Unauthorized(
        string code = nameof(Unauthorized),
        string message = "Unauthorized access"
    ) => new(code, message, ErrorKind.Unauthorized);

    public static Error Forbidden(
        string code = nameof(Forbidden),
        string message = "Access forbidden"
    ) => new(code, message, ErrorKind.Forbidden);

    public static Error Internal(
        string code = nameof(Internal),
        string message = "Internal server error"
    ) => new(code, message, ErrorKind.Internal);
}
