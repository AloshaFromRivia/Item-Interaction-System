using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static Inventory.Dtos;

namespace Inventory.Clients
{
    public class CatalogClient
    {
        private HttpClient httpClient;

        public CatalogClient(HttpClient client)
        {
            httpClient= client;
        }

        public async Task<IReadOnlyCollection<CatalogItemDto>> GetCatalogItemsAsync()
        {
            var items = await httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogItemDto>>("/api/items");

            return items;
        }
    }
}