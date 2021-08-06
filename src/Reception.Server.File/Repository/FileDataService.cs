using Microsoft.EntityFrameworkCore;
using Reception.Extension;
using Reception.Server.File.Entities;
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
                _context.FileDatas.Remove(dataToDelete);
                return true;
            }
            return false;
        }

        public async Task<FileData> GetAsync(int id)
        {
            return await _context.FileDatas.FirstOrDefaultAsync(fi => fi.Id == id);
        }

        public IQueryable<FileData> Queryable()
        {
            return _context.FileDatas.AsQueryable();
        }

        public async Task<FileData> SaveAsync(FileData value)
        {
            var trackedData = await _context.AddAsync(value);
            await _context.SaveChangesAsync();
            return await GetAsync(trackedData.Entity.Id);
        }

        public async Task<IEnumerable<FileData>> SearchAsync(string searchText)
        {
            return await SearchFileDataQuery(searchText).ToListAsync();
        }

        public async Task<IEnumerable<FileData>> SearchPagedAsync(string searchText, int count, int page)
        {
            return await
                SearchFileDataQuery(searchText)
                .Paged(page, count)
                .ToListAsync();
        }

        private IQueryable<FileData> SearchFileDataQuery(string searchText)
        {
            var likeSearchText = searchText.AsLike();
            return
                _context.FileDatas
                .Where(
                    fd =>
                    EF.Functions.Like(fd.Comment, likeSearchText)
                    || EF.Functions.Like(fd.Name + "." + fd.Extension, likeSearchText));
        }
    }
}