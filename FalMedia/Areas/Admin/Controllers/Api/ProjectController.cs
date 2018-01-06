using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [EnableCors("http://falmedia.sa", "*", "*", SupportsCredentials = true)]
    public class ProjectController : ApiController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // DELETE: api/Project/5
        //[Route("api/Project/{id}")]
        [HttpPost]
        [ResponseType(typeof(Project))]
        public IHttpActionResult DeleteProject(int id)
        {
            var project = _db.Projects.Find(id);
            if (project == null)
                return NotFound();

            _db.Projects.Remove(project);
            _db.SaveChanges();

            return Ok(project);
        }
    }
}