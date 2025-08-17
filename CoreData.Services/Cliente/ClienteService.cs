using CoreData.Data.Context;
using CoreData.DTOs.Cliente;
using CoreData.Models.Clientes;
using CoreData.Exceptions;
using CoreData.Models.ResponseModel;
using CoreData.Services.Base;
using CoreData.Services.Utils;
using Microsoft.EntityFrameworkCore;

namespace CoreData.Services.Clientes
{
    public class ClienteService : ServicoBase<Cliente>, IClienteService
    {
        public ClienteService(AppDbContext context) : base(context)
        {
        }

        private void ValidarCpfCnpj(ClienteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CpfCnpj))
                throw new DadosIncorretosException<Cliente>(
                    ResponseModel<Cliente>.Erro("É necessário informar um CPF ou CNPJ para o cliente.")
                );
        }

        private async Task VerificarDuplicado(ClienteDto dto, int tipo, int idCliente = 0)
        {
            Cliente? existente = null;

            switch (tipo)
            {
                case 1:
                    existente = await _context.Clientes.FirstOrDefaultAsync(c => c.CpfCnpj == dto.CpfCnpj);
                    break;
                case 2:
                    existente = await _context.Clientes.FirstOrDefaultAsync(c => c.CpfCnpj == dto.CpfCnpj && c.IdCliente != idCliente);
                    break;
            }

            if (existente != null)
                throw new ConflitoException<Cliente>(
                    ResponseModel<Cliente>.Erro("Já existe um cliente para o CPF / CNPJ informado.")
                );
        }

        // CRUD principal
        public async Task<ResponseModel<Cliente>> CadastrarCliente(ClienteDto dto)
        {
            ValidarCpfCnpj(dto);
            await VerificarDuplicado(dto, 1);

            var novoCliente = MapeadorModels.Montar<Cliente, ClienteDto>(dto);
            return await Cadastrar(novoCliente);
        }

        public async Task<ResponseModel<Cliente>> EditarCliente(ClienteDto dto, int idCliente)
        {
            ValidarCpfCnpj(dto);
            await VerificarDuplicado(dto, 2, idCliente);

            var clienteExistente = await ObterPorId(idCliente);
            if (clienteExistente.Dados == null)
                throw new NotFoundException<Cliente>(
                    ResponseModel<Cliente>.Erro("Cliente não encontrado na base com esse Id.")
                );

            MapeadorModels.CopiarPropriedades(dto, clienteExistente.Dados);

            return await Atualizar(clienteExistente.Dados);
        }

        public async Task<ResponseModel<Cliente>> DeletarCliente(int idCliente)
        {
            var clienteExistente = await ObterPorId(idCliente);
            if (clienteExistente == null)
                throw new NotFoundException<Cliente>(
                    ResponseModel<Cliente>.Erro("Cliente não encontrado na base com esse Id.")
                );

            return await Deletar(idCliente);
        }

        // CONSULTAS usando ServicoBase
        public async Task<ResponseModel<Cliente>> ObterClientePorId(int idCliente)
        {
            return await ObterPorId(idCliente); // método do ServicoBase
        }

        public async Task<ResponseModel<List<Cliente>>> ListarTodosClientes()
        {
            return await ListarTodos(); // método do ServicoBase
        }

        public async Task<ResponseModel<List<Cliente>>> ListarClientesAtivos()
        {
            var ativos = await _context.Clientes.Where(c => c.Ativo).ToListAsync();
            return ResponseModel<List<Cliente>>.Sucesso(ativos, "Clientes ativos listados com sucesso.");
        }

        public async Task<ResponseModel<List<Cliente>>> ListarClientesPorNome(string nome)
        {
            var filtrados = await _context.Clientes
                .Where(c => c.NmCliente.Contains(nome))
                .ToListAsync();
            return ResponseModel<List<Cliente>>.Sucesso(filtrados, $"Clientes contendo '{nome}' no nome.");
        }
    }
}
