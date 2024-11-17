using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class BookRepository
    {
        private readonly AppContext _context;

        public BookRepository(AppContext context)
        {
            _context = context;
        }

        // Получение книги по Id
        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        // Получение всех книг
        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        // Добавление новой книги
        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        // Удаление книги по Id
        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        // Обновление года выпуска книги по Id
        public void UpdateBookYear(int id, int newYear)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                book.Year = newYear;
                _context.SaveChanges();
            }
        }
        public void AssignBookToUser(int bookId, int userId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (book != null && user != null)
            {
                user.Books.Add(book);
                _context.SaveChanges();
            }
        }
        public List<Book> GetBooksByGenreAndYearRange(string genre, int startYear, int endYear)
        {
            return _context.Books
                .Where(b => b.Genre == genre && b.Year >= startYear && b.Year <= endYear)
                .ToList();
        }

        // Получение количества книг определенного автора
        public int GetBookCountByAuthor(string author)
        {
            return _context.Books.Count(b => b.Author == author);
        }

        // Получение количества книг определенного жанра
        public int GetBookCountByGenre(string genre)
        {
            return _context.Books.Count(b => b.Genre == genre);
        }

        // Проверка наличия книги с определенным автором и названием
        public bool IsBookAvailable(string author, string name)
        {
            return _context.Books.Any(b => b.Author == author && b.Name == name);
        }

        // Проверка наличия книги на руках у пользователя
        public bool IsBookWithUser(int bookId, int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return user != null && user.Books.Any(b => b.Id == bookId);
        }

        // Получение количества книг на руках у пользователя
        public int GetBookCountWithUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return user?.Books.Count ?? 0;
        }

        // Получение последней вышедшей книги
        public Book GetLatestBook()
        {
            return _context.Books.OrderByDescending(b => b.Year).FirstOrDefault();
        }

        // Получение списка всех книг, отсортированных по названию (в алфавитном порядке)
        public List<Book> GetAllBooksSortedByName()
        {
            return _context.Books.OrderBy(b => b.Name).ToList();
        }

        // Получение списка всех книг, отсортированных по году выхода (по убыванию)
        public List<Book> GetAllBooksSortedByYearDescending()
        {
            return _context.Books.OrderByDescending(b => b.Year).ToList();
        }
    }
}
