using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [EnableCors("http://falmedia.sa", "*", "*", SupportsCredentials = true)]
    public class ClintController : ApiController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // DELETE: api/Clint/5
        //[Route("api/Clint/{id}")]
        [HttpPost]
        [ResponseType(typeof(Partners))]
        public IHttpActionResult DeleteClint(int id)
        {
            var clint = _db.Partners.Find(id);
            if (clint == null)
                return NotFound();

            _db.Partners.Remove(clint);
            _db.SaveChanges();

            return Ok(clint);
        }
    }
}