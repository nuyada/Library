using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class UserRepository
    {
        private readonly AppContext _context;

        public UserRepository(AppContext context)
        {
            _context = context;
        }

        // Получение пользователя по Id
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        // Получение всех пользователей
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        // Добавление нового пользователя
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // Удаление пользователя по Id
        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // Обновление имени пользователя по Id
        public void UpdateUserName(int id, string newName)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                user.Name = newName;
                _context.SaveChanges();
            }
        }
    }
}
