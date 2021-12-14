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
        public static async Task<List<VolumeInfo>> GetAllBooksAsync(Author author, string apiKey, int startIndex = 0)
        {
            var builder = new UriBuilder("https://www.googleapis.com/books/v1/volumes?");
            const int maxResults = 40;

            var query = HttpUtility.ParseQueryString(String.Empty);
            query["q"] = $"inauthor:\"{author.FirstName}+{author.LastName}\"";
            query["key"] = apiKey;
            query["langRestrict"] = "en";
            query["maxResults"] = maxResults.ToString();
            query["startIndex"] = startIndex.ToString();

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

                //Console.WriteLine($"{result.TotalItems} books found for {author.FirstName} {author.LastName}");
                
                if (result.TotalItems > 0)
                {
                    foreach (var item in result.Items)
                    {
                        if (item.VolumeInfo.Authors != null && 
                            item.SaleInfo != null &&
                            !item.SaleInfo.isEbook &&
                            item.VolumeInfo.Language == "en" &&
                            item.VolumeInfo.Authors.Contains($"{author.FirstName} {author.LastName}"))
                        {
                        books.Add(item.VolumeInfo);
                        }
                    } 

                    if ((startIndex + maxResults) < result.TotalItems)
                    {
                        books.AddRange(await GetAllBooksAsync(author, apiKey, startIndex + maxResults));
                    }
                }
            }

            return books
                .Distinct()
                .OrderBy(b => b.Title).ToList();
        }
    }
}