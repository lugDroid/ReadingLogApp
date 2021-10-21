using System.Linq;
using System.Collections.Generic;
using ReadingLog.Core;
using System;

namespace ReadingLog.Data
{
    public class InMemoryRepository : IReadingLogRepository
    {
        private List<Author> authors;
        private List<Book> books;

        public InMemoryRepository()
        {
            authors = new List<Author>
            {
                new Author { Id = 1, FirstName = "Álvaro", LastName = "Bilbao" },
                new Author { Id = 2, FirstName = "Hervé", LastName = "Le Tellier"},
                new Author { Id = 3, FirstName = "Tania", LastName = "García"},
                new Author { Id = 4, FirstName = "Carmen", LastName = "Laforet"}
            };

            books = new List<Book>
            {
                new Book { Id = 1, Title = "El cerebro del niño explicado a los padres", AuthorId = 1, Status = Status.Reading, StartDate = new DateTime(2021,9,9)},
                new Book { Id = 2, Title = "La anomalía", AuthorId = 2, Status = Status.Finished, StartDate = new DateTime(2021,8,29), EndDate = new DateTime(2021,9,9)},
                new Book { Id = 3, Title = "Educar sin perder los nervios", AuthorId = 3, Status = Status.Abandoned, StartDate = new DateTime(2021,6,30), EndDate = new DateTime(2021,7,7)},
                new Book { Id = 4, Title = "Nada", AuthorId = 4, Status = Status.ToRead},
            };

            books.ForEach(b => {
                if (b.StartDate != DateTime.MinValue && b.EndDate != DateTime.MinValue)
                {
                    b.DaysReading = b.EndDate - b.StartDate; 
                }
                else
                {
                    b.DaysReading = TimeSpan.MinValue;
                }
            });
        }

        public Author AddAuthor(Author newAuthor)
        {
            newAuthor.Id = authors.Max(auth => auth.Id) + 1;
            authors.Add(newAuthor);

            return newAuthor;
        }

        public Book AddBook(Book newBook)
        {
            newBook.Id = books.Max(auth => auth.Id) + 1;
            books.Add(newBook);

            return newBook;
        }

        public int Commit()
        {
            return 0;
        }

        public Author DeleteAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public Book DeleteBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return from author in authors
                orderby author.LastName
                select author;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return from book in books
                orderby book.Title
                select book;
        }

        public Author GetAuthorById(int id)
        {
            return authors.SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Author> GetAuthorsByName(string name)
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(int id)
        {
            return books.SingleOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> GetBooksByName(string name)
        {
            throw new NotImplementedException();
        }

        public Author UpdateAuthor(Author updatedAuthor)
        {
            var author = authors.SingleOrDefault(auth => auth.Id == updatedAuthor.Id);

            if (author != null)
            {
                author.FirstName = updatedAuthor.FirstName;
                author.LastName = updatedAuthor.LastName;
            }

            return author;
        }

        public Book UpdateBook(Book updatedBook)
        {
            var book = books.SingleOrDefault(b => b.Id == updatedBook.Id);

            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.AuthorId = updatedBook.AuthorId;
                book.Status = updatedBook.Status;
                book.StartDate = updatedBook.StartDate;
                book.EndDate = updatedBook.EndDate;
                book.DaysReading = updatedBook.DaysReading;
            }

            return book;
        }
    }
}
