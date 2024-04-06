using CatalogService.Entities;
using Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CatalogService.Dtos;

namespace Catalog.Controllers
{

    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> _itemRepository;

        public ItemsController(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync() => (await _itemRepository.GetAllAsync()).Select(i=>i.AsDto());

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetAsync(Guid id)
        {
            var item = await _itemRepository.GetAsync(id);
            

            if(item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item()
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _itemRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetAsync), new { item.Id},item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto) 
        {
            var existingItem = await _itemRepository.GetAsync(id);

            if(existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await _itemRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _itemRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await _itemRepository.RemoveAsync(id);

            return NoContent();
        }
    }
}
