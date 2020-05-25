using Sample.Users.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sample.Users.Infrastructure
{
    public class DummyRepository : IUsersRepository
    {
        private Dictionary<int, User> _users;

        public DummyRepository()
        {
            _users = JsonSerializer
                .Deserialize<IEnumerable<User>>(Properties.Resources.Users)
                .ToDictionary(p => p.Id, p =>
                {
                    p.EMail = $"{p.FirstName}@{p.LastName}.com";
                    return p;
                });
        }

        public Task CreateAsync(User user)
        {
            var id = _users.Keys.Max() + 1;
            user.Id = id;

            _users.Add(id, user);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _users.Remove(id);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(User user)
        {
            _users[user.Id] = user;

            return Task.CompletedTask;
        }

        public User Get(int id) => _users[id];

        public IEnumerable<User> GetAll()
            => _users.Values;
    }
}
