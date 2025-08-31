﻿using System.ComponentModel.DataAnnotations;

namespace ResturantRESTAPI.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
