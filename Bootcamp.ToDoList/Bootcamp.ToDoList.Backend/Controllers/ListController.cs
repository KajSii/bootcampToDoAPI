using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.DTO;
using Bootcamp.ToDoList.Backend.Entities.Models;
using Bootcamp.ToDoList.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bootcamp.ToDoList.Backend.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    
    [Authorize]
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;
        private IHttpContextAccessor _httpContextAccessor;

        public ListController(IListService listService, IHttpContextAccessor httpContextAccessor = null)
        {
            _listService = listService;
            _httpContextAccessor = httpContextAccessor;
        }

        public const string GetListRouteName = "getlist";

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ListDto))]
        [SwaggerOperation(
            summary: "Create list",
            description: "Create new list",
            OperationId = "CreateList",
            Tags = new[] { "List Management" }
        )]
        public async Task<IActionResult> CreateAsync(
            [Bind, FromBody] ListModel model,
            CancellationToken ct
        ) 
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext.User.Identity.Name;
            ListDto listDto = await _listService.CreateListAsync(user, model, ct);
            return CreatedAtRoute(
                GetListRouteName,
                new {list_id = listDto.publicId},
                listDto
            );
        }

        [HttpGet("{list_id}", Name=GetListRouteName)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Get list",
            description: "Get list by id",
            OperationId = "GetList",
            Tags = new[] { "List Management" }
        )]
        public async Task<IActionResult> GetAsync(
            [Required, FromRoute(Name = "list_id")] Guid? listId,
            CancellationToken ct
        ) 
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext.User.Identity.Name;
            var listDto = await _listService.GetListAsync(user, listId.Value, ct);
            return Ok(listDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ListDto>))]
        [SwaggerOperation(
            summary: "Get lists",
            description: "Get lists",
            OperationId = "GetLists",
            Tags = new[] { "List Management" }
        )]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext.User.Identity.Name;
            List<ListDto> listOfItems = await _listService.GetListsAsync(user, ct: ct);

            return Ok(listOfItems);
        }

        [HttpDelete("{list_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Delete list",
            description: "Delete list by id",
            OperationId = "DeleteList",
            Tags = new[] { "List Management" }
        )]
        public async Task<IActionResult> DeleteAsync(
            [Required, FromRoute(Name = "list_id")] Guid listId,
            CancellationToken ct
        )
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext.User.Identity.Name;
            await _listService.DeleteListAsync(user, listId, ct);
            return NoContent();
        }

        [HttpPut("{list_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Update list",
            description: "Update list by id",
            OperationId = "UpdateList",
            Tags = new[] { "List Management" }
        )]
        public async Task<IActionResult> UpdateAsync(
            [Required, FromRoute(Name = "list_id")] Guid listId,
            [Bind, FromBody] ListModel model,
            CancellationToken ct
        )
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext.User.Identity.Name;
            var list = await _listService.UpdateListAsync(user, listId, model, ct);
            return Ok(list);
        }
    }
}