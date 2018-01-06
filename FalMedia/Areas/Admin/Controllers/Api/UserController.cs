using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FalMedia.Areas.Admin.Core;
using FalMedia.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FalMedia.Areas.Admin.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [EnableCors("http://falmedia.sa", "*", "*", SupportsCredentials = true)]
    public class UserController : ApiController
    {
        private GroupManager _groupManager;
        private UserManager _userManager;

        public GroupManager GroupManager
        {
            get { return _groupManager ?? new GroupManager(); }
            private set { _groupManager = value; }
        }

        public UserManager UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<UserManager>(); }
            private set { _userManager = value; }
        }

        // DELETE: api/User/5
        //[Route("api/User/{id}")]
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            if (id == null) return NotFound();
            var user = UserManager.FindById(id);

            if (user == null)
                return NotFound();

            GroupManager.ClearUserGroups(id);
            var result = UserManager.Delete(user);
            if (!result.Succeeded)
                ModelState.AddModelError("Error", result.Errors.First());

            return Ok(user);
        }
    }
}