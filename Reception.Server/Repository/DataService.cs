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
        private ReceptionContext _context;

        public DataService(ReceptionContext context)
        {
            _context = context;
        }

        public async Task<PersonDto> GetPersonAsync(int uid)
        {
            return
                uid == 0 ?
                null :
                await _context.Persons.Include(p => p.Post).FirstOrDefaultAsync(p => p.Id == uid);
        }

        public async Task<PostDto> GetPostAsync(int uid)
        {
            return
                uid == 0 ?
                null :
                await _context.Posts.FirstOrDefaultAsync(p => p.Id == uid);
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
            return
                _context.Persons
                .Include(p => p.Post)
                .Where(
                    p =>
                    (p.Comment != null && p.Comment.ToLower().Contains(searchText)) ||
                    p.FirstName.ToLower().Contains(searchText) ||
                    (p.MiddleName != null && p.MiddleName.ToLower().Contains(searchText)) ||
                    (p.Post != null && p.Post.Name.ToLower().Contains(searchText)) ||
                    p.SecondName.ToLower().Contains(searchText) ||
                    p.Id.ToString().Contains(searchText));
        }
    }
}