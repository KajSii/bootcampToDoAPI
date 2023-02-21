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
        Task<ItemDto> CreateItemAsync(ItemModel model, CancellationToken ct = default);
        Task<ItemDto> GetItemAsync(Guid itemId, CancellationToken ct = default);
        Task<List<ItemDto>> GetItemsAsync(CancellationToken ct = default);
        Task<ItemDto> UpdateItemAsync(Guid itemId, ItemModel model, CancellationToken ct = default);
        Task DeleteItemAsync(Guid itemId, CancellationToken ct = default);
    }
}