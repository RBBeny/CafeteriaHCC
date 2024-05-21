using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeteriaHCC.Models;
using CafeteriaHCC.Utilidades;
using System.Text.Json;

namespace CafeteriaHCC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private  CafeteriaContext _context;

        public OrdenesController(CafeteriaContext context)
        {
            _context = context;
        }
        //- Obtener el número total de órdenes y en que mesa están.
        // GET: api/Ordenes/TotalPorMesa
        [HttpGet("TotalPorMesa")]
        public async Task<IActionResult> GetTotalOrdenesPorMesa()
        {
            var resultado = await _context.Ordenes
                .Where(o => o.Estatus == 1)
                .GroupBy(o => o.MesaId)
                .Select(g => new MesaOrdenes
                {
                    MesaId = g.Key,
                    TotalOrdenes = g.Count()
                })
                .ToListAsync();

            string resultadoJson = JsonSerializer.Serialize(resultado);

            if (resultado == null)
            {
                return MesajeRespuestaApi.CrearRespuesta("No Se encontraron ordenes", 0, 500);
            }
            return MesajeRespuestaApi.CrearRespuesta(resultadoJson, 1, 200);
        }
        // Obtener el número total de mesas disponibles y la canƟdad de lugares por mesa.
        // GET: api/Mesas/Disponibles
        [HttpGet("MesasDisponibles")]
        public async Task<IActionResult> GetMesasDisponibles()
        {
            var mesasDisponibles = await _context.Mesas
                .Where(m => m.Disponible == 1 && m.Estatus == 1) // Filtrar por mesas disponibles y con estatus 1
                .Select(m => new MesaDisponible
                {
                    MesaId = m.Id,
                    Lugares = m.Lugares
                })
                .ToListAsync();

            var resultado = new MesasDisponiblesResponse
            {
                TotalMesasDisponibles = mesasDisponibles.Count,
                MesasDisponibles = mesasDisponibles
            };
            if(resultado == null)
            {
                return MesajeRespuestaApi.CrearRespuesta("No Se encontraron ordenes", 0, 500);
            }
            string resultadoJson = JsonSerializer.Serialize(resultado);
            return MesajeRespuestaApi.CrearRespuesta(resultadoJson, 1, 200);


        }
        //- Insertar una nueva orden
        // POST: api/Ordenes
        [HttpPost("AgregarOrden")]
        public async Task<IActionResult> PostOrden(Ordenes orden)
        {
            try
            {
                _context.Ordenes.Add(orden);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetOrden", new { id = orden.Id }, orden);
                return MesajeRespuestaApi.CrearRespuesta("Se agrego Orden ", 1, 200);
            }
            catch (Exception ex)
            {
                return MesajeRespuestaApi.CrearRespuesta("No se agrego la orden: "+ex.Message, 0, 500);
            }
        }

        // Actualizar orden (Agregar nuevo producto)
        // POST: api/OrdenesDetalle
        [HttpPost("AgregarDetalle")]
        public async Task<IActionResult> PostOrdenDetalle(OrdenesDetalle nuevoDetalle)
        {
            try
            {
                // Verificar que la orden a la que se le va a agregar el detalle existe
                var orden = await _context.Ordenes.FindAsync(nuevoDetalle.Id);
                if (orden == null)
                {
                    return MesajeRespuestaApi.CrearRespuesta("La orden especificada no existe. ", 0, 500);
                }

                // Verificar que el producto a agregar existe en la base de datos
                var productoExistente = await _context.Productos.FindAsync(nuevoDetalle.ProductoId);
                if (productoExistente == null)
                {
                    return MesajeRespuestaApi.CrearRespuesta("El producto especificado no existe.", 0, 500);
                }

                _context.OrdenDetalle.Add(nuevoDetalle);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                CreatedAtAction("GetOrdenDetalle", new { id = nuevoDetalle.OrdenId }, nuevoDetalle);
                return MesajeRespuestaApi.CrearRespuesta("Detalle Agregado", 0, 200);

            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                return MesajeRespuestaApi.CrearRespuesta("No se agrego el detalle: " + ex.Message, 0, 500);
            }
        }

        // - Actualizar orden (Cambiar estatus)
        // PUT: api/Ordenes/5/ActualizarEstatus
        [HttpPut("{id}/ActualizarEstatus")]
        public async Task<IActionResult> ActualizarEstatus(int id, [FromBody] byte nuevoEstatus)
        {
            try
            {
 
                var orden = await _context.Ordenes.FindAsync(id);

                if (orden == null)
                {
                    return MesajeRespuestaApi.CrearRespuesta("No existe orden", 0, 500);
                }

                // Actualizar el estatus de la orden
                orden.Estatus = nuevoEstatus;
                await _context.SaveChangesAsync();

                return MesajeRespuestaApi.CrearRespuesta("Orden actualizada", 0, 200);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción 
                return MesajeRespuestaApi.CrearRespuesta("No se actualizo la orden: " + ex.Message, 0, 500);
            }
        }

        // -Eliminar orden (borrado lógico)
        // PUT: api/Ordenes/5/EliminarOrden
        [HttpDelete("{id}/EliminarOrden")]
        public async Task<IActionResult> AEliminarOrden(int id)
        {
            try
            {
                // Buscar la orden existente por su ID
                var orden = await _context.Ordenes.FindAsync(id);

                if (orden == null)
                {
                    return MesajeRespuestaApi.CrearRespuesta("No existe orden", 0, 500);
                }

                // Actualizar el estatus de la orden
                orden.Estatus = 0;
                await _context.SaveChangesAsync();
                return MesajeRespuestaApi.CrearRespuesta("Orden Eliminada", 0, 200);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                return MesajeRespuestaApi.CrearRespuesta("No se pudo eliminar orden" + ex.Message, 0, 500);
            }
        }


        // GET: api/Ordenes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ordenes>>> GetOrdenes()
        {
            return await _context.Ordenes
                .Where(o => o.Estatus == 1)
                .Include(o => o.Mesa)
                .Include(o => o.CategoriaOrden)
                .ToListAsync();
        }        
    }
}