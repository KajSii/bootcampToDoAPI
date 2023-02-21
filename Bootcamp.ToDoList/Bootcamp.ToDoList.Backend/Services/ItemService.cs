using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.DTO;
using Bootcamp.ToDoList.Backend.Entities.Models;
using Bootcamp.ToDoList.Backend.Services.Interfaces;

namespace Bootcamp.ToDoList.Backend.Services
{
    public class ItemService : IItemService
    {
        Task<ItemDto> IItemService.CreateItemAsync(ItemModel model, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task IItemService.DeleteItemAsync(Guid itemId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task<ItemDto> IItemService.GetItemAsync(Guid itemId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task<List<ItemDto>> IItemService.GetItemsAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task<ItemDto> IItemService.UpdateItemAsync(Guid itemId, ItemModel model, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}