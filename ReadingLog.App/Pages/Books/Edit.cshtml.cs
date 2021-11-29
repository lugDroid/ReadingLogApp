using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadingLog.Core;
using ReadingLog.Data;

namespace ReadingLog.App.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty] public Book Book { get; set; }
        [BindProperty] public IEnumerable<int> SelectedAuthorsId { get; set; }
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
                SelectedAuthorsId = Book.Authors.Select(a => a.Id);
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
                return Page();
            }

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