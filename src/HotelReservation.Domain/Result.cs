using Microsoft.AspNetCore.Http;

namespace HotelReservation.Domain;
public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public List<string> Errors { get; }
    public int StatusCode { get; }
    public string Code { get; }

    private Result(bool isSuccess, T? value, List<string> errors, int statusCode, string code)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
        StatusCode = statusCode;
        Code = code;
    }

    public static Result<T> Success(T value, int statusCode = StatusCodes.Status200OK, string code = "") => 
        new(true, value, [], statusCode, code);
    public static Result<T> Failure(List<string> errors, 
        int statusCode = StatusCodes.Status400BadRequest, string code = "") => 
        new(false, default, errors, statusCode, code);
}

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<string> Errors { get; }
    public int StatusCode { get; }
    public string Code { get; }

    private Result(bool isSuccess, List<string> errors, int statusCode, string code)
    {
        IsSuccess = isSuccess;
        Errors = errors;
        StatusCode = statusCode;
        Code = code;
    }

    public static Result Success(int statusCode = StatusCodes.Status200OK, 
        string code = "") => 
        new(true, [], statusCode, code);
    public static Result Failure(List<string> errors, 
        int statusCode = StatusCodes.Status400BadRequest, string code = "") => 
        new(false, errors, statusCode, code);
}
