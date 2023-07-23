using AutoMapper;
using BusinessObjects;
using WarehouseMSAPI.DTO;
using WMSAPI.DTO;

namespace WarehouseMSAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // map with role and map with roledto again 
            CreateMap<RoleDTO, Role>().ReverseMap();

            CreateMap<CategoryDTO, Category>().ReverseMap();

            CreateMap<SupplierDTO, Supplier>().ReverseMap();

            CreateMap<CustomerDTO, Customer>().ReverseMap();

            CreateMap<CarrierDTO, Carrier>().ReverseMap();

            CreateMap<Location, LocationDTO>();

            CreateMap<LocationRequestDTO, Location>().ReverseMap();

            CreateMap<User, UserDTO>();

            CreateMap<UserRequestDTO, User>().ReverseMap();

            CreateMap<UserLoginDTO, User>().ReverseMap();

            CreateMap<Product, ProductDTO>();

            CreateMap<ProductRequestDTO, Product>().ReverseMap();

            CreateMap<Inventory, InventoryDTO>();

            CreateMap<InventoryRequestDTO, Inventory>().ReverseMap();

            CreateMap<Transaction, TransactionDTO>();

            CreateMap<TransactionRequestDTO, Transaction>().ReverseMap();

            CreateMap<Report, ReportDTO>();

            CreateMap<ReportRequestDTO, Report>().ReverseMap();
        }
    }
}
