using System.ComponentModel.DataAnnotations;

namespace Recipe_API.Models.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; } 
        [Required]
        [MaxLength(40)]
        public string? Title { get; set; } 
        public string? Ingredients { get; set; } 
        public string? Instructions { get; set; }
        public string? Categories { get; set; } 
    }
}
