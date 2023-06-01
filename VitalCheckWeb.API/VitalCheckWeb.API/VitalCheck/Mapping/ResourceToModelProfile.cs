using AutoMapper;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveClientResource, Client>();
        CreateMap<SaveCompanyResource, Company>();
        CreateMap<SaveDispatchResource, Dispatch>();
        CreateMap<SaveInventoryResource, Inventory>();
        CreateMap<SaveMedicineResource, Medicine>();
        CreateMap<SaveMedicineTypeResource, MedicineType>();
        CreateMap<SaveProviderResource, Provider>();
        CreateMap<SaveSaleResource, Sale>();
        CreateMap<SaveUserResource, User>();
        CreateMap<SaveUserPlanResource, UserPlan>();
    }
}