using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Core;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Authors
{
    public class ListModel : PageModel
    {
        private readonly IReadingLogRepository logRepository;

        public IEnumerable<Author> Authors { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IReadingLogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public void OnGet()
        {
            Authors = logRepository.GetAuthorsByName(SearchTerm);
        }
    }
}