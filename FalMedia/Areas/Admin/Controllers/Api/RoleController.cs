using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FalMedia.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FalMedia.Areas.Admin.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [EnableCors("http://falmedia.sa", "*", "*", SupportsCredentials = true)]
    public class RoleController : ApiController
    {
        private RoleManager _roleManager;

        public RoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.Current.GetOwinContext().Get<RoleManager>(); }
            private set { _roleManager = value; }
        }

        // DELETE: api/Role/5
        //[Route("api/Role/{id}")]
        [HttpPost]
        [ResponseType(typeof(Role))]
        public IHttpActionResult DeleteRole(string id)
        {
            if (id == null)
                return NotFound();
            var role = RoleManager.FindById(id);
            if (role == null)
                return NotFound();

            var result = RoleManager.Delete(role);
            if (!result.Succeeded)
                ModelState.AddModelError("Error", result.Errors.First());


            return Ok(role);
        }
    }
}