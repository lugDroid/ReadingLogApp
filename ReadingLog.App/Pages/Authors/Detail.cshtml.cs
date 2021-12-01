using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Authors
{
    public class DetailModel : PageModel
    {
        private readonly IAuthorRepository authorRepository;

        public Author Author { get; set; }

        public DetailModel(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public void OnGet(int authorId)
        {
            Author = authorRepository.GetAuthorById(authorId);
        }
    }
}