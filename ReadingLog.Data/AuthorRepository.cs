using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ReadingLog.Data
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ReadingLogDbContext db;

        public AuthorRepository(ReadingLogDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            IEnumerable<Author> authors = db.Authors
                .Include(auth => auth.Books);

            return authors;
        }

        public IEnumerable<Author> GetAuthorsByName(string name)
        {
            IEnumerable<Author> authors;

            if (string.IsNullOrEmpty(name))
            {
                authors = db.Authors
                    .OrderBy(auth => auth.FirstName)
                    .Include(auth => auth.Books);
            }
            else
            {
                authors = db.Authors
                    .Where(auth => (
                        auth.FirstName.ToLower().Contains(name.ToLower()) ||
                        auth.LastName.ToLower().Contains(name.ToLower())
                    ))
                    .OrderBy(auth => auth.FirstName)
                    .Include(auth => auth.Books);
            }

            return authors;
        }

        public Author GetAuthorById(int id)
        {
            var author = db.Authors
                .Include(auth => auth.Books)
                .FirstOrDefault(auth => auth.Id == id);

            return author;
        }

        public Author UpdateAuthor(Author updatedAuthor)
        {
            var entity = db.Authors.Attach(updatedAuthor);
            entity.State = EntityState.Modified;

            return updatedAuthor;
        }

        public Author AddAuthor(Author newAuthor)
        {
            db.Authors.Add(newAuthor);

            return newAuthor;
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

        public int Commit()
        {
            return db.SaveChanges();
        }
    }
}