using LibraryBackEnd.Model;
using System.Collections.Generic;
namespace LibraryBackEnd.Model.BookDTO
{
    public class BookDetailDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverUrl { get; set; }
        public string Content { get; set; }
        public int AverageRating { get; set; }
        public List<ReviewDTO> Reviews { get; set; }
    }
}
