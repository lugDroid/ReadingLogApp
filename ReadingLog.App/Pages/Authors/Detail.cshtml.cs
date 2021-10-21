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
    public class DetailModel : PageModel
    {
        private readonly IReadingLogRepository readingLog;

        public Author Author { get; set; }

        public DetailModel(IReadingLogRepository readingLog)
        {
            this.readingLog = readingLog;
        }

        public IActionResult OnGet(int authorId)
        {
            Author = readingLog.GetAuthorById(authorId);

            if (Author == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }
    }
}