using System;

namespace Inventory
{
    public class Dtos
    {
        public record GrantItemsDto(Guid UserId, Guid CalaogItemId, int Quantity);
        public record InventoryItemDto(Guid CatalogItemId,int Quantity, DateTimeOffset AcquiredDate);
    }
}
