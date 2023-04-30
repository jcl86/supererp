using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuperErp.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperErp.Sales.Domain
{
    [Service]
    public class ProductLister
    {
        private readonly Data.ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProductLister(Data.ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Model.Product>> ToList()
        {
            var list = await context.Set<Product>().AsNoTracking()
                .ProjectTo<Model.Product>(mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }
    }

}
