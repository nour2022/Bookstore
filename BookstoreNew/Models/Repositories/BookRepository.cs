using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreNew.Models.Repositories
{
    public class BookRepository:IBookStoreRepository<Book>
    {
        List<Book> Book;
        public BookRepository()
        {
            Book = new List<Book>() {
            new Book{ Id = 1 ,
                BookName = "ASP.NET Core",
                Description="Book for teaching ASP.Net Core Princibles",
                Author=new Author{ Id =1},
                ImgUrl = "books.jpg"
                    } ,
            new Book{
                Id = 2 ,
                BookName = "C# Programming",
                Description="Book for teaching C# Princibles",
                Author=new Author{ Id = 2} ,
                ImgUrl = "books2.jpg"
            
            },
            new Book{ Id = 3 ,
                BookName = "OOP Principles",
                Description="Book for teaching OOP Princibles",
                Author=new Author{ Id=3},
                 ImgUrl = "books3.jpg"
            }


            };
        }
        public void Add(Book entity)
        {
            entity.Id = Book.Last().Id + 1;
            Book.Add(entity);
        }

        public void Delete(int id)
        {

            var book = Find(id);
            Book.Remove(book);
        }

        public void Edit(Book newBook, int id)
        {
            Book book = Find(id);
            book.BookName = newBook.BookName;
            book.Author = newBook.Author;
            book.Description = newBook.Description;
            book.PublishDate = newBook.PublishDate;
            book.ImgUrl = newBook.ImgUrl;
        }

        public Book Find(int id)
        {
            return Book.Find(b => b.Id == id);
        }

        public List<Book> list()
        {
            return Book;
        }
    }
}
