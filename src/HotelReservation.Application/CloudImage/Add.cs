using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HotelReservation.Application.CloudImage.Contracts;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace HotelReservation.Application.CloudImage;
public class Add(ICloudinary cloudinary) : IAdd
{
    public async Task<Result<string>> UploadImage(IFormFile image, string folder)
    {
        await using var stream = image.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(image.FileName, stream),
            Folder = folder
        };

        var result = await cloudinary.UploadAsync(uploadParams);

        if(result.StatusCode != HttpStatusCode.OK)
            return Result<string>.Failure(new List<string> 
            { "Image upload failed." }, 
            (int)result.StatusCode);
        
        return Result<string>.Success(result.Url.ToString());
    }
}
