using L01_2022EO650_2022HC650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2022EO650_2022HC650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public PedidosController(restauranteContext context)
        {
            _restauranteContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedidos>>> GetPedidos()
        {
            return await _restauranteContext.Set<Pedidos>().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedidos>> GetPedido(int id)
        {
            var pedido = await _restauranteContext.Set<Pedidos>().FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        [HttpPost]
        public async Task<ActionResult<Pedidos>> PostPedido(Pedidos pedido)
        {
            _restauranteContext.Set<Pedidos>().Add(pedido);
            await _restauranteContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.pedidoId }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedidos pedido)
        {
            if (id != pedido.pedidoId)
            {
                return BadRequest();
            }

            _restauranteContext.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _restauranteContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _restauranteContext.Set<Pedidos>().FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _restauranteContext.Set<Pedidos>().Remove(pedido);
            await _restauranteContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("PORCliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<Pedidos>>> GetPedidosPorCliente(int clienteId)
        {
            var pedidos = await _restauranteContext.Set<Pedidos>()
                .Where(p => p.clienteId == clienteId)
                .ToListAsync();

            if (pedidos.Count == 0)
            {
                return NotFound();
            }

            return pedidos;
        }

        [HttpGet("PORMotorista/{motoristaId}")]
        public async Task<ActionResult<IEnumerable<Pedidos>>> GetPedidosPorMotorista(int motoristaId)
        {
            var pedidos = await _restauranteContext.Set<Pedidos>()
                .Where(p => p.motoristaId == motoristaId)
                .ToListAsync();

            if (pedidos.Count == 0)
            {
                return NotFound();
            }

            return pedidos;
        }

        private bool PedidoExists(int id)
        {
            return _restauranteContext.Set<Pedidos>().Any(e => e.pedidoId == id);
        }
    }
}
