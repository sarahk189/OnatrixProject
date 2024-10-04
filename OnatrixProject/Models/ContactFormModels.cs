using System.ComponentModel.DataAnnotations;

namespace OnatrixProject.Models
{
    public class ContactFormModels
    {
        public string? Name { get; set; } 
        public string? Email { get; set; } 
        public string? Phone { get; set; }  
        public string? Service { get; set; } 
        public string? Message { get; set; } 
        public string? Choices { get; set; } 
    }
}
