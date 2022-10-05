namespace LibraryBackEnd.Model.BookDTO
{
    public class BookDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverURL { get; set; }
        public int Raiting { get; set; }
        public int ReviewsNumber { get; set; }
    }
}
