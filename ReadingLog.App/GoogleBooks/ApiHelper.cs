using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using ReadingLog.Data;

namespace ReadingLog.App
{
    public static class ApiHelper
    {
        public static async Task<List<VolumeInfo>> GetAllBooksAsync(Author Author, string apiKey)
        {
            var builder = new UriBuilder("https://www.googleapis.com/books/v1/volumes?");

            var query = HttpUtility.ParseQueryString(String.Empty);
            query["q"] = $"inauthor:\"{Author.FirstName}+{Author.LastName}\"";
            query["key"] = apiKey;
            query["langRestrict"] = "en";
            query["maxResults"] = "40";

            builder.Query = query.ToString();
            string url = builder.ToString();

            Console.WriteLine(url);

            var client = new HttpClient();
            var response = await client.GetAsync(url);

            var books = new List<VolumeInfo>();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResult>(json, options);

                Console.WriteLine($"{result.TotalItems} books found for {Author.FirstName} {Author.LastName}");
                
                foreach (var item in result.Items)
                {
                    if (item.VolumeInfo.Authors != null)
                    {
                      books.Add(item.VolumeInfo);
                    }
                } 
            }

            return books.OrderBy(b => b.Title).ToList();
        }
    }
}