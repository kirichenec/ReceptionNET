using Microsoft.EntityFrameworkCore;
using Reception.Extensions;
using Reception.Server.File.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.File.Repository
{
    public class FileVersionService : IFileVersionService
    {
        private readonly FileContext _context;

        public FileVersionService(FileContext context)
        {
            _context = context;
        }

        public async Task<FileVersion> GetAsync(int id)
        {
            return await _context.FileVersions.FirstOrDefaultAsync(fi => fi.Id == id);
        }

        public IQueryable<FileVersion> Queryable()
        {
            return _context.FileVersions.AsQueryable();
        }

        public async Task<List<FileVersion>> SearchAsync(string searchText)
        {
            return await SearchVersionInfosQuery(searchText).ToListAsync();
        }

        public async Task<List<FileVersion>> SearchPagedAsync(string searchText, int count, int page)
        {
            return await
                SearchVersionInfosQuery(searchText)
                .Paged(page, count)
                .ToListAsync();
        }

        private IQueryable<FileVersion> SearchVersionInfosQuery(string searchText)
        {
            var likeSearchText = searchText.AsLike();
            return
                _context.FileVersions
                .Where(
                    vi =>
                    EF.Functions.Like(vi.Comment, likeSearchText) ||
                    EF.Functions.Like(vi.Name + "." + vi.Extension, likeSearchText) ||
                    EF.Functions.Like((string)(object)vi.Version, likeSearchText));
        }
    }
}