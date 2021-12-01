using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly IAuthorRepository authorRepository;

        public Author Author { get; set; }

        public DeleteModel(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public IActionResult OnGet(int authorId)
        {
            Author = authorRepository.GetAuthorById(authorId);

            if (Author == null)
           {
               return RedirectToPage("./NotFound");
           } 

           return Page();
        }

        public IActionResult OnPost(int authorId)
        {
            Author = authorRepository.DeleteAuthor(authorId);
            authorRepository.Commit();

            if (Author == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["DeleteResult"] = $"Author {Author.FirstName} {Author.LastName} successfully deleted";
            return RedirectToPage("./List");
        }
    }
}