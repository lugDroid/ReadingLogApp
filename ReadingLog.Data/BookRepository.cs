using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ReadingLog.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly ReadingLogDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookRepository(ReadingLogDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return db.Books.Select(b => b);
        }

        public IEnumerable<Book> GetBooksByName(string name)
        {
            IEnumerable<Book> books;

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(name))
            {
                books = db.Books
                    .OrderByDescending(b => b.StartDate)
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Authors);
            }
            else
            {
                books = db.Books
                    .Where(b => b.Title.ToLower().Contains(name.ToLower()))
                    .Where(b => b.UserId == userId)
                    .OrderByDescending(b => b.StartDate)
                    .Include(b => b.Authors);
            }

            return books;
        }

        public Book GetBookById(int id)
        {
            return db.Books
                .Include(b => b.Authors)
                // .Include(b => b.User)
                .FirstOrDefault(b => b.Id == id);
        }

        public Book UpdateBook(Book updatedBook)
        {
            var book = db.Books
                .Include(b => b.Authors)
                .FirstOrDefault(b => b.Id == updatedBook.Id);

            var updatedAuthors = new List<Author>();
            foreach (var author in updatedBook.Authors)
            {
                updatedAuthors.Add(db.Authors
                    .Include(a => a.Books)
                    .FirstOrDefault(a => a.Id == author.Id));
            }

            book.Title = updatedBook.Title;
            book.Status = updatedBook.Status;
            book.StartDate = updatedBook.StartDate;
            book.EndDate = updatedBook.EndDate;
            book.Rating = updatedBook.Rating;
            book.DaysReading = updatedBook.DaysReading;
            book.Authors.Clear();
            book.Authors.AddRange(updatedAuthors);

            db.SaveChanges();

            return book;
        }

        public Book AddBook(Book newBook)
        {
            db.Books.Add(newBook);

            return newBook;
        }

        public Book DeleteBook(int bookId)
        {
            var book = GetBookById(bookId);

            if (book != null)
            {
                db.Books.Remove(book);
            }

            return book;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }
    }
}