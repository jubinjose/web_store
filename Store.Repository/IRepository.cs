using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : ModelBase;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : ModelBase;

        void Delete<TEntity>(object id)
            where TEntity : ModelBase;

        void Delete<TEntity>(TEntity entity)
            where TEntity : ModelBase;

        void Save();

        Task SaveAsync();
    }
}
