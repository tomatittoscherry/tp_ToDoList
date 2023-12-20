using tp_ToDoList.Domain.DTO;

namespace tp_ToDoList.Domain.Request
{
    public class UpdateTareasRequest
    {
        public int Id { get; set; }
        public TareasDTO InfoTareas { get; set; }

    }
}
