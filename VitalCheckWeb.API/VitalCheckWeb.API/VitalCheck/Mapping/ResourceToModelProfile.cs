using AutoMapper;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveClientResource, Client>();
        CreateMap<SaveDispatchResource, Dispatch>();
        CreateMap<SaveInventoryResource, Inventory>();
        CreateMap<SaveMedicineResource, Medicine>();
        CreateMap<SaveMedicineTypeResource, MedicineType>();
        CreateMap<SaveSaleResource, Sale>();
        CreateMap<SaveUserPlanResource, UserPlan>();
        CreateMap<SaveUserTypeResource, UserType>();
    }
}