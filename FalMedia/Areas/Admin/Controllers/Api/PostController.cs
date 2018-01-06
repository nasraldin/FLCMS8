using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [EnableCors("http://falmedia.sa", "*", "*", SupportsCredentials = true)]
    public class PostController : ApiController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // DELETE: api/Post/5
        //[Route("api/Post/{id}")]
        [HttpPost]
        [ResponseType(typeof(Post))]
        public IHttpActionResult DeletePost(int id)
        {
            var post = _db.Posts.Find(id);
            if (post == null)
                return NotFound();

            _db.Posts.Remove(post);
            _db.SaveChanges();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}