using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductApiController : BaseApiController
{
    private readonly IProductService _productService;

    public ProductApiController(ILogger<ProductApiController> logger, IMapper mapper, IProductService productService)
        : base(logger, mapper)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult Get()
    {
        try
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }

    [HttpGet("{id:int:min(1)}")]
    public IActionResult Get(int id)
    {
        try
        {
            var product = _productService.GetProduct(p => p.Id == id);
            return Ok(product);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody] AddOrUpdateProductVm vm)
    {
        return PostOrPutHelper(vm);
    }

    [HttpPut]
    public ActionResult Put([FromBody] AddOrUpdateProductVm vm)
    {
        return PostOrPutHelper(vm);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var product = _productService.GetProduct(p => p.Id == id);
            if (product == null) return NotFound();

            // TODO: Zaimplementuj metodę usuwającą w IProductService i wywołaj ją tutaj.
            // Na przykład: _productService.DeleteProduct(id);
            return NoContent(); // Zwraca status 204 No Content, co jest standardem dla udanego usunięcia.
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }

    private ActionResult PostOrPutHelper(AddOrUpdateProductVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_productService.AddOrUpdateProduct(vm));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }
}