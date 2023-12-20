using tp_ToDoList.Repository;
using tp_ToDoList.Domain.Entities;
using tp_ToDoList.Domain.DTO;
using tp_ToDoList.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Channels;

namespace tp_ToDoList.Services
{
    public class TareasService : ITareasService
    {
        private readonly ToDoContext _ToDoContext;
        //private readonly ITareasRepository _tareasRepository;

        public TareasService(ToDoContext ToDoContext) //inyección de dependencia
        {
            _ToDoContext = ToDoContext;
        }
        /*public TareasService(ITareasRepository tareasRepository) //inyección de dependencia
        {
            _tareasRepository = tareasRepository;
        }*/

        /*public async Task<List<Tareas>> GetAllTareasAsync()
        {
            var result = await _tareasRepository.GetAllTareasAsync();
            return result;
        }*/

        public async Task<List<Tareas>> GetAllTareasAsync()
        {
            return await _ToDoContext.Tareas.ToListAsync();
        }

        public async Task<List<Tareas>> GetAllTareasActivasAsync()
        {
            return await _ToDoContext.Tareas.Where(task => task.Activo).ToListAsync();
        }

        public async Task<List<Tareas>> GetAllTareasDadasDeBajaAsync()
        {
            return await _ToDoContext.Tareas.Where(task => task.Activo == false).ToListAsync();
        }


        public async Task<bool> AddTareasAsync(TareasDTO tareas)
        {

            var newTarea = new Tareas();

            if (tareas.Titulo != null && tareas.Descripcion != null)
            {
                newTarea.Titulo = tareas.Titulo;
                newTarea.Descripcion = tareas.Descripcion;
                newTarea.FechaAlta = DateTime.Now;
                newTarea.FechaModificacion = DateTime.Now;
                newTarea.Activo = true;

                if (tareas.Estado.Trim() == "pendiente" || tareas.Estado.Trim() == "en curso" || tareas.Estado.Trim() == "finalizadas")
                {
                    newTarea.Estado = tareas.Estado.ToUpper();
                }
                else
                {
                    return false;
                }

                await _ToDoContext.Tareas.AddAsync(newTarea);
            }
            
            int rows = await _ToDoContext.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<bool> DeleteTareasAsync(int id)
        {
            var tareaEliminada = await _ToDoContext.Tareas.FirstOrDefaultAsync(t => t.Id == id);
            if (tareaEliminada == null) return false;
            tareaEliminada.Activo = false;

            int rows = await _ToDoContext.SaveChangesAsync();

            return rows > 0;
        }
        
        public async Task<bool> UpdateTareasAsync(int id, TareasDTO changes)
        {
            var updatedTarea = await _ToDoContext.Tareas.FirstOrDefaultAsync(t => t.Id == id && t.Activo);

            if (updatedTarea == null) return false;

            if (changes.Estado.ToLower() == "inconcluso" || changes.Estado.ToLower() == "en proceso" || changes.Estado.ToLower() == "finalizado")
            {
                updatedTarea.Estado = changes.Estado.ToUpper();
                updatedTarea.Descripcion = changes.Descripcion;
                updatedTarea.FechaModificacion = DateTime.Now;
                updatedTarea.Titulo = changes.Titulo;

                int rows = await _ToDoContext.SaveChangesAsync();
            }

            return false;
        }
    }
}
