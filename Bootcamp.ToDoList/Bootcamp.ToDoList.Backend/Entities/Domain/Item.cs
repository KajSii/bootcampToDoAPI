using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.DTO;

namespace Bootcamp.ToDoList.Backend.Entities.Domain
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid? PublicId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime TimeOfCreation { get; set; }

        [Required]
        public bool Status { get; set; }

        // foreign key
        [Required]
        public int ListId { get; set; }
        public virtual Lists List { get; set; }

        public ItemDto ToDto()
        {
            return new ItemDto
            {
                PublicId = PublicId,
                Name = Name,
                Description = Description,
                TimeOfCreation = TimeOfCreation,
                Status = Status
            };
        }
    }
}