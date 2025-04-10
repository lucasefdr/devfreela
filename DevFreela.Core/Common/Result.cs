namespace DevFreela.Core.Common;

public class Result
{
    protected bool IsSuccess { get; }
    public Error? Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, Error? error)
    {
        switch (isSuccess)
        {
            case true when error != null:
                throw new InvalidOperationException("A successful result cannot contain error.");
            case false when error == null:
                throw new InvalidOperationException("A failure result must contain an error.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    // Métodos para resultados que não precisam de um retorno
    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);

    // Métodos para resultados que precisam de um retorno
    public static Result<T> Success<T>(T value) => new(value, true, null);
    public static Result<T> Failure<T>(Error error) => new(default!, false, error);
}

public class Result<T> : Result
{
    private readonly T _value;
    public T Value => IsSuccess ? _value : throw new InvalidOperationException("Não há valor para resultados falhos.");

    protected internal Result(T value, bool isSuccess, Error? error)
        : base(isSuccess, error)
    {
        _value = value;
    }
}