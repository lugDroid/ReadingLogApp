using System;
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

        public DetailModel(IAuthorRepository authorRepository, IConfiguration config)
        {
            this.authorRepository = authorRepository;
            this.config = config;
        }
        public void OnGet(int authorId)
        {
            Author = authorRepository.GetAuthorById(authorId);

            var apiKey = config["GoogleBooksApiKey"];
        }
    }
}