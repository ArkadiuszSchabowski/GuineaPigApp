using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Database.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana!")]
        [MaxLength(25, ErrorMessage = "Nazwa produktu nie może być dłuższa niż 25 znaków!")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Opis produktu jest wymagany")]
        [MaxLength(1000, ErrorMessage = "Opis produktu nie może być dłuższy niż 1000 znaków!")]
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "/assets/images/products/default.jpg";
        [Required]
        public bool IsGoodProduct { get; set; }
    }
}
