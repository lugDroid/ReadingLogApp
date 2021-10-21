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
            IEnumerable<Author> authors = from a in db.Authors
                                        select a;

            foreach(var a in authors)
            {
                a.Books = GetBooksByAuthorId(a.Id);
            }

            return authors;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var query = from b in db.Books
                        select b;

            return query;
        }

        public Author GetAuthorById(int id)
        {
            var author = db.Authors.Find(id);
            author.Books = GetBooksByAuthorId(author.Id);

            return author;
        }

        public IEnumerable<Author> GetAuthorsByName(string name)
        {
            IEnumerable<Author> authors = from a in db.Authors
                                        where a.FirstName.StartsWith(name) || a.LastName.StartsWith(name) || string.IsNullOrEmpty(name)
                                        orderby a.FirstName
                                        select a;
            foreach (var a in authors)
            {
                a.Books = GetBooksByAuthorId(a.Id);
            }

            return authors;
        }

        public Book GetBookById(int id)
        {
            return db.Books.Find(id);
        }

        public IEnumerable<Book> GetBooksByName(string name)
        {
            var query = from b in db.Books
                        where b.Title.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby b.StartDate descending
                        select b;

            return query;
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

        public IEnumerable<Book> GetBooksByAuthorId(int authorId)
        {
            var query = from b in db.Books
                        where b.AuthorId == authorId
                        select b;

            return query.ToList();
        }
    }
}