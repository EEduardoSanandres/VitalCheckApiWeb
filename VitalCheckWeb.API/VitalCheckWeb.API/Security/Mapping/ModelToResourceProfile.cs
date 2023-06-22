using AutoMapper;
using VitalCheckWeb.API.Security.Domain.Models;
using VitalCheckWeb.API.Security.Domain.Services.Communication;
using VitalCheckWeb.API.Security.Resources;

namespace VitalCheckWeb.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
    }
}