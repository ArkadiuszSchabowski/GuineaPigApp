using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Database.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 25, MinimumLength = 3,
            ErrorMessage = "Nazwa produktu nie może być krótsza niż 3 znaki i dłuższa niż 25 znaków!"
        )]
        public string Name { get; set; } = string.Empty;

        [StringLength(maximumLength: 1000, MinimumLength = 25,
            ErrorMessage = "Opis produktu nie może być krótszy niż 25 znaków i dłuższa niż 1000 znaków!"
        )]
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "/assets/images/products/default.jpg";
        [Required]
        public bool isGoodProduct { get; set; }
    }
}
