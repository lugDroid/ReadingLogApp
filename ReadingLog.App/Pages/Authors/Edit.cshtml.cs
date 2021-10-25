using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Core;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly IReadingLogRepository logRepository;

        [BindProperty]
        public Author Author { get; set; }

        public EditModel(IReadingLogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public IActionResult OnGet(int? authorId)
        {
            if (authorId.HasValue)
            {
                Author = logRepository.GetAuthorById(authorId.Value);
            }
            else
            {
                Author = new Author();
            }

            if (Author == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Author.Id > 0)
            {
                TempData["EditResult"] = $"{Author.FirstName} {Author.LastName} Udpated";
                logRepository.UpdateAuthor(Author);
            }
            else
            {
                TempData["EditResult"] = $"{Author.FirstName} {Author.LastName} Added";
                logRepository.AddAuthor(Author);
            }

            logRepository.Commit();
            return RedirectToPage("./List");
        }
    }
}