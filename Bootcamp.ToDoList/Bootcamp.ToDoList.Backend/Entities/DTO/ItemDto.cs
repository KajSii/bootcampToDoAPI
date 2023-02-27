using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bootcamp.ToDoList.Backend.Entities.DTO
{
    public class ItemDto
    {
        [Required]
        public Guid? PublicId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        // [Required, StringLength(200)]
        // public string Description { get; set; }

        [Required]
        public DateTime TimeOfCreation { get; set; }

        [Required]
        public bool Status { get; set; }

        public DateTime? EndTime { get; set; }
    }
}