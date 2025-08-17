using CoreData.Data.Context;
using CoreData.Models.ResponseModel;
using Microsoft.EntityFrameworkCore;

namespace CoreData.Services.Base
{
    public class ServicoBase<TEntity> where TEntity : class, new()
    {
        protected readonly AppDbContext _context;

        public ServicoBase(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<ResponseModel<TEntity>> ObterPorId(int id)
        {
            var entidade = await _context.Set<TEntity>().FindAsync(id);
            var nomeEntidade = typeof(TEntity).Name;

            if (entidade == null)
                return ResponseModel<TEntity>.Erro($"{nomeEntidade} não encontrado.");

            return ResponseModel<TEntity>.Sucesso(entidade, $"{nomeEntidade} encontrado com sucesso.");
        }
        public virtual async Task<ResponseModel<TModelo>> ObterPorIdModelo<TModelo>(int id) where TModelo : class
        {
            var entidade = await _context.Set<TModelo>().FindAsync(id);
            var nomeEntidade = typeof(TModelo).Name;

            if (entidade == null)
                return ResponseModel<TModelo>.Erro($"{nomeEntidade} não encontrado.");

            return ResponseModel<TModelo>.Sucesso(entidade, $"{nomeEntidade} encontrado com sucesso.");
        }


        public virtual async Task<ResponseModel<List<TEntity>>> ListarTodos()
        {
            var lista = await _context.Set<TEntity>().ToListAsync();
            var nomeEntidade = typeof(TEntity).Name;
            return ResponseModel<List<TEntity>>.Sucesso(lista, $"{nomeEntidade}s listados com sucesso.");
        }


        public virtual async Task<ResponseModel<TEntity>> Cadastrar(TEntity entidade)
        {
            await _context.Set<TEntity>().AddAsync(entidade);
            await _context.SaveChangesAsync();
            var nomeEntidade = typeof(TEntity).Name;
            return ResponseModel<TEntity>.Sucesso(entidade, $"{nomeEntidade} cadastrado com sucesso.");
        }

        public virtual async Task<ResponseModel<TEntity>> Atualizar(TEntity entidade)
        {
            _context.Set<TEntity>().Update(entidade);
            await _context.SaveChangesAsync();
            var nomeEntidade = typeof(TEntity).Name;
            return ResponseModel<TEntity>.Sucesso(entidade, $"{nomeEntidade} atualizado com sucesso.");
        }

        public virtual async Task<ResponseModel<TEntity>> Deletar(int id)
        {
            var entidade = await _context.Set<TEntity>().FindAsync(id);
            var nomeEntidade = typeof(TEntity).Name;

            if (entidade == null)
                return ResponseModel<TEntity>.Erro($"{nomeEntidade} não encontrado.");

            _context.Set<TEntity>().Remove(entidade);
            await _context.SaveChangesAsync();

            return ResponseModel<TEntity>.Sucesso(entidade, $"{nomeEntidade} deletado com sucesso.");
        }

    }
}
