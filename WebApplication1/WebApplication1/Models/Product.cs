using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsAvailable { get; set; }
        public double Tax { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DisPrice { get; set; }

        public List<ProductTag>? ProductTags { get; set; }
        [NotMapped]
        public List<int>? TagIds { get; set; }

    }
}
