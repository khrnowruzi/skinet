using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IUnitOfWork uow) : BaseApiController
{
    [HttpGet]
    [Cache(600)]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(
        [FromQuery] ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);

        return await CreatePagedResult(uow.Repository<Product>(), spec, specParams.PageIndex, specParams.PageSize);
    }

    [HttpGet("{id:int}")]  // api/products/2
    [Cache(600)]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await uow.Repository<Product>().GetByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }

    [InvalidateCache("api/products|")]
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        uow.Repository<Product>().Add(product);

        if (await uow.Complete())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        return BadRequest("Problem creating product");
    }

    [InvalidateCache("api/products|")]
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");

        uow.Repository<Product>().Update(product);

        if (await uow.Complete())
        {
            return NoContent();
        }

        return BadRequest("Problem updating product");
    }

    [InvalidateCache("api/products|")]
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await uow.Repository<Product>().GetByIdAsync(id);

        if (product == null) return NotFound();

        uow.Repository<Product>().Remove(product);

        if (await uow.Complete())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting product");
    }

    [HttpGet("brands")]
    [Cache(10000)]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetBrands()
    {
        var spec = new BrandlistSpecification();

        return Ok(await uow.Repository<Product>().ListAsync(spec));
    }

    [HttpGet("types")]
    [Cache(10000)]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetTypes()
    {
        var spec = new TypeListSpecification();

        return Ok(await uow.Repository<Product>().ListAsync(spec));
    }

    private bool ProductExists(int id)
    {
        return uow.Repository<Product>().Exists(id);
    }
}
