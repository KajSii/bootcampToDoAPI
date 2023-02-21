using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bootcamp.ToDoList.Backend.Entities.DTO
{
    public class ListDto : ListBaseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ItemDto> Items { get; set; }
    }
}