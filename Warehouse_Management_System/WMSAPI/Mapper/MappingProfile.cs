using AutoMapper;
using BusinessObjects;
using WarehouseMSAPI.DTO;

namespace WarehouseMSAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // map with role and map with roledto again 
            //CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Role, RoleResponseDTO>();
            CreateMap<RoleRequestDTO, Role>();

        }
    }
}
