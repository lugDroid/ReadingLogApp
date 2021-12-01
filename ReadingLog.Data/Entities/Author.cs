using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ReadingLog.Data
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
        public decimal AvgRating { get; set; }

        public int GetBooksCount(Status status)
        {
            var query = from b in Books
                        where b.Status == status
                        select b;

            return query.Count();
        }

        public decimal GetAvgRating()
        {
            var aggregateRating = 0;

            foreach(var b in Books)
            {
                if (b.Status == Status.Finished)
                    aggregateRating += b.Rating;
            }

            if (Books.Count() != 0)
                AvgRating = (decimal)aggregateRating / Books.Count();

            return AvgRating;
        } 
    }
}