using CoreData.DTOs.Vendas;
using CoreData.Exceptions;
using CoreData.Models.ResponseModel;
using CoreData.Models.Vendas;
using CoreData.Services.Vendas;
using Microsoft.AspNetCore.Mvc;

namespace CoreData.Api.Controllers
{
    [ApiController]
    [Route("venda")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _service;

        public VendaController(IVendaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<Venda>>> Post(VendaDTO dto)
        {
            try
            {
                var response = await _service.CadastrarVenda(dto);
                return Ok(response);
            }
            catch (NotFoundException<Venda> ex)
            {
                return NotFound(ResponseModel<Venda>.Erro(ex.Message));
            }
            catch (DadosIncorretosException<Venda> ex)
            {
                return BadRequest(ResponseModel<Venda>.Erro(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseModel<Venda>.Erro(ex.Message));
            }
        }

        [HttpPut("{idVenda}")]
        public async Task<ActionResult<ResponseModel<Venda>>> Put(int idVenda, VendaDTO dto)
        {
            try
            {
                var response = await _service.EditarVenda(dto, idVenda);
                return Ok(response);
            }
            catch (NotFoundException<Venda> ex)
            {
                return NotFound(ResponseModel<Venda>.Erro(ex.Message));
            }
            catch (DadosIncorretosException<Venda> ex)
            {
                return BadRequest(ResponseModel<Venda>.Erro(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseModel<Venda>.Erro(ex.Message));
            }
        }

        [HttpGet("{idVenda}")]
        public async Task<ActionResult<ResponseModel<Venda>>> Get(int idVenda)
        {
            var response = await _service.ObterVendaPorId(idVenda);
            if (!response.Status)
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<Venda>>>> GetAll()
        {
            var response = await _service.ListarTodasVendas();
            return Ok(response);
        }

        [HttpDelete("{idVenda}")]
        public async Task<ActionResult<ResponseModel<Venda>>> Delete(int idVenda)
        {
            try
            {
                var response = await _service.DeletarVenda(idVenda);
                return Ok(response);
            }
            catch (NotFoundException<Venda> ex)
            {
                return NotFound(ResponseModel<Venda>.Erro(ex.Message));
            }
        }
    }
}
