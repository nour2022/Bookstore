using BookstoreNew.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreNew.ViewModels
{
    public class BookAuthorViewModels
    {
        public DateTime Date { get; set; }
        public int BookID { get; set; }
        [Required]
        public int AuthorID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; }
        public string ImgUrl { get; set; }

    }
}
