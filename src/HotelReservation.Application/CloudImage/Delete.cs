using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HotelReservation.Application.CloudImage.Contracts;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace HotelReservation.Application.CloudImage;
public class Delete(ICloudinary cloudinary) : IDelete
{
    public async Task<Result> DeleteImage(string imageUrl)
    {
        var publicIdResult = ExtractPublicIdFromUrl(imageUrl);
        if(publicIdResult.IsFailure)
            return Result.Failure(publicIdResult.Errors, publicIdResult.StatusCode);

        var deleteParams = new DeletionParams(publicIdResult.Value);
        var result = await cloudinary.DestroyAsync(deleteParams);

        if (result.StatusCode != HttpStatusCode.OK)
            return Result.Failure(
                ["Failed to delete image from cloudinary."], 
                (int)result.StatusCode);

        return Result.Success();
    }

    private static Result<string> ExtractPublicIdFromUrl(string imageUrl)
    {
        var uri = new Uri(imageUrl);
        var segments = uri.AbsolutePath.Split("/", StringSplitOptions.RemoveEmptyEntries);
        int uploadIndex = Array.IndexOf(segments, "upload");
        if (uploadIndex == -1 || uploadIndex + 1 >= segments.Length)
            return Result<string>.Failure(new List<string>
            { "Invalid cloudinary Url Format"}, StatusCodes.Status400BadRequest);

        var pathSegments = segments.Skip(uploadIndex + 2).ToArray();

        var fileNameWithoutExtention = Path.GetFileNameWithoutExtension(pathSegments[^1]);
        pathSegments[^1] = fileNameWithoutExtention;

        var publicId = string.Join("/", pathSegments);
        return Result<string>.Success(publicId);
    }
}
