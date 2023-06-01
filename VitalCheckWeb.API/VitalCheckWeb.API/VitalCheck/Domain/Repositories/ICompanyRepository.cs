using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> ListAsync();
    Task<IEnumerable<Company>> ListByUserIdAsync(int userId);
    Task AddAsync(Company company);
    Task<Company> FindByIdAsync(int companyId);
    void Update(Company company);
    void Remove(Company company);
}