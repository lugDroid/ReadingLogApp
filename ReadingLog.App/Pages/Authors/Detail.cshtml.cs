using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Authors
{
    public class DetailModel : PageModel
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IConfiguration config;

        public Author Author { get; set; }
        public List<VolumeInfo> PublishedBooks { get; set; } = new List<VolumeInfo>();

        public DetailModel(IAuthorRepository authorRepository, IConfiguration config)
        {
            this.authorRepository = authorRepository;
            this.config = config;
        }
        public async Task OnGetAsync(int authorId)
        {
            Author = authorRepository.GetAuthorById(authorId);
            PublishedBooks = await ApiHelper.GetAllBooksAsync(Author, config["GoogleBooksApiKey"]);
        }
    }
}