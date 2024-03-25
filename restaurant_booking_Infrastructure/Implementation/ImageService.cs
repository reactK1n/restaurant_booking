using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using restaurant_booking_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace restaurant_booking_Infrastructure.Implementation
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _config;
        private readonly Cloudinary _cloudinary;
        public ImageService(IConfiguration config, Cloudinary cloudinary)
        {
            _config = config;
            _cloudinary = cloudinary;
        }
        public async Task<UploadResult> UploadAsync(IFormFile image)
        {
            long pictureSize = Convert.ToInt64(_config["PhotoSettings:Size"]);
            if (image.Length > pictureSize)
            {
                throw new ArgumentException("File size exceeded");
            }
            bool pictureFormat = false;



            var listOfImageExtensions = new List<string>() { ".jpg", ".png", ".jpeg" };

            foreach (var item in listOfImageExtensions)
            {
                if ((image.FileName.ToLower().EndsWith(item)))
                {
                    pictureFormat = true;
                    break;
                }
            }

            if (pictureFormat == false)
            {
                throw new ArgumentException("File format not supported");
            }

            var uploadResult = new ImageUploadResult();

            using (var imageStream = image.OpenReadStream())
            {
                string filename = Guid.NewGuid().ToString() + image.FileName;

                uploadResult = await _cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(filename + Guid.NewGuid().ToString(), imageStream),
                    PublicId = "Expense Attachment/" + filename,

                    Transformation = new Transformation().Crop("thumb").Gravity("face").Width(150)
                });
            }
            return uploadResult;
        }

        public async Task<DelResResult> DeleteResourcesAsync(string publicId)
        {
            var delParams = new DelResParams
            {
                PublicIds = new List<string> { publicId },
                All = true,
                KeepOriginal = false,
                Invalidate = true
            };

            DelResResult deletionResult = await _cloudinary.DeleteResourcesAsync(delParams);
            if (deletionResult.Error != null)
            {
                throw new ApplicationException($"" +
                    $"an error occured in method: " +
                    $"{nameof(DeleteResourcesAsync)}" +
                    $" class: {nameof(ImageService)}");
            }

            return deletionResult;
        }
    }
}
