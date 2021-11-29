using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Core;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Authors
{
    public class ListModel : PageModel
    {
        private readonly IAuthorRepository authorRepository;

        public IEnumerable<Author> Authors { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public void OnGet()
        {
            Authors = authorRepository.GetAuthorsByName(SearchTerm);
        }
    }
}