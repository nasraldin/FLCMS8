using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FalMedia.Areas.Admin.Core;
using FalMedia.Areas.Admin.Models;

namespace FalMedia.Areas.Admin.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [EnableCors("http://falmedia.sa", "*", "*", SupportsCredentials = true)]
    public class GroupController : ApiController
    {
        private GroupManager _groupManager;

        public GroupManager GroupManager
        {
            get { return _groupManager ?? new GroupManager(); }
            private set { _groupManager = value; }
        }

        // DELETE: api/Group/5
        //[Route("api/Group/{id}")]
        [HttpPost]
        [ResponseType(typeof(Group))]
        public IHttpActionResult DeleteGroup(string id)
        {
            if (id == null)
                return NotFound();
            var group = GroupManager.FindById(id);
            if (group == null)
                return NotFound();
            GroupManager.DeleteGroup(id);

            return Ok(group);
        }
    }
}