using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface ICompanyService
{
    Task<IEnumerable<Company>> ListAsync();
    Task<IEnumerable<Company>> ListByUserIdAsync(int userId);
    Task<CompanyResponse> SaveAsync(Company company);
    Task<CompanyResponse> UpdateAsync(int companyId, Company company);
    Task<CompanyResponse> DeleteAsync(int companyId);
}