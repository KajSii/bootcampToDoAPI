using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.Domain;

namespace Bootcamp.ToDoList.Backend.Entities.Models
{
    public class ListModel
    {
        [Required, StringLength(15)]
        public string Name { get; set; }

        public Lists ToDomain()
        {
            return new Lists
            {
                Name = Name
            };
        }
    }
}