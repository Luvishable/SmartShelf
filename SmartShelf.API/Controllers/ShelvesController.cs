using Microsoft.AspNetCore.Mvc;
using SmartShelf.Application.DTOs;
using SmartShelf.Application.Interfaces;

namespace SmartShelf.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ShelvesController : ControllerBase
{
    private readonly IShelfService _shelfService;

    public ShelvesController(IShelfService shelfService)
    {
        _shelfService = shelfService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] ShelfCreateDto dto)
    {
        var id = _shelfService.CreateShelf(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPost("add-product")]
    public IActionResult AddProductToShelf([FromBody] AddProductToShelfDto dto)
    {
        try
        {
            _shelfService.AddProductToShelf(dto.ShelfId, dto.ProductId, dto.Quantity);
            return Ok("Product added to shelf.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var shelf = _shelfService.GetById(id);
        return Ok(shelf);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var shelves = _shelfService.GetAll();
        return Ok(shelves);
    }

    [HttpPost("{id}/deactivate")]
    public IActionResult Deactivate(Guid id)
    {
        _shelfService.DeactivateShelf(id);
        return NoContent();
    }

    [HttpPost("{id}/reactivate")]
    public IActionResult Reactivate(Guid id)
    {
        _shelfService.ReactivateShelf(id);
        return NoContent();
    }

    [HttpPost("remove-product")]
    public IActionResult RemoveProduct([FromBody] RemoveProductFromShelfDto dto)
    {
        _shelfService.RemoveProductFromShelf(dto.ShelfId, dto.ProductId, dto.Quantity);
        return Ok("Product removed from shelf.");
    }
}