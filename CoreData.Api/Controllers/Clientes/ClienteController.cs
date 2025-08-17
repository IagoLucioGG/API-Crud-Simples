using CoreData.DTOs.Cliente;
using CoreData.Exceptions;
using CoreData.Models.Clientes;
using CoreData.Models.ResponseModel;
using CoreData.Services.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace CoreData.Api.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<Cliente>>> Post(ClienteDto dto)
        {
            try
            {
                var response = await _service.CadastrarCliente(dto);
                return Ok(response);
            }
            catch (NotFoundException<Cliente> ex)
            {
                return NotFound(ResponseModel<Cliente>.Erro(ex.Message));
            }
            catch (ConflitoException<Cliente> ex)
            {
                return Conflict(ResponseModel<Cliente>.Erro(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseModel<Cliente>.Erro(ex.Message));
            }
        }

        [HttpPut("{idCliente}")]
        public async Task<ActionResult<ResponseModel<Cliente>>> Put(int idCliente, ClienteDto dto)
        {
            try
            {
                var response = await _service.EditarCliente(dto, idCliente);
                return Ok(response);
            }
            catch (NotFoundException<Cliente> ex)
            {
                return NotFound(ResponseModel<Cliente>.Erro(ex.Message));
            }
            catch (ConflitoException<Cliente> ex)
            {
                return Conflict(ResponseModel<Cliente>.Erro(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseModel<Cliente>.Erro(ex.Message));
            }
        }

        [HttpGet("{idCliente}")]
        public async Task<ActionResult<ResponseModel<Cliente>>> Get(int idCliente)
        {
            var response = await _service.ObterClientePorId(idCliente);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet("ativos")]
        public async Task<ActionResult<ResponseModel<List<Cliente>>>> GetAtivos()
        {
            var response = await _service.ListarClientesAtivos();
            return Ok(response);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<ResponseModel<List<Cliente>>>> GetPorNome([FromQuery] string nome)
        {
            var response = await _service.ListarClientesPorNome(nome);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<Cliente>>>> GetAll()
        {
            var response = await _service.ListarTodosClientes();
            return Ok(response);
        }

        [HttpDelete("{idCliente}")]
        public async Task<ActionResult<ResponseModel<Cliente>>> Delete(int idCliente)
        {
            try
            {
                var response = await _service.DeletarCliente(idCliente);
                return Ok(response);
            }
            catch (NotFoundException<Cliente> ex)
            {
                return NotFound(ResponseModel<Cliente>.Erro(ex.Message));
            }
        }
    }
}
