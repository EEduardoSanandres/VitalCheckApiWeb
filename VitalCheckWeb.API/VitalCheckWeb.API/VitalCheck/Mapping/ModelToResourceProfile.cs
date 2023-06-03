using AutoMapper;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Client, ClientResource>();
        CreateMap<Dispatch, DispatchResource>();
        CreateMap<Inventory, InventoryResource>();
        CreateMap<Medicine, MedicineResource>();
        CreateMap<MedicineType, MedicineTypeResource>();
        CreateMap<Sale, SaleResource>();
        CreateMap<User, UserResource>();
        CreateMap<UserPlan, UserPlanResource>();
        CreateMap<UserType, UserTypeResource>();
    }

}