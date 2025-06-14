﻿namespace HotelReservation.API;
public class ErrorResponse
{
    public int ErrorCode { get; set; }
    public string? Message { get; set; }
    public List<string> Errors { get; set; } = new();
}
