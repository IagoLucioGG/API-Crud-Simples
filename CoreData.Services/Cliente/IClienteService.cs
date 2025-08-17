using CoreData.DTOs.Cliente;
using CoreData.Models.Clientes;
using CoreData.Models.ResponseModel;

namespace CoreData.Services.Clientes
{
    public interface IClienteService
    {
        public Task<ResponseModel<Cliente>> CadastrarCliente(ClienteDto dto);
        public Task<ResponseModel<Cliente>> EditarCliente(ClienteDto dto, int idCliente);
    }
}