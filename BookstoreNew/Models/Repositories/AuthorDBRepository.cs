using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreNew.Models.Repositories
{
    public class AuthorDBRepository : IBookStoreRepository<Author>
    {
        BookStoreDbContext db;
        public AuthorDBRepository(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Author entity)
        {
           
           db.Authors.Add(entity);
            Commit();
        }

        public void Delete(int id)
        {
            db.Authors.Remove(Find(id));
           
            Commit();
        }

        public void Edit(Author entity, int id)
        {
            db.Update(entity);
            Commit();
        }
        
        public Author Find(int id)
        {
            Author author = db.Authors.SingleOrDefault(f => f.Id == id);
            return author;
        }

        public List<Author> list()
        {
            return db.Authors.ToList();
        }
        public void Commit()
        {
            db.SaveChanges();
           
        }
    }
}
