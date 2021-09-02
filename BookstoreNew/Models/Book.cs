using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreNew.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public Author Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }

    }
}
