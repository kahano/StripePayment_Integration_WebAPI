﻿using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.DTOS.Account
{
    public class LoginDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
     
     
    }
}

