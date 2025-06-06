using Microsoft.AspNetCore.Http;

namespace HotelReservation.Domain;
public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public List<string> Errors { get; }
    public int StatusCode { get; }

    private Result(bool isSuccess, T? value, List<string> errors, int statusCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
        StatusCode = statusCode;
    }

    public static Result<T> Success(T value, int statusCode = StatusCodes.Status200OK) => 
        new(true, value, [], statusCode);
    public static Result<T> Failure(List<string> errors, int statusCode = StatusCodes.Status400BadRequest) => 
        new(false, default, errors, statusCode);
}

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<string> Errors { get; }
    public int StatusCode { get; }

    private Result(bool isSuccess, List<string> errors, int statusCode)
    {
        IsSuccess = isSuccess;
        Errors = errors;
        StatusCode = statusCode;
    }

    public static Result Success(int statusCode = StatusCodes.Status200OK) => 
        new(true, [], statusCode);
    public static Result Failure(List<string> errors, int statusCode = StatusCodes.Status400BadRequest) => 
        new(false, errors, statusCode);
}
