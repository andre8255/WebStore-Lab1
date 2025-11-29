using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceApiController : BaseApiController
{
    private readonly IInvoiceService _invoiceService;

    // ZMIANA TUTAJ: Dodano <InvoiceApiController>
    public InvoiceApiController(ILogger<InvoiceApiController> logger, IMapper mapper, IInvoiceService invoiceService)
        : base(logger, mapper)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet]
    public ActionResult Get()
    {
        try
        {
            var invoices = _invoiceService.GetInvoices();
            return Ok(invoices);
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
            var invoice = _invoiceService.GetInvoice(i => i.Id == id);
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody] AddOrUpdateInvoiceVm vm)
    {
        return PostOrPutHelper(vm);
    }

    [HttpPut]
    public ActionResult Put([FromBody] AddOrUpdateInvoiceVm vm)
    {
        return PostOrPutHelper(vm);
    }

    private ActionResult PostOrPutHelper(AddOrUpdateInvoiceVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_invoiceService.AddOrUpdateInvoice(vm));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, "Error occured");
        }
    }
}