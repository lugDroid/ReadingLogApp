using System.Collections.Generic;
using ReadingLog.Core;

namespace ReadingLog.Data
{
    public interface IReadingLogRepository
    {
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetBooksByName(string name);
        Book GetBookById(int id);
        Book UpdateBook(Book updatedBook);
        Book AddBook(Book newBook);
        Book DeleteBook(int bookId);
        
        IEnumerable<Author> GetAllAuthors();
        IEnumerable<Author> GetAuthorsByName(string name);
        Author GetAuthorById(int id);
        Author UpdateAuthor(Author updatedAuthor);
        Author AddAuthor(Author newAuthor);
        Author DeleteAuthor(int authorId);
        
        int Commit();
    }
}
