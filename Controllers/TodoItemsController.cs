using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories.Contracts;

namespace TodoApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TodoItemsController : ControllerBase
  {
    private readonly ITodoRepository _todoRepository;

    public TodoItemsController(ITodoRepository todoRepository)
    {
      _todoRepository = todoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
    {
      var items = await _todoRepository.GetAll();

      return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
    {
      var todoItem = await _todoRepository.GetById(id);

      if (todoItem == null)
      {
        return NotFound();
      }

      return Ok(todoItem);
    }

    [HttpPost]
    public async Task<ActionResult> PostTodoItem(TodoItemDTO todoItemDTO)
    {
      var wasCreated = await _todoRepository.Create(todoItemDTO);

      if (!wasCreated)
      {
        throw new Exception("error on todoItem creation");
      }

      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
    {
      try
      {
        await _todoRepository.Update(id, todoItemDTO);
      }
      catch (DbUpdateConcurrencyException) when (!_todoRepository.VerifyIfExist(id))
      {
        return NotFound();
      }

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
      await _todoRepository.Remove(id);

      return NoContent();
    }

  }
}
