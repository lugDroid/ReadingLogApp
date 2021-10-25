using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadingLog.Core;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Authors
{
    public class DetailModel : PageModel
    {
        private readonly IReadingLogRepository logRepository;

        public Author Author { get; set; }

        public DetailModel(IReadingLogRepository logRepository)
        {
            this.logRepository = logRepository;
        }
        public void OnGet(int authorId)
        {
            Author = logRepository.GetAuthorById(authorId);
        }
    }
}