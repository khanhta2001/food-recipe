

using System.ComponentModel.DataAnnotations;


namespace WebApp.Models
{
    public class UserViewModel
    {
        [Required]
        public string? Username { get; set; }
        
        [Required]
        public string? Summary { get; set; }
    }   
}