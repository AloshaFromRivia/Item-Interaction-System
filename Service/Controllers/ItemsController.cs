using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static CatalogService.Dtos;

namespace CatalogService.Controllers
{

    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new List<ItemDto>()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restore a HP",5, DateTimeOffset.Now),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison",10, DateTimeOffset.Now),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals damage",25, DateTimeOffset.Now),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get() => items;

        [HttpGet("{id}")]
        public ItemDto Get(int id) => items.FirstOrDefault(i => i.Equals(id));
    }
}
