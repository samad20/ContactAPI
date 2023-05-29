using ContactAPI.Data;
using ContactAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok( await dbContext.Contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts(AddContactRequest addContactRequest)
        {

            // link Contact model with AddContactRequest model
            var contsct = new Contact() { 
                Id = Guid.NewGuid(),  
                Name = addContactRequest.Name,  
                Email = addContactRequest.Email,    
                Phone = addContactRequest.Phone,
                Address = addContactRequest.Address
            };

            // update database
            await dbContext.Contacts.AddAsync(contsct);
            // save database
            await dbContext.SaveChangesAsync();

            return Ok(contsct);
        }
    }
}
