

using System.ComponentModel.DataAnnotations;


namespace WebApp.Models
{
    public class ReviewViewModel
    {
        [Required]
        public string? Review { get; set; }
    }   
}