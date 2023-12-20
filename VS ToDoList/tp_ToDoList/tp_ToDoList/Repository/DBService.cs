namespace tp_ToDoList.Repository
{
    public class DBService
    {
        public readonly ToDoContext _ToDoContext;

        public DBService(ToDoContext context)
        {
            _ToDoContext = context;
        }
    }
}
