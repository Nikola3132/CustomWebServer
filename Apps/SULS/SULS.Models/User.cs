﻿using System.ComponentModel.DataAnnotations;

namespace SULS.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}