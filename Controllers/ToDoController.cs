using ToDoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ToDoController : ControllerBase
    {
        public static List<ToDoItem> tasks = new List<ToDoItem>
        {
            new ToDoItem
            {
                Id=1,
                Title="Learn ASP Net",
                IsCompleted=false
            }
        };

        [HttpGet]

        public ActionResult<IEnumerable<ToDoItem>> GetAll()
        {
            return Ok(tasks);
        }

        [HttpGet("{id}")]

        public ActionResult<ToDoItem> GetById(int id)
        {
            var task = tasks.FirstOrDefault(t=>t.Id==id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
        //[HttpPost]
        //public ActionResult<ToDoItem> Create(ToDoItem item)
        //{
        //    item.Id = tasks.Count + 1;
        //    tasks.Add(item);
        //    return Ok(item);
        //}

        [HttpPost]
        public ActionResult<ToDoItem> Create(ToDoItem item)
        {
            item.Id = tasks.Count + 1;
            tasks.Add(item);

            return CreatedAtAction(nameof(GetAll), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]

        public ActionResult<ToDoItem> Update(int id,ToDoItem updatedItem)
        {
            var item = tasks.FirstOrDefault(x => x.Id == id);

            if (item == null) return NotFound();

            item.Title = updatedItem.Title;
            item.IsCompleted = updatedItem.IsCompleted;

            return NoContent();
        }

        [HttpDelete("{id}")]

        public ActionResult<ToDoItem> Delete (int id)
        {
            var item = tasks.FirstOrDefault(x => x.Id == id); ;
            if (item == null) return NotFound();

            tasks.Remove(item);

            return NoContent();
        }

    }
}
