﻿using System.ComponentModel.DataAnnotations;

namespace OnatrixProject.Models
{
    public class ContactFormModels
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

    }
}
