using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Database;
using Bootcamp.ToDoList.Backend.Entities.Domain;
using Bootcamp.ToDoList.Backend.Entities.DTO;
using Bootcamp.ToDoList.Backend.Entities.Models;
using Bootcamp.ToDoList.Backend.Exceptions;
using Bootcamp.ToDoList.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp.ToDoList.Backend.Services
{
    public class ListService : IListService
    {
        private ApplicationContext _context;
        //private IHttpContextAccessor _httpContextAccessor;

        public ListService(ApplicationContext context /*IHttpContextAccessor httpContextAccessor = null*/)
        {
            _context = context;
            // _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ListDto> CreateListAsync(/*string user,*/ ListModel model, CancellationToken ct)
        {
            Lists duplicate = await _context.Lists.AsNoTracking().SingleOrDefaultAsync(x => x.Name == model.Name, ct);
            if (duplicate != null)
            {
                throw new ConflictException($"List with name {model.Name} is already exists");
            }

            Lists list = model.ToDomain();
            list.publicId = Guid.NewGuid();

            /*var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext.User.Identity!.Name;*/

            /*User userData = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.UserName == user, ct);
            list.UserId = userData.Id;*/
            list.UserId = 1;

            await _context.AddAsync(list, ct);
            await _context.SaveChangesAsync(ct);

            return list.ToDto();
        }

        public async Task DeleteListAsync(Guid listId, CancellationToken ct = default)
        {
            var list = await _context.Lists.AsNoTracking().Include(x => x.Items).SingleOrDefaultAsync(x => x.publicId == listId, ct);
            if(list == null) {
                throw new NotFoundException($"List with ID {listId} doesn't exists");
            }

            var test = list.Id;
            var items = _context.Items.Where(x => x.Id == list.Id);


            _context.Lists.Remove(list);
            _context.Items.RemoveRange(items);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<ListDto> GetListAsync(Guid listId, CancellationToken ct = default)
        {

            var list = await _context.Lists.AsNoTracking().Include(x => x.Items).SingleOrDefaultAsync(x => x.publicId == listId, ct);
            if(list == null) {
                throw new NotFoundException($"List with ID {listId} doesn't exists");
            }

            return list.ToDto();
        }

        public async Task<List<ListDto>> GetListsAsync(int? pageSize, CancellationToken ct = default)
        {
            IQueryable<Lists> query = _context.Lists
                .AsNoTracking()
                .AsQueryable();

            if (pageSize > 0)
            {
                query = query.Take(pageSize.Value);
            }

            List<Lists> lists = await query.ToListAsync();
            List<ListDto> dtos = lists.Select(x => x.ToDto()).ToList();

            return dtos;
        }

        public async Task<ListDto> UpdateListAsync(Guid listId, ListModel model, CancellationToken ct = default)
        {
            var list = await _context.Lists.AsNoTracking().Include(x => x.Items).SingleOrDefaultAsync(x => x.publicId == listId, ct);
            if(list == null) {
                throw new NotFoundException($"List with ID {listId} doesn't exists");
            }

            list.Name = model.Name;

            _context.Lists.Update(list);
            await _context.SaveChangesAsync(ct);

            return list.ToDto();
        }

    }
}