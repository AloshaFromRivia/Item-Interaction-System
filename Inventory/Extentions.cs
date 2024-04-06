using Inventory.Entities;
using static Inventory.Dtos;

namespace Inventory
{
    public static class Extentions
    {
        public static InventoryItemDto AsDto(this InventoryItem item)
        {
            return new InventoryItemDto(item.CatalogItemId,item.Quantity,item.AccuiredDate);
        }
    }
}
