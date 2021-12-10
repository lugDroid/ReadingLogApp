using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
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
        public async Task OnGetAsync(int authorId)
        {
            Author = authorRepository.GetAuthorById(authorId);

            var builder = new UriBuilder("https://www.googleapis.com/books/v1/volumes?");

            var query = HttpUtility.ParseQueryString(String.Empty);
            query["q"] = $"inauthor:\"{Author.FirstName}+{Author.LastName}\"";
            query["key"] = config["GoogleBooksApiKey"];
            query["langRestrict"] = "en";
            query["maxResults"] = "40";

            builder.Query = query.ToString();
            string url = builder.ToString();

            Console.WriteLine(url);

            var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GoogleBooksResult>(json, options);
                Console.WriteLine($"Total books found: {result.TotalItems}");
                //Console.WriteLine(result.Items[0].VolumeInfo.Title);
                foreach(var item in result.Items)
                {
                    Console.Write($"{item.VolumeInfo.Title} - ");
                    foreach(var author in item.VolumeInfo.Authors)
                    {
                        Console.Write($"{author}, ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}