using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RockApp.Models
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}