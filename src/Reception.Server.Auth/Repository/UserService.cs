using Microsoft.EntityFrameworkCore;
using Reception.Extension;
using Reception.Server.Auth.Entities;

namespace Reception.Server.Auth.Repository
{
    public class UserService : IUserService
    {
        private readonly AuthContext _context;

        public UserService(AuthContext userContext)
        {
            _context = userContext;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (await GetAsync(id, cancellationToken) is User dataToDelete)
            {
                _context.Users.Remove(dataToDelete);
                return true;
            }
            return false;
        }

        public async Task<User> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        }

        public IQueryable<User> Queryable()
        {
            return _context.Users.AsQueryable();
        }

        public async Task<User> SaveAsync(User value, CancellationToken cancellationToken = default)
        {
            var trackedData = await _context.Users.AddAsync(value, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await GetAsync(trackedData.Entity.Id, cancellationToken);
        }

        public async Task<IEnumerable<User>> SearchAsync(string searchText, CancellationToken cancellationToken = default)
        {
            return await SearchUserQuery(searchText).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> SearchPagedAsync(string searchText, int count, int page,
            CancellationToken cancellationToken = default)
        {
            return await
                SearchUserQuery(searchText)
                .Paged(page, count)
                .ToListAsync(cancellationToken);
        }

        private IQueryable<User> SearchUserQuery(string searchText)
        {
            var likeSearchText = searchText.AsLike();
            return
                _context.Users
                .Where(
                    user =>
                    EF.Functions.Like(user.FirstName, likeSearchText) ||
                    EF.Functions.Like(user.LastName, likeSearchText) ||
                    EF.Functions.Like(user.MiddleName, likeSearchText) ||
                    EF.Functions.Like(user.Login, likeSearchText));
        }
    }
}