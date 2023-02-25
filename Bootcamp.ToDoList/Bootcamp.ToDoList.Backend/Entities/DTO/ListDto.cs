using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bootcamp.ToDoList.Backend.Entities.DTO
{
    public class ListDto : ListBaseDto
    {
        public Guid publicId { get; set; }
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ItemDto> Items { get; set; }
    }
}