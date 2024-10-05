﻿using System.ComponentModel.DataAnnotations;

namespace OnatrixProject.Models
{
    public class QuestionFormModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;

    }
}
