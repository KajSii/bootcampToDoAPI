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

        public async Task<ItemDto> CreateItemAsync(Guid listId, ItemModel model, CancellationToken ct = default)
        {
            var list = await _context.Lists.AsNoTracking().SingleOrDefaultAsync(x => x.publicId == listId);

            if (await _context.Items.AnyAsync(x => x.Name == model.Name, ct))
            {
                throw new ConflictException($"Item with name {model.Name} already exists");
            }

            var item = model.ToDomain();
            item.PublicId = Guid.NewGuid();
            item.Status = false;
            item.ListId = list.Id;

            await _context.Items.AddAsync(item, ct);
            await _context.SaveChangesAsync(ct);

            return item.ToDto();
        }

        public async Task DeleteItemAsync(Guid itemId, CancellationToken ct = default)
        {
            var item = await GetItemMethod(itemId, ct);

            _context.Items.Remove(item);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<ItemDto> GetItemAsync(Guid itemId, CancellationToken ct = default)
        {
            var item = await GetItemMethod(itemId, ct);
            return item.ToDto();
        }

        public async Task<List<ItemDto>> GetAllItemsAsync(Guid listId, int? pageSize = null, CancellationToken ct = default)
        {
            var list = await _context.Lists.AsNoTracking().SingleOrDefaultAsync(x => x.publicId == listId);

            IQueryable<Item> query = _context.Items
                .AsNoTracking()
                .AsQueryable()
                .Where(x => x.ListId == list.Id);

            if (pageSize > 0)
            {
                query = query.Take(pageSize.Value);
            }

            List<Item> items = await query.ToListAsync();
            List<ItemDto> dtos = items.Select(x => x.ToDto()).ToList();

            return dtos;
        }

        public async Task<ItemDto> UpdateItemAsync(Guid itemId, ItemModel model, CancellationToken ct = default)
        {
            var item = await GetItemMethod(itemId, ct);

            item.Name = model.Name;
            item.Description = model.Description;

            _context.Items.Update(item);
            await _context.SaveChangesAsync(ct);
            
            return item.ToDto();
        }

        public async Task<ItemDto> UpdateStatusAsync(Guid itemId, CancellationToken ct)
        {
            var item = await GetItemMethod(itemId, ct);

            if(item.Status == true) {
                item.Status = false;
            } else {
                item.Status = true;
            }

            _context.Items.Update(item);
            await _context.SaveChangesAsync(ct);
            return item.ToDto();
        }

        private async Task<Item> GetItemMethod(Guid itemId, CancellationToken ct = default) {
            var item = await _context.Items
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.PublicId == itemId, ct);

            if (item == null)
            {
                throw new NotFoundException($"Item with Id: {itemId} doesn't exist.");
            }

            return item;
        }
    }
}