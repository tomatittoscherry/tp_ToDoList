using System.Threading;
using tp_ToDoList.Domain.DTO;
using tp_ToDoList.Domain.Entities;

namespace tp_ToDoList.Services.Interfaces
{
    public interface ITareasService
    {
        public Task<List<Tareas>> GetAllTareasActivasAsync();
        public Task<List<Tareas>> GetAllTareasDadasDeBajaAsync();

        public Task<bool> AddTareasAsync(TareasDTO tarea);

        public Task<bool> UpdateTareasAsync(int id, TareasDTO changes);

        public Task<bool> DeleteTareasAsync(int id);
    }
}