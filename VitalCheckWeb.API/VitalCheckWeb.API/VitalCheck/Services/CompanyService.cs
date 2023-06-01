using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompanyService(ICompanyRepository companyRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Company>> ListAsync()
    {
        return await _companyRepository.ListAsync();
    }

    public async Task<IEnumerable<Company>> ListByUserIdAsync(int userId)
    { 
        return await _companyRepository.ListByUserIdAsync(userId);
    }

    public async Task<CompanyResponse> SaveAsync(Company company)
    {
        try
        {
            var existingUser = await _userRepository.FindByIdAsync(company.UserID);
            if (existingUser == null)
            {
                return new CompanyResponse("User not found.");
            }

            await _companyRepository.AddAsync(company);
            await _unitOfWork.CompleteAsync();
            return new CompanyResponse(company);
        }
        catch (Exception e)
        {
            return new CompanyResponse($"An error occurred while saving the Company: {e.Message}");
        }
    }

    public async Task<CompanyResponse> UpdateAsync(int companyId, Company company)
    {
        var existingCompany = await _companyRepository.FindByIdAsync(companyId);
        if (existingCompany == null)
        {
            return new CompanyResponse("Company not found.");
        }

        existingCompany.UserID = company.UserID;

        try
        {
            _companyRepository.Update(existingCompany);
            await _unitOfWork.CompleteAsync();
            return new CompanyResponse(existingCompany);
        }
        catch (Exception e)
        {
            return new CompanyResponse($"An error occurred while updating the Company: {e.Message}");
        }
    }

    public async Task<CompanyResponse> DeleteAsync(int companyId)
    {
        var existingCompany = await _companyRepository.FindByIdAsync(companyId);
        if (existingCompany == null)
        { 
            return new CompanyResponse("Company not found.");
        }

        try
        {
            _companyRepository.Remove(existingCompany);
            await _unitOfWork.CompleteAsync();
            return new CompanyResponse(existingCompany);
        }
        catch (Exception e)
        {
            return new CompanyResponse($"An error occurred while deleting the Company: {e.Message}");
        }
    }
}