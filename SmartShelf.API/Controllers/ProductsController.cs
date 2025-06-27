using Microsoft.AspNetCore.Mvc;
using SmartShelf.Application.DTOs;
using SmartShelf.Application.Interfaces;

namespace SmartShelf.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] ProductCreateDto dto)
    {
        var id = _productService.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var product = _productService.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productService.GetAll();
        return Ok(products);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] ProductCreateDto dto)
    {
        _productService.Update(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _productService.Delete(id);
        return NoContent();
    }
}
