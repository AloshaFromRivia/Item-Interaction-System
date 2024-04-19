using Catalog.Entities;
using Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Catalog.Dtos;
using Contracts;
using static Contracts.Contracts;

namespace Catalog.Controllers
{

    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ItemsController(IRepository<Item> itemRepository, IPublishEndpoint publishEndpoint)
        {
            _itemRepository = itemRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAsync()
        { 
           return Ok((await _itemRepository.GetAllAsync()).Select(i=>i.AsDto()));
        }

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

            await _publishEndpoint.Publish(new CatalogitemCreated(item.Id,item.Name,item.Description));

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

            await _publishEndpoint.Publish(new CatalogitemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));

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

            await _publishEndpoint.Publish(new CatalogitemDeleted(id));

            return NoContent();
        }
    }
}
