namespace ECommerce.Domain.Common.Results.Abstraction;

public interface IResult
{
    bool IsSuccess { get; }

    IReadOnlyCollection<Error>? Errors { get; }
}

public interface IResult<TValue> : IResult
{
    TValue? Value { get; }
}
