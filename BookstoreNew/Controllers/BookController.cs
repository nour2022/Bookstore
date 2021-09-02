using BookstoreNew.Models;
using BookstoreNew.Models.Repositories;
using BookstoreNew.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreNew.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IHostingEnvironment hosting;

        public IBookStoreRepository<Author> AuthorRepository { get; }

        // GET: BookController
        public BookController(IBookStoreRepository<Book> bookRepository,
                            IBookStoreRepository<Author> authorRepository,
                            IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            AuthorRepository = authorRepository;
            this.hosting = hosting;
        }
        public ActionResult Index()
        {
            List<Book> books = bookRepository.list();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            Book book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModels {
                Authors = FillSelectItems()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModels bookAuthorMV)
        {
            var vmodel = new BookAuthorViewModels
            {
                Authors = FillSelectItems()
            };
           
            if (ModelState.IsValid)
            {

                try
                {
                    ///////
                    string fileName = string.Empty;
                    if (bookAuthorMV.File != null)
                    {
                       fileName = getImgUrl( bookAuthorMV);
                    }
                    if (bookAuthorMV.AuthorID == -1)
                    {
                        ViewBag.Message = "Please select an author!";
                       
                        return View(vmodel);

                    }
                    Book book = new Book
                    {
                        Id = bookAuthorMV.BookID,
                        Author = AuthorRepository.Find(bookAuthorMV.AuthorID),
                        BookName = bookAuthorMV.Title,
                        Description = bookAuthorMV.Description,
                        PublishDate = bookAuthorMV.Date,
                        ImgUrl = fileName
                    };
                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            else
                return View(vmodel);
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
           
            var model = new BookAuthorViewModels
            {
              
                Description = book.Description,
                Title = book.BookName,
                Date = book.PublishDate,
                AuthorID = book.Author.Id,
                Authors = AuthorRepository.list(),
              ImgUrl = book.ImgUrl,
              BookID = book.Id
              
            };
          
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModels bookAuthorMV)
        {
            try
            {
               
                if (bookAuthorMV.File != null)
                {
                    //Delete old img
                    string upload = Path.Combine(hosting.WebRootPath, "Ref");
                    string oldUrl = bookAuthorMV.ImgUrl;
                    string fullPath = Path.Combine(upload, oldUrl);
                    System.IO.File.Delete(fullPath);
                    //Save image
                    getImgUrl( bookAuthorMV);
                }
                else
                {
                   
                }
                Book book = new Book
                {
                    Id = bookAuthorMV.BookID,
                    Author = AuthorRepository.Find(bookAuthorMV.AuthorID),
                    BookName = bookAuthorMV.Title,
                    Description = bookAuthorMV.Description,
                    PublishDate = bookAuthorMV.Date,
                  
                    
                };
                book.ImgUrl = (bookAuthorMV.File == null) ? bookRepository.Find(id).ImgUrl : bookAuthorMV.File.FileName;
               
                    bookRepository.Edit(book,id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            Book book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book book)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> FillSelectItems()
        {
            var authors = AuthorRepository.list();
            if (authors != null && authors[0].Id != -1)
            { authors.Insert(0, new Author { Id = -1, FullName = "---select an author" }); }
            return authors;
        }
        string getImgUrl(BookAuthorViewModels bookAuthorMV)
        {
            string upload = Path.Combine(hosting.WebRootPath, "Ref");
            string fileName = bookAuthorMV.File.FileName;
            string fullPath = Path.Combine(upload, fileName);
            bookAuthorMV.File.CopyTo(new FileStream(fullPath, FileMode.Create));
            return fileName;
        }
    }
}
