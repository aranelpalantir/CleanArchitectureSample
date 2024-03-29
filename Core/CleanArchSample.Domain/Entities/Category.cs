﻿using CleanArchSample.Domain.Primitives;

namespace CleanArchSample.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
