using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Books
{
    public class ListModel : PageModel
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public Dictionary<int, string> AuthorNames { get; set; } = new Dictionary<int, string>();
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        
        public ListModel (IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        public void OnGet()
        {
            Books = bookRepository.GetBooksByName(SearchTerm);
            Authors = authorRepository.GetAllAuthors();

            foreach (var auth in Authors)
            {
                AuthorNames[auth.Id] = $"{auth.FirstName} {auth.LastName}";
            }
        }
    }
}