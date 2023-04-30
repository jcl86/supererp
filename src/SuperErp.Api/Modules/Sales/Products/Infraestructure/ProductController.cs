using AutoMapper;
using SuperErp.Sales.Domain;
using SuperErp.Sales.Model;
using Microsoft.AspNetCore.Mvc;

namespace SuperErp.Sales
{
    [ApiController]
    [Route(Endpoints.Products.Base)]
    public class ProductController : ControllerBase
    {
        private readonly ProductCreator creator;
        private readonly ProductFinder finder;
        private readonly ProductLister lister;
        private readonly ProductUpdater updater;
        private readonly ProductEraser eraser;
        private readonly IMapper mapper;

        public ProductController(ProductCreator creator,
            ProductFinder finder,
            ProductLister lister,
            ProductUpdater updater,
            ProductEraser eraser,
            IMapper mapper)
        {
            this.creator = creator;
            this.finder = finder;
            this.lister = lister;
            this.updater = updater;
            this.eraser = eraser;
            this.mapper = mapper;
        }

        [HttpGet, Route("{productId}")]
        public async Task<Model.Product> Get(string productId)
        {
            var user = User;
            var entity = await finder.Find(productId);
            var result = mapper.Map<SuperErp.Sales.Model.Product>(entity);
            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<Model.Product>> GetAll()
        {
            var result = await lister.ToList();
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Model.CreateProduct dto)
        {
            var entity = await creator.Create(dto);
            var result = mapper.Map<Model.Product>(entity);
            return CreatedAtAction(nameof(Get), new { productId = result.Id }, result);
        }

        [HttpPatch, Route("{productId}")]
        public async Task<IActionResult> Update(string productId, Model.UpdateProduct dto)
        {
            await updater.Update(productId, dto);
            return NoContent();
        }

        [HttpDelete, Route("{productId}")]
        public async Task<IActionResult> Delete(string productId)
        {
            await eraser.Delete(productId);
            return NoContent();
        }
    }
}
