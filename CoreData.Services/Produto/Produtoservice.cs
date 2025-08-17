using CoreData.Data.Context;
using CoreData.DTOs.Produtos;
using CoreData.Exceptions;
using CoreData.Models.Produtos;
using CoreData.Models.ResponseModel;
using CoreData.Services.Base;
using CoreData.Services.Utils;
using Microsoft.EntityFrameworkCore;

namespace CoreData.Services.Produtos
{
    public class ProdutoService : ServicoBase<Produto>, IProdutoService
    {
        public ProdutoService(AppDbContext context) : base(context) { }

        private async Task VerificarProdutoDuplicado(ProdutoDTO dto, int tipo, int idProduto = 0)
        {
            Produto? existente = null;
            switch (tipo)
            {
                case 1:
                    existente = await _context.Produtos.FirstOrDefaultAsync(p => p.CdChamada == dto.CdChamada || p.NmProduto == dto.NmProduto);
                    break;

                case 2:
                    existente = await _context.Produtos.FirstOrDefaultAsync(p => (p.CdChamada == dto.CdChamada || p.NmProduto == dto.NmProduto) && p.IdProduto != idProduto);
                    break;
            }

            if (existente != null)
            {
                throw new ConflitoException<Produto>(ResponseModel<Produto>.Erro("Já existe um produto para o 'NmProduto ou Cdchamada' informado."));
            }

        }

        public async Task<ResponseModel<Produto>> CadastrarProduto(ProdutoDTO dto)
        {
            await VerificarProdutoDuplicado(dto, 1);

            var novoProduto = MapeadorModels.Montar<Produto, ProdutoDTO>(dto);
            return await Cadastrar(novoProduto);
        }

        public async Task<ResponseModel<Produto>> EditarCliente(ProdutoDTO dto, int idProduto)
        {
            var ProdutoExistente = await ObterPorId(idProduto);
            if (ProdutoExistente.Dados == null)
            {
                throw new NotFoundException<Produto>(ResponseModel<Produto>.Erro("Produto não encontrada na base com esse Id."));
            }

            await VerificarProdutoDuplicado(dto, 2, idProduto);

            MapeadorModels.CopiarPropriedades(dto, ProdutoExistente.Dados);

            return await Atualizar(ProdutoExistente.Dados);
        }

        public async Task<ResponseModel<Produto>> DeletarProduto(int idProduto)
        {
            var ProdutoDeletado = await ObterPorId(idProduto);
            if (ProdutoDeletado.Dados == null)
            {
                throw new NotFoundException<Produto>(ResponseModel<Produto>.Erro("Produto não encontrado na base com esse Id."));
            }

            return await Deletar(idProduto);
        }

        public async Task<ResponseModel<List<Produto>>> ListarTodosProdutos()
        {
            return await ListarTodos();
        }

        public async Task<ResponseModel<Produto>> ObterProdutoPorId(int idProduto)
        {
            return await ObterPorId(idProduto);
        }

        


    }
}