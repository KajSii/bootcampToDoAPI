using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.DTO;
using Bootcamp.ToDoList.Backend.Entities.Models;
using Bootcamp.ToDoList.Backend.Services.Interfaces;
using Bootcamp.ToDoList.Backend.Database;
using Bootcamp.ToDoList.Backend.Exceptions;
using Microsoft.EntityFrameworkCore;
using Bootcamp.ToDoList.Backend.Entities.Domain;
using System.Linq.Expressions;

namespace Bootcamp.ToDoList.Backend.Services
{
    public class ItemService : IItemService
    {
        private ApplicationContext _context;

        public ItemService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ItemDto> CreateItemAsync(ItemModel model, CancellationToken ct = default)
        {
            if (await _context.Items.AnyAsync(x => x.Name == model.Name, ct))
            {
                throw new ConflictException($"Item with name {model.Name} already exists");
            }

            var item = model.ToDomain();
            item.PublicId = Guid.NewGuid();

            await _context.Items.AddAsync(item, ct);
            await _context.SaveChangesAsync(ct);

            return item.ToDto();
        }

        public Task DeleteItemAsync(Guid itemId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ItemDto> GetItemAsync(Guid itemId, CancellationToken ct = default)
        {
            var item = await _context.Items
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.PublicId == itemId, ct);

            if (item == null)
            {
                throw new NotFoundException($"Item with Id: {itemId} doesn't exist.");
            }

            return item.ToDto();
        }

        public async Task<List<ItemDto>> GetAllItemsAsync(int? pageSize = null, CancellationToken ct = default)
        {
            IQueryable<Item> query = _context.Items
                .AsNoTracking()
                .AsQueryable();

            if (pageSize > 0)
            {
                query = query.Take(pageSize.Value);
            }

            List<Item> items = await query.ToListAsync();
            List<ItemDto> dtos = items.Select(x => x.ToDto()).ToList();

            return dtos;
        }

        public Task<ItemDto> UpdateItemAsync(Guid itemId, ItemModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}