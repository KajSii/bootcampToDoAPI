using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.DTO;
using Bootcamp.ToDoList.Backend.Entities.Models;

namespace Bootcamp.ToDoList.Backend.Services.Interfaces
{
    public interface IItemService
    {
        Task<ItemDto> CreateItem(ItemModel model, CancellationToken ct = default);
        Task<ItemDto> GetItem(Guid itemId, CancellationToken ct = default);
        Task<List<ItemDto>> GetItems(CancellationToken ct = default);
        Task<ItemDto> UpdateItem(Guid itemId, ItemModel model, CancellationToken ct = default);
        Task DeleteItem(Guid itemId, CancellationToken ct = default);
    }
}