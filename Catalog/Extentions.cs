using CatalogService.Entities;
using System.Runtime.CompilerServices;
using static CatalogService.Dtos;

namespace Catalog
{
    public static class Extentions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id,item.Name,item.Description,item.Price,item.CreatedDate);
        }
    }
}
