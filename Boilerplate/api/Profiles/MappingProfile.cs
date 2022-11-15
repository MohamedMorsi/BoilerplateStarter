using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDto>();
            CreateMap<Account, AccountDto>();
        }
    }
}
