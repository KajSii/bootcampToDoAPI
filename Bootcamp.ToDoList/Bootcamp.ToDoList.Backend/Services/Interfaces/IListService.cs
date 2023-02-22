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
        Task<ListDto> CreateListAsync(string user, ListModel model, CancellationToken ct = default);
        Task<ListDto> GetListAsync(string user, Guid listId, CancellationToken ct = default);
        Task<List<ListDto>> GetListsAsync(string user, int? pageSize = null, CancellationToken ct = default);
        Task DeleteListAsync(string user, Guid listId, CancellationToken ct = default);
        Task<ListDto> UpdateListAsync(string user, Guid listId, ListModel model, CancellationToken ct = default);

    }
}