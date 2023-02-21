using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.DTO;

namespace Bootcamp.ToDoList.Backend.Entities.Domain
{
    public class Lists
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid? publicId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Item> Items { get; set; }

        public ListDto ToDto()
        {
            return new ListDto
            {
                Name = Name,
                publicId = publicId.Value,
                Items = Items?.Select(x => x.ToDto()).ToList()
            };
        }
    }
}