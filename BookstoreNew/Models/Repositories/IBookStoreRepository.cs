using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreNew.Models.Repositories
{
   public interface IBookStoreRepository<TEntity>
    {
        List<TEntity> list();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Edit(TEntity entity, int id);
        void Delete(int id);
    }
}
