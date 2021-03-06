using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty] public Book Book { get; set; }
        [BindProperty] public List<int> SelectedAuthorsId { get; set; } = new List<int>();
        [BindProperty] public Status SelectedBookStatus { get; set; }

        public IEnumerable<SelectListItem> Authors { get; set; }
        public IEnumerable<SelectListItem> BookStatus { get; set; }
        
        public EditModel(IAuthorRepository authorRepository, IBookRepository bookRepository, IHtmlHelper htmlHelper)
        {
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? bookId)
        {
            Authors = authorRepository.GetAllAuthors()
                .OrderBy(a => a.FirstName)
                .Select(a => new SelectListItem {
                    Value = a.Id.ToString(),
                    Text = $"{a.FirstName} {a.LastName}"
                });
            
            BookStatus = htmlHelper.GetEnumSelectList<Status>();
            
            if (bookId.HasValue)
            {
                Book = bookRepository.GetBookById(bookId.Value);
            }
            else
            {
                Book = new Book();
            }

            if (Book == null)
            {
                return RedirectToPage("./NotFound");
            }

            if (Book.Authors.Count > 0)
            {
                Book.Authors.ForEach(auth => SelectedAuthorsId.Add(auth.Id));
            }

            SelectedBookStatus = Book.Status;

            return Page();
        }

        public IActionResult OnPost()
        {
            foreach(var id in SelectedAuthorsId)
            {
                Book.Authors.Add(authorRepository.GetAuthorById(id));
            }
            Book.Status = SelectedBookStatus;

            if (Book.EndDate != null && Book.StartDate != null)
            {
                Book.DaysReading = Book.EndDate - Book.StartDate;
            }
            else
            {
                Book.DaysReading = TimeSpan.Zero;
            }


            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model not valid");
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach(var err in errors)
                {
                    Console.WriteLine(err.ErrorMessage);
                }

                return Page();
            }

            Book.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (Book.Id > 0)
            {
                TempData["EditResult"] = $"Book {Book.Title} was updated";
                bookRepository.UpdateBook(Book);
            }
            else
            {
                TempData["EditResult"] = $"New book {Book.Title} successfully added";
                bookRepository.AddBook(Book);
            }

            authorRepository.Commit();
            bookRepository.Commit();
            return RedirectToPage("./List");
        }
    }
}