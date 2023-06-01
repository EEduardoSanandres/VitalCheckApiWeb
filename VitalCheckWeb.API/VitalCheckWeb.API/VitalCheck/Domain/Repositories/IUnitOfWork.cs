namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}