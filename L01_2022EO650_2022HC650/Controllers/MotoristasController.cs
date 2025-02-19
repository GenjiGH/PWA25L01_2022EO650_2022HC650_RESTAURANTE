using L01_2022EO650_2022HC650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2022EO650_2022HC650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristasController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;
        public MotoristasController(restauranteContext restauranteContext)
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
            List<Motoristas> ListadoMotoristas = (from e in _restauranteContext.motoristas select e).ToList();
            if (ListadoMotoristas.Count() == 0)
            {
                return NotFound();
            }
            return Ok(ListadoMotoristas);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            Motoristas? motorista = (from e in _restauranteContext.motoristas where e.motoristaId == id select e).FirstOrDefault();
            if (motorista == null)
            {
                return NotFound();
            }
            return Ok(motorista);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarMotorista([FromBody] Motoristas motorista)
        {
            try
            {
                _restauranteContext.motoristas.Add(motorista);
                _restauranteContext.SaveChanges();
                return Ok(motorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] Motoristas motoristaModificar)
        {
            Motoristas? motoristaActual = (from e in _restauranteContext.motoristas where e.motoristaId == id select e).FirstOrDefault();
            if (motoristaActual == null)
            {
                return NotFound();
            }

            motoristaActual.motoristaId = motoristaModificar.motoristaId;
            motoristaActual.nombreMotorista = motoristaModificar.nombreMotorista;

            _restauranteContext.Entry(motoristaActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(motoristaModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarMotorista(int id)
        {
            Motoristas? motorista = (from e in _restauranteContext.motoristas where e.motoristaId == id select e).FirstOrDefault();
            if (motorista == null)
            {
                return NotFound();
            }

            _restauranteContext.motoristas.Attach(motorista);
            _restauranteContext.motoristas.Remove(motorista);
            _restauranteContext.SaveChanges();

            return Ok(motorista);
        }

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByName(string filtro)
        {
            Motoristas? motorista = (from e in _restauranteContext.motoristas where e.nombreMotorista.Contains(filtro) select e).FirstOrDefault();
            if (motorista == null)
            {
                return NotFound();
            }
            return Ok(motorista);
        }
    }
}
