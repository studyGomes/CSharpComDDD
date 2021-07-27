using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context;
        private DbSet<T> _dataset;

        public BaseRepository(MyContext context)
        {
            _context = context ;
            _dataset = _context.Set<T>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                // ID vindo do FRONTEND
                // Busca no banco para confrontar a informação.
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));

                if (result == null)
                    return false;

                // UPDATE

                // UPDATE VIRTUAL    
                /*
                var _item = result ;
                _item.DeleteAt = DateTime.UtcNow;
                _item.CreateAt = result.CreateAt;

                _context.Entry(result).CurrentValues.SetValues(_item);
                // Até Aqui Virtual
                */
                // UPDATE FISICO
                _dataset.Remove(result);
                // Até aqui Fisico

                // Aguarda salvar no banco de Dados
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                
                throw ;
            }
           
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if(item.Id == Guid.Empty){
                    item.Id = Guid.NewGuid();
                }

                // CREATE
                // Tabela recebendo os dados Entity BASE
                item.CreateAt = DateTime.UtcNow;

                // Entidade T, pode ser Usuario, Cliente, Tabelas, etc.. 
                _dataset.Add(item);

                // Aguarda salvar no banco de Dados
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                
                throw;
            }
            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                // ID vindo do FRONTEND
                // Busca no banco para confrontar a informação.
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

                if (result == null)
                    return null;
                
                // UPDATE
                item.UpdateAt = DateTime.UtcNow;
                item.CreateAt = result.CreateAt;

                _context.Entry(result).CurrentValues.SetValues(item);

                // Aguarda salvar no banco de Dados
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                
                throw ;
            }

            return item;
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                // Listar apenas os registros NÃO EXCLUIDOS
                return await _dataset   .ToListAsync(); //.Include(p => p.DeleteAt != null ) 
                                                        // .ToListAsync(); 
            }
            catch (Exception)
            {
                
                throw;
            } 
        }


    }
}
