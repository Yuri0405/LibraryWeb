namespace LibraryBackEnd.Model
{
    public class Rating
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int Score { get; set; }
        public Book Book { get; set; }
    }
}
