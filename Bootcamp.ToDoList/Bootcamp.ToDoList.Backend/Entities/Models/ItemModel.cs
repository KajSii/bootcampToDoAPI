using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.Domain;

namespace Bootcamp.ToDoList.Backend.Entities.Models
{
    public class ItemModel
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        // [Required, StringLength(200)]
        // public string Description { get; set; }
        // public DateTime EndTime { get; set; } = DateTime.Now;

        public Item ToDomain()
        {
            return new Item
            {
                Name = Name,
                // Description = Description,
                // EndTime = EndTime
            };
        }
    }
}