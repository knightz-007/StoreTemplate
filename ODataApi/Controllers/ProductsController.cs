using DataTransferObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ODataController
    {
        private IProductRepository productRepo = new ProductRepository();

        // GET: api/Products
        [EnableQuery]
        public ActionResult GetProducts()
        {
            return Ok(productRepo.GetProducts());
        }

        // GET: api/Products/5
        [EnableQuery]
        public ActionResult GetProduct(int id)
        {
            var product = productRepo.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableQuery]
        public IActionResult PutProduct(int id, [FromBody]Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var temp = productRepo.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            productRepo.UpdateProduct(product);

            return Updated(product);
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableQuery]
        public ActionResult<Product> PostProduct([FromBody] Product product)
        {
            productRepo.SaveProduct(product);
            return Created(product);
        }

        // DELETE: api/Products/5
        [EnableQuery]
        public IActionResult DeleteProduct([FromBody]int id)
        {
            var p = productRepo.GetProductById(id);
            if (p == null)
            {
                return NotFound();
            }

            productRepo.DeleteProduct(p);

            return Ok();
        }
    }
}
