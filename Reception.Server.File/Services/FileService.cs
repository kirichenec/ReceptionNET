using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Reception.Server.File.Services
{
    public static class FileService
    {
        public static async Task<byte[]> GetDataAsync(this IFormFile value)
        {
            using var ms = new MemoryStream();
            await value.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}