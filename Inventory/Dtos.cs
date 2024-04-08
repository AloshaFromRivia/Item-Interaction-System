using System;

namespace Inventory
{
    public class Dtos
    {
        public record GrantItemsDto(Guid UserId, Guid CalaogItemId, int Quantity);
        public record InventoryItemDto(Guid CatalogItemId,string Name,string Description,int Quantity, DateTimeOffset AcquiredDate);
        public record CatalogItemDto(Guid id, string Name, string Description);
    }
}
