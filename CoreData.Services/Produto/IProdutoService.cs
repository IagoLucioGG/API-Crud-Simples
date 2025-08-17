using CoreData.DTOs.Produtos;
using CoreData.Models.Produtos;
using CoreData.Models.ResponseModel;

namespace CoreData.Services.Produtos
{
    public interface IProdutoService
    {
        public Task<ResponseModel<Produto>> CadastrarProduto(ProdutoDTO dto);
        public Task<ResponseModel<Produto>> EditarCliente(ProdutoDTO dto, int idProduto);
        public Task<ResponseModel<Produto>> DeletarProduto(int idProduto);
        public Task<ResponseModel<List<Produto>>> ListarTodosProdutos();
        public Task<ResponseModel<Produto>> ObterProdutoPorId(int idProduto);
    }
}