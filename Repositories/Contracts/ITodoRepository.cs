using TodoApi.Models;

namespace TodoApi.Repositories.Contracts
{
  public interface ITodoRepository
  {
    public Task<bool> Create(TodoItemDTO todoItems);
    public Task Update(long id, TodoItemDTO todoItems);
    public Task Remove(long id);
    public bool VerifyIfExist(long id);
    public Task<TodoItemDTO> GetById(long id);
    public Task<IEnumerable<TodoItemDTO>> GetAll();
  }
}
