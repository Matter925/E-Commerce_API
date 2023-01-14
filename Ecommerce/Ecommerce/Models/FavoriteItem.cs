namespace Ecommerce.Models
{
    public class FavoriteItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int FavoriteId { get; set; }
        public Favorite Favorite { get; set; }
    }
}
