using AdApi.Models;
using AdApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdApi.Controllers
{
    [Route("ads")]
    [ApiController]
    public class AdController : ControllerBase
    {
        AdService _service;
        public AdController(AdService service)
        {
            _service = service;
        }


        // GET: ads?page={pageNumber}&orderBy={price|price_desc|date|date_desc}
        [HttpGet]
        public IEnumerable<AdDTO> Get(int page = 1, string? orderBy = null)
        {
            return _service.GetPage(page, orderBy);
        }

        // GET ads/{id}?fields=all|desc|photos
        [HttpGet("{id}")]
        public ActionResult<AdDTO> GetById(int id, string? fields)
        {
            AdDTO? ad = _service.GetById(id, fields);
            if (ad is not null)
            {
                return ad;
            }
            else
            {
                return NotFound();
            }
        }

        // POST ads
        [HttpPost]
        public IActionResult Create(Ad newAd)
        {
            var ad = _service.Create(newAd);
            return CreatedAtAction(nameof(GetById), new { id = ad!.Id }, ad.Id);
        }
    }
}
