using System.Collections.Generic;

namespace LibraryBackEnd.Model
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
