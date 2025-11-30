using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Web.Controllers;

[Route("api/Address")]
[ApiController]
public class AddressApiController : BaseApiController
{
    private readonly IAddressService _addressService;

    // ZMIANA TUTAJ: Dodano <AddressApiController>
    public AddressApiController(ILogger<AddressApiController> logger, IMapper mapper, IAddressService addressService)
        : base(logger, mapper)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public ActionResult Get()
    {
        try
        {
            var addresses = _addressService.GetAddresses();
            return Ok(addresses);
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
            var address = _addressService.GetAddress(a => a.Id == id);
            return Ok(address);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody] AddOrUpdateAddressVm vm)
    {
        return PostOrPutHelper(vm);
    }

    [HttpPut]
    public ActionResult Put([FromBody] AddOrUpdateAddressVm vm)
    {
        return PostOrPutHelper(vm);
    }

    private ActionResult PostOrPutHelper(AddOrUpdateAddressVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_addressService.AddOrUpdateAddress(vm));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }
}