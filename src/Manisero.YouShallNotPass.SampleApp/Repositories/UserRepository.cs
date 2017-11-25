using System.Collections.Generic;
using System.Threading;
using Manisero.YouShallNotPass.SampleApp.Model;

namespace Manisero.YouShallNotPass.SampleApp.Repositories
{
    public interface IUserRepository
    {
        User Get(int userId);
        void Create(User user);
        void Put(User user);
    }

    public class UserRepository : IUserRepository
    {
        private int _latestUserId = 0;
        private readonly IDictionary<int, User> _users = new Dictionary<int, User>();

        public User Get(int userId)
        {
            User result;

            return _users.TryGetValue(userId, out result)
                ? result
                : null;
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
