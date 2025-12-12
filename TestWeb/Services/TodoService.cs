using Microsoft.EntityFrameworkCore;

public interface ITodoService {
    Task<IEnumerable<TodoTask>> GetPendingTodos();
}
public class TodoService : ITodoService 
{
 private readonly TodoContext _db;
 private readonly ILogger _logger;
 public TodoService(TodoContext db, ILogger<TodoService> logger)
 {
     _db = db;
     _logger = logger;
 }
    public async Task<IEnumerable<TodoTask>> GetPendingTodos()
    {
        var result  = _db.Todos.Where(t => t.DoneWhen == null);
        return await result.ToListAsync();
    }
}