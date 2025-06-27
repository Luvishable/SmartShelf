using Microsoft.AspNetCore.Mvc;
using SmartShelf.Application.DTOs;
using SmartShelf.Application.Interfaces;

namespace SmartShelf.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] SupplierCreateDto dto)
    {
        var id = _supplierService.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var supplier = _supplierService.GetById(id);
        return Ok(supplier);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var suppliers = _supplierService.GetAll();
        return Ok(suppliers);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] SupplierCreateDto dto)
    {
        _supplierService.Update(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _supplierService.Delete(id);
        return NoContent();
    }
}

