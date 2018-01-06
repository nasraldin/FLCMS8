using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [EnableCors("http://falmedia.sa", "*", "*", SupportsCredentials = true)]
    public class CategoryController : ApiController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // DELETE: api/Category/5
        //[Route("api/Category/{id}")]
        [HttpPost]
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
                return NotFound();

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return Ok(category);
        }
    }
}