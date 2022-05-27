using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories.Contracts;

namespace TodoApi.Repositories.Implementations
{
  public class TodoItemsRepository : ITodoRepository
  {
    private readonly TodoContext _context;

    public TodoItemsRepository(TodoContext context)
    {
      _context = context;
    }
    public async Task<bool> Create(TodoItemDTO todoItemDTO)
    {
      try
      {
        var todoItem = new TodoItem
        {
          IsComplete = todoItemDTO.IsComplete,
          Name = todoItemDTO.Name
        };

        var itemId = _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return true;
      }
      catch
      {
        return false;
      }
    }
    public async Task Update(long id, TodoItemDTO todoItemDTO)
    {
      var todoItem = await GetById(id);

      if (todoItem == null)
      {
        throw new Exception("item not exist");
      }

      todoItem.Name = todoItemDTO.Name;
      todoItem.IsComplete = todoItemDTO.IsComplete;

      await _context.SaveChangesAsync();
    }
    public async Task Remove(long id)
    {
      var todoItem = await _context.TodoItems.FindAsync(id);

      if (todoItem == null)
      {
        throw new Exception("item not exist");
      }

      _context.TodoItems.Remove(todoItem);
      await _context.SaveChangesAsync();
    }
    public bool VerifyIfExist(long id)
    {
      var exist = _context.TodoItems.Any(e => e.Id == id);

      return exist;
    }
    public async Task<TodoItemDTO> GetById(long id)
    {
      var todoItem = await _context.TodoItems.FindAsync(id);

      if (todoItem == null)
      {
        throw new Exception("item not exist");
      }

      return TodoItemDTO.ItemToDTO(todoItem);
    }
    public async Task<IEnumerable<TodoItemDTO>> GetAll()
    {
      var items = await _context.TodoItems
        .Select(x => TodoItemDTO.ItemToDTO(x))
        .ToListAsync();

      return items;
    }
  }
}
