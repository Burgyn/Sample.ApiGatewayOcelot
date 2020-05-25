using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Users.Domain
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAll();

        User Get(int id);

        Task CreateAsync(User user);

        Task UpdateAsync(User user);

        Task DeleteAsync(int id);
    }
}
