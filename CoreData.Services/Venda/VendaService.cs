using CoreData.Data.Context;
using CoreData.DTOs.Vendas;
using CoreData.Exceptions;
using CoreData.Models.Clientes;
using CoreData.Models.Produtos;
using CoreData.Models.ResponseModel;
using CoreData.Models.Usuario;
using CoreData.Models.Vendas;
using CoreData.Services.Base;
using CoreData.Services.Utils;
using Microsoft.IdentityModel.Tokens;

namespace CoreData.Services.Vendas
{
    public class VendaService : ServicoBase<Venda>, IVendaService
    {
        public VendaService(AppDbContext context) : base(context)
        { }

        private async Task VerificaDados(VendaDTO dto)
        {
            var usuario = await ObterPorIdModelo<Usuario>(dto.IdUsuario);
            var cliente = await ObterPorIdModelo<Cliente>(dto.IdCliente);
            var produto = await ObterPorIdModelo<Produto>(dto.IdProduto);
            if (usuario.Dados == null)
                throw new NotFoundException<Venda>(ResponseModel<Venda>.Erro("Usuario não encontrado na base com esse Id."));

            if (cliente.Dados == null)
                throw new NotFoundException<Venda>(ResponseModel<Venda>.Erro("Cliente não encontrado na base com esse Id."));

            if (produto.Dados == null)
                throw new NotFoundException<Venda>(ResponseModel<Venda>.Erro("Produto não encontrado na base com esse Id."));
        }

        private void VerificaDataCancelamento(VendaDTO dto, int tipo, int idVenda = 0)
        {
            var mensagem = "";
            switch (tipo)
            {
                case 1:
                    if (dto.DataCancelamento != null)
                        mensagem = "Não pode ser enviado data de cancelamento no cadastro de uma venda.";
                    break;
                case 2:
                    if (dto.DataCancelamento < dto.DataVenda)
                        mensagem = "A  data de cancelamento não pode ser menor que a data da venda.";
                    break;

            }

            if (mensagem.IsNullOrEmpty())
            {
                throw new DadosIncorretosException<Venda>(ResponseModel<Venda>.Erro(mensagem));
            }

        }

        public async Task<ResponseModel<Venda>> CadastrarVenda(VendaDTO dto)
        {
            await VerificaDados(dto);
            VerificaDataCancelamento(dto, 1);

            var novaVenda = MapeadorModels.Montar<Venda, VendaDTO>(dto);

            return await Cadastrar(novaVenda);
        }

        public async Task<ResponseModel<Venda>> EditarVenda(VendaDTO dto, int idVenda)
        {
            await VerificaDados(dto);
            VerificaDataCancelamento(dto, 2);

            var vendaExistente = await ObterPorId(idVenda);
            if (vendaExistente.Dados == null)
            {
                throw new NotFoundException<Venda>(ResponseModel<Venda>.Erro("Venda não encontrada na base com esse Id."));
            }

            MapeadorModels.CopiarPropriedades(dto, vendaExistente.Dados);

            return await Atualizar(vendaExistente.Dados);
        }

        public async Task<ResponseModel<Venda>> DeletarVenda(int idVenda)
        {
            var vendaDeletada = await ObterPorId(idVenda);
            if (vendaDeletada.Dados == null)
            {
                throw new NotFoundException<Venda>(ResponseModel<Venda>.Erro("Venda não encontrada na base com esse Id."));
            }

            return await Deletar(idVenda);

        }

        public async Task<ResponseModel<List<Venda>>> ListarTodasVendas()
        {
            return await ListarTodos();
        }

        public async Task<ResponseModel<Venda>> ObterVendaPorId(int idVenda)
        {
            return await ObterPorId(idVenda);
        }
    }
}