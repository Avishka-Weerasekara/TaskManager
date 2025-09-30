using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _service;

        public TaskController(TaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<TaskItem>> Get() => _service.GetAll();

        [HttpPost]
        public ActionResult<TaskItem> Post([FromBody] TaskItem task)
        {
            var added = _service.AddTask(task.Name);
            return CreatedAtAction(nameof(Post), new { id = added.Id }, added);
        }
    }
}
