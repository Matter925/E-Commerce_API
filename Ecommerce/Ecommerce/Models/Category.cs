﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(100)]
         public string Name { get; set; }
        public string ImageURL { get; set; }
        public Boolean IsActive { get; set; }

    }
}
