﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}