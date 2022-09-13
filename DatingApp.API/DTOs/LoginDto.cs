using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.DTOs
{
    public class LoginDto
    {
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}