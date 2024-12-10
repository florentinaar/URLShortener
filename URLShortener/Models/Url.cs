using System.ComponentModel.DataAnnotations;

namespace URLShortener.Models
{
    public class Url
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Url is required")]
        public string Main { get; set; }
        public string Shortened { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Expiration is required")]
        public DateTime Expiration{ get; set; }
        public int Clicked{ get; set; }
        public string QRCode { get; set; }
    }
}
