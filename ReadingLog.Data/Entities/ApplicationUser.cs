using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ReadingLog.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
    }
}