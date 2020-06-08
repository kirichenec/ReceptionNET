using Microsoft.EntityFrameworkCore;
using Reception.Extensions;
using Reception.Server.File.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.File.Repository
{
    public class FileDataService : IFileDataService
    {
        private readonly FileContext _context;

        public FileDataService(FileContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (await GetAsync(id) is FileData dataToDelete)
            {
                _context.Datas.Remove(dataToDelete);
                return true;
            }
            return false;
        }

        public async Task<FileData> GetAsync(int id)
        {
            return await _context.Datas.FirstOrDefaultAsync(fi => fi.Id == id);
        }

        public IQueryable<FileData> Queryable()
        {
            return _context.Datas.AsQueryable();
        }

        public async Task<FileData> SaveAsync(FileData value)
        {
            var trackedData = await _context.AddAsync(value);
            await _context.SaveChangesAsync();
            return await GetAsync(trackedData.Entity.Id);
        }

        public async Task<List<FileData>> SearchPagedAsync(string searchText, int count, int page)
        {
            return await
                SearchFileDataQuery(searchText)
                .Paged(page, count)
                .ToListAsync();
        }

        public async Task<List<FileData>> SearchAsync(string searchText)
        {
            return await SearchFileDataQuery(searchText).ToListAsync();
        }

        private IQueryable<FileData> SearchFileDataQuery(string searchText)
        {
            var likeSearchText = searchText.AsLike();
            return
                _context.Datas
                .Include(data => data.VersionInfo)
                .Where(
                    vi =>
                    EF.Functions.Like(vi.Id.ToString(), likeSearchText) ||
                    EF.Functions.Like(vi.VersionInfo.Comment, likeSearchText) ||
                    EF.Functions.Like(vi.VersionInfo.Name, likeSearchText) ||
                    EF.Functions.Like(vi.VersionInfo.Version.ToString(), likeSearchText));
        }
    }
}