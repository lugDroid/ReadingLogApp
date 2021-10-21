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
    public class DeleteModel : PageModel
    {
        private readonly IReadingLogRepository logRepository;

        public Author Author { get; set; }

        public DeleteModel(IReadingLogRepository logRepository)
        {
            this.logRepository = logRepository;
        }
        public IActionResult OnGet(int authorId)
        {
            Author = logRepository.GetAuthorById(authorId);

            if (Author == null)
           {
               return RedirectToPage("./NotFound");
           } 

           return Page();
        }

        public IActionResult OnPost(int authorId)
        {
            Author = logRepository.DeleteAuthor(authorId);
            logRepository.Commit();

            if (Author == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["DeleteMessage"] = $"{Author.FirstName} {Author.LastName} was deleted";
            return RedirectToPage("./List");
        }
    }
}