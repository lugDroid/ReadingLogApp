using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Core;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Books
{
    public class ListModel : PageModel
    {
        private readonly IReadingLogRepository logRepository;

        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public Dictionary<int, string> AuthorNames { get; set; } = new Dictionary<int, string>();
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        
        public ListModel (IReadingLogRepository logRepository)
        {
            this.logRepository = logRepository;
        }
        public void OnGet()
        {
            //Books = logRepository.GetAllBooks();
            Books = logRepository.GetBooksByName(SearchTerm);
            Authors = logRepository.GetAllAuthors();

            foreach (var auth in Authors)
            {
                AuthorNames[auth.Id] = $"{auth.FirstName} {auth.LastName}";
            }
        }
    }
}