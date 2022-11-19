using AutoMapper;
using Dtos;
using Entities.Models;

namespace api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerForCreationDto, Owner>();
            CreateMap<OwnerForUpdateDto, Owner>();

            CreateMap<Account, AccountDto>();
        }
    }
}
