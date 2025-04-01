using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace MoblieShop.Service.Cloudinary
{
    public class ImageService : IImageService
    {
        private readonly ICloudinary _cloudinary;

        public ImageService(ICloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> SaveImageToCloudinaryAsync(IFormFile imageFile)
        {
            try
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
                    PublicId = $"posts/{Guid.NewGuid()}"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.ToString();
                }
                else
                {
                    throw new Exception("Image upload failed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload image to Cloudinary.", ex);
            }
        }
    }
}
