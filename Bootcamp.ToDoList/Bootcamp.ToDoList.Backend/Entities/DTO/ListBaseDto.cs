using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bootcamp.ToDoList.Backend.Entities.DTO
{
    public class ListBaseDto
    {
        public Guid publicId { get; set; }
        public string Name { get; set; }
    }
}