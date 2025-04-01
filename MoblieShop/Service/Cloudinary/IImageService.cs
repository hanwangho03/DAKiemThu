namespace MoblieShop.Service.Cloudinary
{
    public interface IImageService
    {
        Task<string> SaveImageToCloudinaryAsync(IFormFile imageFile);
    }
}
