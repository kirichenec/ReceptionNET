using System.IO;
using System.Threading.Tasks;

namespace Reception.Extension.Converters
{
    public static class FilePathToByteArrayConverter
    {
        public static async Task<byte[]> GetFileBytesByPathAsync(this string filePath)
        {
            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}