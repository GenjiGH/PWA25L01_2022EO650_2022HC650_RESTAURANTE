using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022EO650_2022HC650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2022EO650_2022HC650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;
        public PlatosController(restauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }

        /// <summary>
        /// ENdpoint que retorna el listado de todos los equipos existentes
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Platos> ListadoPlatos = (from e in _restauranteContext.platos select e).ToList();
            if (ListadoPlatos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(ListadoPlatos);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            Platos? plato = (from e in _restauranteContext.platos where e.platoId == id select e).FirstOrDefault();
            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPlato([FromBody] Platos plato)
        {
            try
            {
                _restauranteContext.platos.Add(plato);
                _restauranteContext.SaveChanges();
                return Ok(plato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPlato(int id, [FromBody] Platos platoModificar)
        {
            Platos? platoActual = (from e in _restauranteContext.platos where e.platoId == id select e).FirstOrDefault();
            if (platoActual == null)
            {
                return NotFound();
            }

            platoActual.platoId = platoModificar.platoId;
            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;

            _restauranteContext.Entry(platoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(platoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            Platos? plato = (from e in _restauranteContext.platos where e.platoId == id select e).FirstOrDefault();
            if (plato == null)
            {
                return NotFound();
            }

            _restauranteContext.platos.Attach(plato);
            _restauranteContext.platos.Remove(plato);
            _restauranteContext.SaveChanges();

            return Ok(plato);
        }

        [HttpGet]
        [Route("FiltrarPorPrecio/{precioMaximo}")]
        public IActionResult FilterByPrice(decimal precioMaximo)
        {
            var platos = _restauranteContext.platos
                                 .Where(p => p.precio < precioMaximo)
                                 .ToList();

            if (platos.Count == 0)
            {
                return NotFound("No hay platos con un precio menor a " + precioMaximo);
            }
            return Ok(platos);
        }

    }
}
