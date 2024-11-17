using Library.Repository;

namespace Library
{
    public class Program
    {
        static void Main(string[] args)
        {

            using (var context = new AppContext())
            {
                var userRepository = new UserRepository(context);
                var bookRepository = new BookRepository(context);

                // Создаем пользователей
                var user1 = new User { Name = "Alice", Email = "alice@example.com" };
                var user2 = new User { Name = "Bob", Email = "bob@example.com" };

                // Создаем книги
                var book1 = new Book { Name = "The Witcher", Year = 1990, Author = "Andrzej Sapkowski", Genre = "Fantasy" };
                var book2 = new Book { Name = "Roadside Picnic", Year = 1972, Author = "Strugatsky Brothers", Genre = "Sci-Fi" };

                // Добавляем в БД
                userRepository.AddUser(user1);
                userRepository.AddUser(user2);

                bookRepository.AddBook(book1);
                bookRepository.AddBook(book2);

                // Привязываем книгу к пользователю
                bookRepository.AssignBookToUser(book1.Id, user1.Id);
                bookRepository.AssignBookToUser(book2.Id, user2.Id);

                // Проверяем, какие книги есть у пользователя
                var users = userRepository.GetAllUsers();
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Name} has the following books:");
                    foreach (var book in user.Books)
                    {
                        Console.WriteLine($"- {book.Name} by {book.Author} ({book.Genre})");
                    }
                }
                // Получение книг определенного жанра и года
                var fantasyBooks = bookRepository.GetBooksByGenreAndYearRange("Fantasy", 1990, 2020);
                Console.WriteLine("Fantasy books between 1990 and 2020:");
                foreach (var book in fantasyBooks)
                {
                    Console.WriteLine($"- {book.Name} by {book.Author} ({book.Year})");
                }

                // Количество книг определенного автора
                int authorBookCount = bookRepository.GetBookCountByAuthor("Andrzej Sapkowski");
                Console.WriteLine($"Books by Andrzej Sapkowski: {authorBookCount}");

                // Количество книг определенного жанра
                int genreBookCount = bookRepository.GetBookCountByGenre("Sci-Fi");
                Console.WriteLine($"Sci-Fi books: {genreBookCount}");

                // Проверка наличия книги с определенным автором и названием
                bool isBookAvailable = bookRepository.IsBookAvailable("Andrzej Sapkowski", "The Witcher");
                Console.WriteLine($"Is 'The Witcher' by Andrzej Sapkowski available? {isBookAvailable}");

                // Проверка, есть ли книга на руках у пользователя
                bool isBookWithUser = bookRepository.IsBookWithUser(1, 1); // Пример с bookId = 1, userId = 1
                Console.WriteLine($"Is book with ID 1 with user ID 1? {isBookWithUser}");

                // Количество книг на руках у пользователя
                int booksWithUser = bookRepository.GetBookCountWithUser(1); // Пример с userId = 1
                Console.WriteLine($"Books with user ID 1: {booksWithUser}");

                // Получение последней вышедшей книги
                var latestBook = bookRepository.GetLatestBook();
                if (latestBook != null)
                {
                    Console.WriteLine($"Latest book: {latestBook.Name} by {latestBook.Author} ({latestBook.Year})");
                }

                // Список книг, отсортированных по названию
                var booksSortedByName = bookRepository.GetAllBooksSortedByName();
                Console.WriteLine("Books sorted by name:");
                foreach (var book in booksSortedByName)
                {
                    Console.WriteLine($"- {book.Name}");
                }

                // Список книг, отсортированных по году выхода
                var booksSortedByYear = bookRepository.GetAllBooksSortedByYearDescending();
                Console.WriteLine("Books sorted by year (descending):");
                foreach (var book in booksSortedByYear)
                {
                    Console.WriteLine($"- {book.Name} ({book.Year})");
                }
            }
        }
    }
}