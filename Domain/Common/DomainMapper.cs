using AutoMapper;
using Domain.Models.Dtos;
using Domain.Models.Requests.Brand;
using Domain.Models.Requests.User;
using Domain.Models.Responses.Brand;
using Domain.Models.Responses.User;
using Domain.Services.Brand.Commands;
using Domain.Services.Brand.Commands.Handlers;
using Domain.Services.User.Commands;
using Infrastructure.Models;


namespace Domain.Common
{
    public class DomainMapper : Profile
    {
        public DomainMapper() {

            #region User maps
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<LoginRequest, LoginUserCommand>();
            CreateMap<RegisterUserRequest, RegisterAdminUserCommand>();
            CreateMap<RegisterAdminUserCommand, UserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName));
            CreateMap<UserDto, AuthenticateResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Product maps
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
          
            #endregion

            #region Category maps
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();

            #endregion


            #region Role maps
            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleDto>();
           
            #endregion

            #region Orders
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            #endregion

            #region Orders
            CreateMap<OrdersProducts, OrdersProductsDto>();
            CreateMap<OrdersProductsDto, OrdersProducts>();
            #endregion

            #region Brand maps
            CreateMap<BrandDto, Brand>();
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, GetBrandResponse>();
            CreateMap<BrandRequest, CreateBrandCommand>();
            CreateMap<UpdateBrandCommand, BrandDto>();
            CreateMap<CreateBrandCommand, BrandDto>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            #endregion

        }
    }
}
