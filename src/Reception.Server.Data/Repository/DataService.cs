using Microsoft.EntityFrameworkCore;
using Reception.Extension;
using Reception.Server.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.Server.Data.Repository
{
    public class DataService : IDataService
    {
        private readonly DataContext _context;

        public DataService(DataContext context)
        {
            _context = context;
        }

        public async Task<Person> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Persons
                .Include(p => p.Post)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<Post> GetPostAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public IQueryable<Person> Queryable()
        {
            return _context.Persons.AsQueryable();
        }

        public async Task<IEnumerable<Person>> SearchAsync(string searchText,
            CancellationToken cancellationToken = default)
        {
            return await SearchPersonsQuery(searchText).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Person>> SearchPagedAsync(string searchText, int count, int page,
            CancellationToken cancellationToken = default)
        {
            return await
                SearchPersonsQuery(searchText)
                .Paged(page, count)
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Person> SearchPersonsQuery(string searchText)
        {
            var likeSearchText = searchText.AsLike();
            return
                _context.Persons
                .Include(p => p.Post)
                .Where(
                    p =>
                    EF.Functions.Like(p.Comment, likeSearchText) ||
                    EF.Functions.Like(p.FirstName, likeSearchText) ||
                    EF.Functions.Like(p.MiddleName, likeSearchText) ||
                    EF.Functions.Like(p.Post.Name, likeSearchText) ||
                    EF.Functions.Like(p.SecondName, likeSearchText));
        }
    }
}