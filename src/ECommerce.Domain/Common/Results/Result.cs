using System.ComponentModel;
using System.Text.Json.Serialization;
using ECommerce.Domain.Common.Results.Abstraction;

namespace ECommerce.Domain.Common.Results;

public readonly record struct Success;

public readonly record struct Updated;

public readonly record struct Deleted;

public readonly record struct Created;

public class Result
{
    public static Success Success = default;
    public static Updated Updated = default;
    public static Deleted Deleted = default;
    public static Created Created = default;
}

public class Result<TValue> : IResult<TValue>
{
    private readonly List<Error> _errors = [];
    private readonly TValue? _value = default!;

    public bool IsSuccess { get; }

    public bool IsError => !IsSuccess;

    public IReadOnlyCollection<Error> Errors => IsError ? _errors.AsReadOnly() : Array.Empty<Error>();

    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("Cannot access value of a failed result.");

    public Error TopError => _errors.Count > 0 ? _errors[0] : throw new InvalidOperationException("Cannot access top error of a successful result.");


[JsonConstructor]
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("For serializer only.", true)]
public Result(TValue? value, List<Error>? errors, bool isSuccess)
{
    if (isSuccess)
        {
            if (value is null)
    {
        throw new ArgumentNullException(nameof(value));
    }
    _value = value; 
            _errors = [];
            IsSuccess = true;
        }
        else
        {
            if (errors == null || errors.Count == 0)
            {
                throw new ArgumentException("Provide at least one error.", nameof(errors));
            }

            _errors = errors;
            _value = default!;
            IsSuccess = false;
        }
}

private  Result(Error error)
{
    IsSuccess  =false;
    _errors = [error];
}

private  Result(List<Error> errors)
{
     if (errors is null || errors.Count == 0)
        {
            throw new ArgumentException(
                "Cannot create an ErrorOr<TValue> from an empty collection of errors. Provide at least one error.",
                nameof(errors)
            );
        }
    IsSuccess  =false;
    
    _errors =  errors;
}

private  Result(TValue value)
{
    if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
    IsSuccess  =true;
  _value = value;
}


public TValueNext Match <TValueNext>(Func<TValue, TValueNext> OnValue, Func<IReadOnlyCollection<Error>, TValueNext>OnError)=> IsSuccess ? OnValue(_value!) : OnError(_errors);

public static implicit operator Result<TValue>(Error error)=> new (error);
public static implicit operator Result<TValue>(List<Error> errors)=> new (errors);
public static implicit operator Result<TValue>(TValue value)=> new (value);
}
