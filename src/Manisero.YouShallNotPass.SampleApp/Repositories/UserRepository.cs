using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Manisero.YouShallNotPass.SampleApp.Model;

namespace Manisero.YouShallNotPass.SampleApp.Repositories
{
    public interface IUserRepository
    {
        ICollection<User> GetAll();
        User Get(int userId);
        User GetByEmail(string email);
        void Create(User user);
        void Put(User user);
    }

    public class UserRepository : IUserRepository
    {
        private int _latestUserId = 0;
        private readonly IDictionary<int, User> _users = new Dictionary<int, User>();

        public ICollection<User> GetAll()
        {
            return _users.Values;
        }

        public User Get(int userId)
        {
            User result;

            return _users.TryGetValue(userId, out result)
                ? result
                : null;
        }

        public User GetByEmail(string email)
        {
            return _users.Values.SingleOrDefault(x => x.Email == email);
        }

        public void Create(User user)
        {
            var userId = Interlocked.Increment(ref _latestUserId);
            user.UserId = userId;

            _users.Add(userId, user);
        }

        public void Put(User user)
        {
            _users[user.UserId] = user;
        }
    }
}
