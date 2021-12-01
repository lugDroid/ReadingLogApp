using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly IBookRepository logRepository;

        public Book Book { get; set; }

        public DeleteModel(IBookRepository logRepository)
        {
            this.logRepository = logRepository;
        }
        public IActionResult OnGet(int bookId)
        {
            Book = logRepository.GetBookById(bookId);

            if (Book == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost(int bookId)
        {
            Book = logRepository.DeleteBook(bookId);
            logRepository.Commit();

            if (Book == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["DeleteResult"] = $"Book {Book.Title} successfully deleted";
            return RedirectToPage("./List");
        }
    }
}