using CoreData.DTOs.Cliente;
using CoreData.Models.Clientes;
using CoreData.Models.ResponseModel;

namespace CoreData.Services.Clientes
{
    public interface IClienteService
    {
        public Task<ResponseModel<Cliente>> CadastrarCliente(ClienteDto dto);
        public Task<ResponseModel<Cliente>> EditarCliente(ClienteDto dto, int idCliente);
        public Task<ResponseModel<Cliente>> DeletarCliente(int idCliente);
        public Task<ResponseModel<Cliente>> ObterClientePorId(int idCliente);
        public Task<ResponseModel<List<Cliente>>> ListarTodosClientes();
        public Task<ResponseModel<List<Cliente>>> ListarClientesAtivos();
        public Task<ResponseModel<List<Cliente>>> ListarClientesPorNome(string nome);
        
    }
}