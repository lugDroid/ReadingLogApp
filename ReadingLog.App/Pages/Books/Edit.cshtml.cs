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
        private readonly IReadingLogRepository logRepository;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Book Book { get; set; }
        [BindProperty]
        public int SelectedAuthorId { get; set; }
        [BindProperty]
        public Status SelectedBookStatus { get; set; }

        public IEnumerable<SelectListItem> Authors { get; set; }
        public IEnumerable<SelectListItem> BookStatus { get; set; }
        
        public EditModel(IReadingLogRepository logRepository, IHtmlHelper htmlHelper)
        {
            this.logRepository = logRepository;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? bookId)
        {
            Authors = logRepository.GetAllAuthors()
                .Select(a => new SelectListItem {
                    Value = a.Id.ToString(),
                    Text = $"{a.FirstName} {a.LastName}"
                })
                .ToList();
            
            BookStatus = htmlHelper.GetEnumSelectList<Status>();
            
            if (bookId.HasValue)
            {
                Book = logRepository.GetBookById(bookId.Value);
            }
            else
            {
                Book = new Book();
            }

            if (Book == null)
            {
                return RedirectToPage("./NotFound");
            }

            SelectedAuthorId = Book.AuthorId;
            SelectedBookStatus = Book.Status;

            return Page();
        }

        public IActionResult OnPost()
        {
            Book.AuthorId = SelectedAuthorId;
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
                logRepository.UpdateBook(Book);
            }
            else
            {
                TempData["EditResult"] = $"New book {Book.Title} successfully added";
                logRepository.AddBook(Book);
            }

            logRepository.Commit();
            return RedirectToPage("./List");
        }
    }
}