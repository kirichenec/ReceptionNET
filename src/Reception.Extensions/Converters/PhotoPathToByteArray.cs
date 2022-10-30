using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.Extension.Converters
{
    public static class FilePathToByteArrayConverter
    {
        public static async Task<byte[]> GetFileBytesByPathAsync(this string filePath, CancellationToken cancellationToken = default)
        {
            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }
    }
}
