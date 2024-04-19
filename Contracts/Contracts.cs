using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class Contracts
    {
        public record CatalogitemCreated(Guid id, string Name, string Description);

        public record CatalogitemUpdated(Guid id, string Name, string Description);

        public record CatalogitemDeleted(Guid id);
    }
}
