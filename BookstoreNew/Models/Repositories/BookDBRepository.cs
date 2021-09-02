using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreNew.Models.Repositories
{
    public class BookDBRepository : IBookStoreRepository<Book>
    {
        BookStoreDbContext db;
        public BookDBRepository(BookStoreDbContext _db)
        {
            db = _db;
           
        }
        public void Add(Book entity)
        {
          
            db.Books.Add(entity);
            Commit();
        }

        public void Delete(int id)
        {

            var book = Find(id);
            db.Books.Remove(book);
            Commit();
        }

        public void Edit(Book newBook, int id)
        {
            db.Update(newBook);
            Commit();
        }

        private void Commit()
        {
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            return db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);
        }

        public List<Book> list()
        {
            return db.Books.Include(a=> a.Author).ToList();
        }
    }
}
