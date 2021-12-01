using System.Collections.Generic;

namespace ReadingLog.Data
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAllAuthors();
        IEnumerable<Author> GetAuthorsByName(string name);
        Author GetAuthorById(int id);
        Author UpdateAuthor(Author updatedAuthor);
        Author AddAuthor(Author newAuthor);
        Author DeleteAuthor(int authorId);
        int Commit();
    }
}
