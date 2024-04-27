namespace TechShopServer.Services
{
    public interface IManageImage
    {
        Task<String> UploadFile(IFormFile _IFormFile);
        Task<(byte[], string, string)> DowloadFile(string FileName);
    }
}
