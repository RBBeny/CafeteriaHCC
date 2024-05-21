using CafeteriaHCC.Models.Respuestas;
using Microsoft.AspNetCore.Mvc;

namespace CafeteriaHCC.Utilidades
{
    public static class MesajeRespuestaApi
    {
        public static IActionResult CrearRespuesta(string mensaje, int codigo, int codigoEstatus)
        {
            var response = new RespuestaApi
            {
                Estatus = codigoEstatus,
                Mensaje = mensaje,
                Codigo = codigo
            };
            return new JsonResult(response) { StatusCode = codigoEstatus };
        }
    }
}

