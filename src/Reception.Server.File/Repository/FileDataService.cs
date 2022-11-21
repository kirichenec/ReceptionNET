using Microsoft.EntityFrameworkCore;
using Reception.Extension;
using Reception.Server.File.Entities;

namespace Reception.Server.File.Repository
{
    public class FileDataService : IFileDataService
    {
        private readonly FileContext _context;

        public FileDataService(FileContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (await GetAsync(id, cancellationToken) is FileData dataToDelete)
            {
                _context.FileDatas.Remove(dataToDelete);
                _ = await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<FileData> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.FileDatas.FirstOrDefaultAsync(fi => fi.Id == id, cancellationToken);
        }

        public IQueryable<FileData> Queryable()
        {
            return _context.FileDatas.AsQueryable();
        }

        public async Task<FileData> SaveAsync(FileData value, CancellationToken cancellationToken = default)
        {
            var trackedData = await _context.AddAsync(value, cancellationToken);
            _ = await _context.SaveChangesAsync(cancellationToken);
            return await GetAsync(trackedData.Entity.Id, cancellationToken);
        }

        public async Task<IEnumerable<FileData>> SearchAsync(string searchText,
            CancellationToken cancellationToken = default)
        {
            return await SearchFileDataQuery(searchText).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<FileData>> SearchPagedAsync(string searchText, int count, int page,
            CancellationToken cancellationToken = default)
        {
            return await SearchFileDataQuery(searchText)
                .Paged(page, count)
                .ToListAsync(cancellationToken);
        }

        private IQueryable<FileData> SearchFileDataQuery(string searchText)
        {
            var likeSearchText = searchText.AsLike();
            return _context.FileDatas
                .Where(fd =>
                    EF.Functions.Like(fd.Comment, likeSearchText)
                    || EF.Functions.Like(fd.Name + "." + fd.Extension, likeSearchText));
        }
    }
}