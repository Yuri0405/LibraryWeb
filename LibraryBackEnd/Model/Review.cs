namespace LibraryBackEnd.Model
{
    public class Review
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public int BookID { get; set; }
        public string Rewiewer { get; set; }
        public Book Book { get; set; }
    }
}
