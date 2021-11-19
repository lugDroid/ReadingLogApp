using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReadingLog.Core;
using System.Linq;

namespace ReadingLog.Data
{
    public class SqliteRepository : IReadingLogRepository
    {
        private readonly ReadingLogDbContext db;

        public SqliteRepository(ReadingLogDbContext db)
        {
            this.db = db;
        }
        public Author AddAuthor(Author newAuthor)
        {
            db.Authors.Add(newAuthor);

            return newAuthor;
        }

        public Book AddBook(Book newBook)
        {
            db.Books.Add(newBook);

            return newBook;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Author DeleteAuthor(int authorId)
        {
            var author = GetAuthorById(authorId);

            if (author != null)
            {
                db.Authors.Remove(author);
            }
            
            foreach (var book in author.Books)
            {
                db.Books.Remove(book);
            }

            return author;
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

        public IEnumerable<Author> GetAllAuthors()
        {
            IEnumerable<Author> authors = db.Authors
                .Include(auth => auth.Books);

            return authors;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return db.Books.Select(b => b);
        }

        public Author GetAuthorById(int id)
        {
            var author = db.Authors
                .Include(auth => auth.Books)
                .FirstOrDefault(auth => auth.Id == id);

            return author;
        }

        public IEnumerable<Author> GetAuthorsByName(string name)
        {
            IEnumerable<Author> authors;

            if (string.IsNullOrEmpty(name))
            {
                authors = db.Authors.ToList();
            } 
            else
            {
                authors = db.Authors
                    .Where(auth => (
                        auth.FirstName.ToLower().Contains(name.ToLower()) || 
                        auth.LastName.ToLower().Contains(name.ToLower()) || 
                        string.IsNullOrEmpty(name)
                    ))
                    .OrderBy(auth => auth.FirstName)
                    .Include(auth => auth.Books);
            }

            return authors;
        }

        public Book GetBookById(int id)
        {
            return db.Books.Find(id);
        }

        public IEnumerable<Book> GetBooksByName(string name)
        {
            IEnumerable<Book> books;

            if (string.IsNullOrEmpty(name))
            {
                books = db.Books.ToList();
            }
            else
            {
                books = db.Books
                .Where(b => b.Title.ToLower().Contains(name.ToLower()))
                .OrderByDescending(b => b.StartDate);                
            }

            return books;
        }

        public Author UpdateAuthor(Author updatedAuthor)
        {
            var entity = db.Authors.Attach(updatedAuthor);
            entity.State = EntityState.Modified;

            return updatedAuthor;
        }

        public Book UpdateBook(Book updatedBook)
        {
            var entity = db.Books.Attach(updatedBook);
            entity.State = EntityState.Modified;

            return updatedBook;
        }
    }
}