using Microsoft.AspNetCore.Mvc;
using CRM.Models;
using Amazon.API.EC;
using CRM.Library.DTO;
namespace Amazon.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger; //the logger allows you to print error messages to the service side log

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }
        [HttpGet()] //httget tells you thath the function is a webcall, and the
                                               // actual name of the function is that is in between the ""
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return await new InventoryEC().Get();

        }
        [HttpDelete("/{id}")]
        public async Task<ProductDTO?> Delete(int id)
        {
            return await new InventoryEC().Delete(id);
        }

        [HttpPost()]
        public async Task<ProductDTO> AddorUpdate([FromBody]ProductDTO p)
        {
            return await new InventoryEC().AddorUpdate(p); 
        }

    }
}
