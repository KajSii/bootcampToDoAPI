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
        private IItemService _itemService;

        public ListService(ApplicationContext context, IItemService itemService)
        {
            _context = context;
            _itemService = itemService;
        }

        public async Task<ListDto> CreateListAsync(string user, ListModel model, CancellationToken ct)
        {
            User userData = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.UserName == user, ct);
            Lists duplicate = await _context.Lists.AsNoTracking().SingleOrDefaultAsync(x => x.Name == model.Name && x.UserId == userData.Id, ct);
            if (duplicate != null)
            {
                throw new ConflictException($"List with name {model.Name} is already exists");
            }

            Lists list = model.ToDomain();
            list.publicId = Guid.NewGuid();
            list.UserId = userData.Id;

            list.UserId = userData.Id;

            await _context.AddAsync(list, ct);
            await _context.SaveChangesAsync(ct);

            return list.ToDto();
        }

        public async Task DeleteListAsync(string user, Guid listId, CancellationToken ct = default)
        {
            User userData = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.UserName == user, ct);
            var list = await _context.Lists.AsNoTracking().Include(x => x.Items).SingleOrDefaultAsync(x => x.publicId == listId && x.UserId == userData.Id, ct);
            if(list == null) {
                throw new NotFoundException($"List with ID {listId} doesn't exists or is not yours.");
            }

            var test = list.Id;
            var items = _context.Items.Where(x => x.Id == list.Id);


            _context.Lists.Remove(list);
            _context.Items.RemoveRange(items);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<ListDto> GetListAsync(string user, Guid listId, CancellationToken ct = default)
        {
            User userData = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.UserName == user, ct);
            var list = await _context.Lists.AsNoTracking().Include(x => x.Items).SingleOrDefaultAsync(x => x.publicId == listId && x.UserId == userData.Id, ct);
            if(list == null) {
                throw new NotFoundException($"List with ID {listId} doesn't exists or is not yours.");
            }

            return list.ToDto();
        }

        public async Task<List<ListDto>> GetListsAsync(string user, int? pageSize, CancellationToken ct = default)
        {
            User userData = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.UserName == user, ct);
            IQueryable<Lists> query = _context.Lists
                .AsNoTracking()
                .AsQueryable()
                .Where(x => x.UserId == userData.Id);

            if (pageSize > 0)
            {
                query = query.Take(pageSize.Value);
            }

            List<Lists> lists = await query.ToListAsync();
            List<ListDto> dtos = lists.Select(x => x.ToDto()).ToList();
            
            foreach (var dto in dtos)
            {
                dto.Items = await _itemService.GetAllItemsAsync(dto.publicId, ct: ct);
            }

            return dtos;
        }

        public async Task<ListDto> UpdateListAsync(string user, Guid listId, ListModel model, CancellationToken ct = default)
        {
            User userData = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.UserName == user, ct);
            var list = await _context.Lists.AsNoTracking().Include(x => x.Items).SingleOrDefaultAsync(x => x.publicId == listId && x.UserId == userData.Id, ct);
            if(list == null) {
                throw new NotFoundException($"List with ID {listId} doesn't exists or is not yours.");
            }

            list.Name = model.Name;

            _context.Lists.Update(list);
            await _context.SaveChangesAsync(ct);

            return list.ToDto();
        }

    }
}