using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Core;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly IAuthorRepository authorRepository;

        [BindProperty]
        public Author Author { get; set; }

        public EditModel(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public IActionResult OnGet(int? authorId)
        {
            if (authorId.HasValue)
            {
                Author = authorRepository.GetAuthorById(authorId.Value);
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
                TempData["EditResult"] = $"Author {Author.FirstName} {Author.LastName} was updated";
                authorRepository.UpdateAuthor(Author);
            }
            else
            {
                TempData["EditResult"] = $"New author {Author.FirstName} {Author.LastName} successfully added";
                authorRepository.AddAuthor(Author);
            }

            authorRepository.Commit();
            return RedirectToPage("./List");
        }
    }
}