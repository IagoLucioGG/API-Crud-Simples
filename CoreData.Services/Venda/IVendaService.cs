using CoreData.DTOs.Vendas;
using CoreData.Models.ResponseModel;
using CoreData.Models.Vendas;

namespace CoreData.Services.Vendas
{
    public interface IVendaService
    {
        public Task<ResponseModel<Venda>> CadastrarVenda(VendaDTO dto);
        public Task<ResponseModel<Venda>> EditarVenda(VendaDTO dto, int idVenda);
        public Task<ResponseModel<Venda>> DeletarVenda(int idVenda);
        public Task<ResponseModel<List<Venda>>> ListarTodasVendas();
        public Task<ResponseModel<Venda>> ObterVendaPorId(int idVenda);
    }
}