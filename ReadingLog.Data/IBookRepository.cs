using System.Collections.Generic;

namespace ReadingLog.Data
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetBooksByName(string name);
        Book GetBookById(int id);
        Book UpdateBook(Book updatedBook);
        Book AddBook(Book newBook);
        Book DeleteBook(int bookId);
        int Commit();
    }
}
