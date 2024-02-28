using MinhaPrimeiraApi.Repositorios.Contexto;
using MinhaPrimeiraApi.Repositorios.Interfaces.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Repositorios.Repositorios.Base
{
    public abstract class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : class
    {
        protected readonly IMongoDbContexto Contexto;
        protected IMongoCollection<TEntity> DbSet;

        protected RepositorioBase(IMongoDbContexto contexto)
        {
            Contexto = contexto;
            DbSet = Contexto.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            Contexto.AddCommand(() => DbSet.InsertOneAsync(obj));
        }

        public virtual async Task<TEntity> GetByIdAsync(ObjectId id)
        {
            var obj = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return obj.FirstOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var objs = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return objs.ToList();
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            Contexto.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj));
        }

        public virtual async Task DeleteAsync(ObjectId id)
        {
            Contexto.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
        }

        public void Dispose()
        {
            Contexto?.Dispose();
        }
    }
}