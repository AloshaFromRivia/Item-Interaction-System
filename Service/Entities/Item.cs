﻿using Common;
using System;

namespace CatalogService.Entities
{
    public class Item : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public decimal Price { get; set; }
    }
}
