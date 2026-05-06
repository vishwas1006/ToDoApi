using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ToDoController(AppDbContext context)
        {
            _context = context;
        }

        //Get all data
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAll()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        //Get By Id
        [HttpGet("{id}")]

        public async Task<ActionResult<ToDoItem>> GetById(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);

            if (item == null) return NotFound();

            return item;
        }

        //Create a record
        [HttpPost]

        public async Task<ActionResult<ToDoItem>> Create(ToDoItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
                return BadRequest("Title Required");

            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }





    }
}