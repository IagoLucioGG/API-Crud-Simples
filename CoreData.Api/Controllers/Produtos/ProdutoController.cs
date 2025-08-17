using CoreData.DTOs.Produtos;
using CoreData.Exceptions;
using CoreData.Models.Produtos;
using CoreData.Models.ResponseModel;
using CoreData.Services.Produtos;
using Microsoft.AspNetCore.Mvc;

namespace CoreData.Api.Controllers
{
    [ApiController]
    [Route("produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<Produto>>> Post(ProdutoDTO dto)
        {
            try
            {
                var response = await _service.CadastrarProduto(dto);
                return Ok(response);
            }
            catch (NotFoundException<Produto> ex)
            {
                return NotFound(ResponseModel<Produto>.Erro(ex.Message));
            }
            catch (ConflitoException<Produto> ex)
            {
                return Conflict(ResponseModel<Produto>.Erro(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseModel<Produto>.Erro(ex.Message));
            }
        }

        [HttpPut("{idProduto}")]
        public async Task<ActionResult<ResponseModel<Produto>>> Put(int idProduto, ProdutoDTO dto)
        {
            try
            {
                var response = await _service.EditarCliente(dto, idProduto);
                return Ok(response);
            }
            catch (NotFoundException<Produto> ex)
            {
                return NotFound(ResponseModel<Produto>.Erro(ex.Message));
            }
            catch (ConflitoException<Produto> ex)
            {
                return Conflict(ResponseModel<Produto>.Erro(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseModel<Produto>.Erro(ex.Message));
            }
        }

        [HttpGet("{idProduto}")]
        public async Task<ActionResult<ResponseModel<Produto>>> Get(int idProduto)
        {
            var response = await _service.ObterProdutoPorId(idProduto);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<Produto>>>> GetAll()
        {
            var response = await _service.ListarTodosProdutos();
            return Ok(response);
        }

        [HttpDelete("{idProduto}")]
        public async Task<ActionResult<ResponseModel<Produto>>> Delete(int idProduto)
        {
            try
            {
                var response = await _service.DeletarProduto(idProduto);
                return Ok(response);
            }
            catch (NotFoundException<Produto> ex)
            {
                return NotFound(ResponseModel<Produto>.Erro(ex.Message));
            }
        }
    }
}
