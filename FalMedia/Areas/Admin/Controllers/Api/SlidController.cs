using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [EnableCors("http://falmedia.sa", "*", "*", SupportsCredentials = true)]
    public class SlidController : ApiController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // DELETE: api/Slid/5
        //[Route("api/Slid/{id}")]
        [HttpPost]
        [ResponseType(typeof(Slids))]
        public IHttpActionResult DeleteSlid(int id)
        {
            var slid = _db.Slids.Find(id);
            if (slid == null)
                return NotFound();

            _db.Slids.Remove(slid);
            _db.SaveChanges();

            return Ok(slid);
        }
    }
}