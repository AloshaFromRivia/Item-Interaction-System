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
        public ActionResult<ItemDto> Get(int id)
        {
            var item = items.FirstOrDefault(i => i.Equals(id));
            

            if(item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.Now);
            items.Add(item);

            return CreatedAtAction(nameof(Get), new { item.id},item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto) 
        {
            var existingItem = items.SingleOrDefault(i=>id.Equals(i.id));

            if(existingItem == null)
                return NotFound();

            var updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price,
            };

            var index= items.FindIndex(existingItem => existingItem.id == id);
            items[index] = updatedItem;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.id == id);

            if(index == -1)
                return NotFound();

            items.RemoveAt(index);

            return NoContent();
        }
    }
}
