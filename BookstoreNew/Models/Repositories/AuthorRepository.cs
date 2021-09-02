using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreNew.Models.Repositories
{
    public class AuthorRepository:IBookStoreRepository<Author>
    {
        List<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>() {
            new Author{ Id =1 ,FullName = "Ahmed Mohamed"},
            new Author{Id=2 , FullName= "Mohamed Ahmed"},
            new Author{Id=3, FullName= "Ahmed Ahmed"}
            };
        }
        public void Add(Author entity)
        {
            entity.Id = authors.Last().Id + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            authors.Remove(Find(id));
        }

        public void Edit(Author entity, int id)
        {
            Author author = Find(id);
            author.FullName = entity.FullName;
            author.Id = entity.Id;
        }

        public Author Find(int id)
        {

            return authors.Find(author => author.Id == id);
        }

        public List<Author> list()
        {
            return authors;
        }
    }
}
