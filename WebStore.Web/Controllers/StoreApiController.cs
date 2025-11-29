using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreApiController : BaseApiController
{
    private readonly IStoreService _storeService;

    // ZMIANA TUTAJ: Dodano <StoreApiController>
    public StoreApiController(ILogger<StoreApiController> logger, IMapper mapper, IStoreService storeService)
        : base(logger, mapper)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public ActionResult Get()
    {
        try
        {
            var stores = _storeService.GetStores();
            return Ok(stores);
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
            var store = _storeService.GetStore(s => s.Id == id);
            return Ok(store);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody] AddOrUpdateStationaryStoreVm vm)
    {
        return PostOrPutHelper(vm);
    }

    [HttpPut]
    public ActionResult Put([FromBody] AddOrUpdateStationaryStoreVm vm)
    {
        return PostOrPutHelper(vm);
    }

    private ActionResult PostOrPutHelper(AddOrUpdateStationaryStoreVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_storeService.AddOrUpdateStore(vm));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }
}