﻿namespace WebApplication1.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductTag> productTags { get; set; }
    }
}
