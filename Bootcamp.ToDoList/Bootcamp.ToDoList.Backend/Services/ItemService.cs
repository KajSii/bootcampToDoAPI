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
        Task<ItemDto> IItemService.CreateItem(ItemModel model, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task IItemService.DeleteItem(Guid itemId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task<ItemDto> IItemService.GetItem(Guid itemId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task<List<ItemDto>> IItemService.GetItems(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task<ItemDto> IItemService.UpdateItem(Guid itemId, ItemModel model, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}