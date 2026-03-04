namespace ResumeSmart.Api.Models;

/// <summary>
/// Represent result object
/// </summary>
public record Result
{
    /// <summary>
    /// Gets is success value
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets error value
    /// </summary>
    public Error? Error { get; }

    /// <summary>
    /// Initialize an instance of result
    /// </summary>
    /// <param name="isSuccess">Is success value</param>
    /// <param name="error">Error value</param>
    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Success result  
    /// </summary>
    /// <returns>Returns an instance of success result</returns>
    public static Result Success() => new(true, null);

    /// <summary>
    /// Failure result
    /// </summary>
    /// <param name="error">Error value</param>
    /// <returns>Returns an instance of failure result</returns>
    /// <exception cref="ArgumentNullException">Throws argument null exception if error value is null</exception>
    private static Result Failure(Error error) => new(false,
        error ?? throw new ArgumentNullException(nameof(error)));

    /// <summary>
    /// Failure result
    /// </summary>
    /// <param name="error">Error value</param>
    /// <returns>Returns an instance of result object</returns>
    public static implicit operator Result(Error error) => Failure(error);
}

/// <summary>
/// Represents typed result object
/// </summary>
/// <typeparam name="T"></typeparam>
public record Result<T> : Result
{
    /// <summary>
    /// Gets typed value
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Initialize success result
    /// </summary>
    /// <param name="value">Typed value</param>
    private Result(T value) : base(true, null) => Value = value;

    /// <summary>
    /// Initialize error result
    /// </summary>
    /// <param name="error">Error value</param>
    private Result(Error error) : base(false, error)
    {
    }

    /// <summary>
    /// Create success result
    /// </summary>
    /// <param name="value">Typed value</param>
    /// <returns>Returns success result</returns>
    public static implicit operator Result<T>(T value) => new(value);

    /// <summary>
    /// Create failure result
    /// </summary>
    /// <param name="error">Error value</param>
    /// <returns>Returns failure result</returns>
    public static implicit operator Result<T>(Error error) => new(error);
}