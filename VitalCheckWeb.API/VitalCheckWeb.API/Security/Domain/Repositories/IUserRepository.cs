using VitalCheckWeb.API.Security.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int userId);
    Task<User> FindByUsernameAsync(string username);
    public bool ExistsByUsername(string username);
    User FindById(int id); 
    void Update(User user);
    void Remove(User user);
}