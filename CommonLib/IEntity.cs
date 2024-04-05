﻿using System;

namespace Common
{
    public interface IEntity
    {
        DateTimeOffset CreatedDate { get; set; }
        string Description { get; set; }
        Guid Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
    }
}