using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Title3 { get; set; }
        public string Description { get; set; }
        public string? Image{ get; set; }
        public string RedirctUrl1 { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }
    }
}
