namespace Recipe_API.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Ingredients { get; set; } = "";
        public string Instructions { get; set; } = "";
        public string Categories { get; set; } = "";
        public DateTime CreatedDate { get; set; }
    }
}
