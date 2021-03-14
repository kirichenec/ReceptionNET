using Microsoft.EntityFrameworkCore;
using Reception.Extension;
using Reception.Server.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Repository
{
    public class UserService : IUserService
    {
        private readonly UserContext _context;

        public UserService(UserContext userContext)
        {
            _context = userContext;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (await GetAsync(id) is User dataToDelete)
            {
                _context.Users.Remove(dataToDelete);
                return true;
            }
            return false;
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public IQueryable<User> Queryable()
        {
            return _context.Users.AsQueryable();
        }

        public async Task<User> SaveAsync(User value)
        {
            var trackedData = await _context.Users.AddAsync(value);
            await _context.SaveChangesAsync();
            return await GetAsync(trackedData.Entity.Id);
        }

        public async Task<IEnumerable<User>> SearchAsync(string searchText)
        {
            return await SearchUserQuery(searchText).ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchPagedAsync(string searchText, int count, int page)
        {
            return await
                SearchUserQuery(searchText)
                .Paged(page, count)
                .ToListAsync();
        }

        #region Private

        private IQueryable<User> SearchUserQuery(string searchText)
        {
            var likeSearchText = searchText.AsLike();
            return
                _context.Users
                .Where(
                    user =>
                    EF.Functions.Like(user.FirstName, likeSearchText) ||
                    EF.Functions.Like(user.LastName, likeSearchText) ||
                    EF.Functions.Like(user.Login, likeSearchText));
        }

        #endregion
    }
}