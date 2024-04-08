using Inventory.Entities;
using static Inventory.Dtos;

namespace Inventory
{
    public static class Extentions
    {
        public static InventoryItemDto AsDto(this InventoryItem item, string name, string description)
        {
            return new InventoryItemDto(item.CatalogItemId,name,description,item.Quantity,item.AccuiredDate);
        }
    }
}
