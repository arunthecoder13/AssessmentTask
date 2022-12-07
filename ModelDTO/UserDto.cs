﻿using System.ComponentModel.DataAnnotations;

namespace AssessmentTask.ModelDTO
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
