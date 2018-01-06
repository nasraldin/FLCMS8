using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [EnableCors("http://falmedia.sa", "*", "*", SupportsCredentials = true)]
    public class ContactController : ApiController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // DELETE: api/Contact/5
        //[Route("api/Contact/{id}")]
        [HttpPost]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {
            var contact = _db.Contacts.Find(id);
            if (contact == null)
                return NotFound();

            _db.Contacts.Remove(contact);
            _db.SaveChanges();

            return Ok(contact);
        }
    }
}