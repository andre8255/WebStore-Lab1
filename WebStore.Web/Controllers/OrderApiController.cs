using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderApiController : BaseApiController
{
    private readonly IOrderService _orderService;

    // ZMIANA TUTAJ: Dodano <OrderApiController>
    public OrderApiController(ILogger<OrderApiController> logger, IMapper mapper, IOrderService orderService)
        : base(logger, mapper)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public ActionResult Get()
    {
        try
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
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
            var order = _orderService.GetOrder(o => o.Id == id);
            return Ok(order);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody] AddOrUpdateOrderVm vm)
    {
        return PostOrPutHelper(vm);
    }

    [HttpPut]
    public ActionResult Put([FromBody] AddOrUpdateOrderVm vm)
    {
        return PostOrPutHelper(vm);
    }

    private ActionResult PostOrPutHelper(AddOrUpdateOrderVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_orderService.AddOrUpdateOrder(vm));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }
}