using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Database.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana!")]
        [MinLength(3, ErrorMessage = "Nazwa produktu nie może być krótsza niż 3 znaki!")]
        [MaxLength(25, ErrorMessage = "Nazwa produktu nie może być dłuższa niż 25 znaków!")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Opis produktu jest wymagany")]
        [MinLength(15, ErrorMessage = "Opis produktu mnie może być krótszy niż 15 znaków!")]
        [MaxLength(200, ErrorMessage = "Opis produktu nie może być dłuższy niż 200 znaków!")]
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "/assets/images/products/default.jpg";
    }
}
