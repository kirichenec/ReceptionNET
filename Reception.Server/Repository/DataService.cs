using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reception.Model.Dto;

namespace Reception.Server.Repository
{
    public class DataService : IDataService
    {
        private readonly ReceptionContext _context;

        public DataService(ReceptionContext context)
        {
            _context = context;
        }

        public async Task<PersonDto> GetPersonAsync(int id)
        {
            return
                id == 0 ?
                null :
                await _context.Persons.Include(p => p.Post).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PostDto> GetPostAsync(int id)
        {
            return
                id == 0 ?
                null :
                await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<PersonDto> QueryablePersons()
        {
            return _context.Persons.AsQueryable();
        }

        public async Task<List<PersonDto>> SearchPersonsAsync(string searchText)
        {
            return await
                SearchPersonsQuery(searchText)
                .ToListAsync();
        }

        public async Task<List<PersonDto>> SearchPersonsPagedAsync(string searchText, int count, int page)
        {
            return await
                SearchPersonsQuery(searchText)
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();
        }

        private IQueryable<PersonDto> SearchPersonsQuery(string searchText)
        {
            var likeSearchText = $"%{searchText}%";
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