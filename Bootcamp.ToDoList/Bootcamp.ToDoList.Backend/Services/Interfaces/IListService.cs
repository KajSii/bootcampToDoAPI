using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.DTO;
using Bootcamp.ToDoList.Backend.Entities.Models;

namespace Bootcamp.ToDoList.Backend.Services.Interfaces
{
    public interface IListService
    {
        Task<ListDto> CreateListAsync(ListModel model, CancellationToken ct = default);
        Task<ListDto> GetListAsync(Guid listId, CancellationToken ct = default);
        Task<List<ListDto>> GetListsAsync(int? pageSize = null, CancellationToken ct = default);
        Task DeleteListAsync(Guid listId, CancellationToken ct = default);
        Task<ListDto> UpdateListAsync(Guid listId, ListModel model, CancellationToken ct = default);

    }
}