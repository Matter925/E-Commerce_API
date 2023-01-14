namespace Ecommerce.Dto
{
    public class FavoriteItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int FavoriteId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageURL { get; set; }
        public decimal Price { get; set; }
        
    }
}
