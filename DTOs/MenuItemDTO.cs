namespace ResturantRESTAPI.DTOs
{
    public class MenuItemDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsPopular { get; set; }
        public string? ImageUrl { get; set; }
    }
}
