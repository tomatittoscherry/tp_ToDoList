using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using tp_ToDoList.Repository;
using tp_ToDoList.Services;
using tp_ToDoList.Services.Interfaces;
using tp_ToDoList.Domain.Entities;
using tp_ToDoList.Domain.DTO;


namespace tp_ToDoList.Controllers
{

    [ApiController]
    [Route("api/controller")]

    public class TareasController : ControllerBase
    {
        private ITareasService _tareasService;

        public TareasController(ITareasService tareasService) //Inyeccion de dependencia
        {
            _tareasService = tareasService;
        }


        [HttpGet("Tareas Dadas de Baja")]
        public async Task<IActionResult> GetTareasDadasDeBaja()
        {
            var result = await _tareasService.GetAllTareasDadasDeBajaAsync();
            return Ok(result);
        }
        

        [HttpGet("Tareas Activas")]
        public async Task<IActionResult> GetTareasActivas()
        {
            var result = await _tareasService.GetAllTareasActivasAsync();
            return Ok(result);
        }


        [HttpPost("Agregado de Tareas")]
        public async Task<IActionResult> AddTareas([FromBody] TareasDTO request)
        {
            var result = await _tareasService.AddTareasAsync(request);

            if (!result) return BadRequest(new { Message = "No se pudo agregar la tarea" });

            return Created("", new { Message = "Agregada correctamente..." });
        }


        [HttpDelete("Eliminación de Tareas")]
        public async Task<IActionResult> DeleteTareas(int id)
        {
            var result = await _tareasService.DeleteTareasAsync(id);

            if (!result) return BadRequest(new { Message = "No se pudo eliminar la tarea" });

            return Ok(new { Message = "Eliminada correctamente..." });
        }


        [HttpPut("Update de Tareas")]
        public async Task<IActionResult> UpdateTareas(int id, [FromBody] TareasDTO request)
        {
            var result = await _tareasService.UpdateTareasAsync(id, request);

            if (!result) return BadRequest(new { Message = "No se pudo actualizar la tarea" });

            return Ok(new { Message = "Actualizada correctamente..." });

        }

    }
}
