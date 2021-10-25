using System.Collections.Generic;
using System.Linq;

namespace ReadingLog.Core
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Book> Books { get; set; } = new List<Book>();

        public int GetBooksCount(Status status)
        {
            var query = from b in Books
                        where b.Status == status
                        select b;

            return query.Count();
        }
    }
}