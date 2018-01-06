using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [EnableCors("http://falmedia.sa", "*", "*", SupportsCredentials = true)]
    public class SliderController : ApiController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // DELETE: api/Slider/5
        //[Route("api/Slider/{id}")]
        [HttpPost]
        [ResponseType(typeof(Slider))]
        public IHttpActionResult DeleteSlider(int id)
        {
            var slider = _db.Sliders.Find(id);
            if (slider == null)
                return NotFound();

            _db.Sliders.Remove(slider);
            _db.SaveChanges();

            return Ok(slider);
        }
    }
}