using AutoMapper;
using Domain.Models.Dtos;
using Domain.Models.Requests;
using Domain.Models.Requests.Brand;
using Domain.Models.Requests.Category;
using Domain.Models.Requests.User;
using Domain.Models.Responses.Brand;
using Domain.Models.Responses.Category;
using Domain.Models.Responses.Product;
using Domain.Models.Responses.User;
using Domain.Services.Brand.Commands;
using Domain.Services.Category.Components;
using Domain.Services.Product.Command;
using Domain.Services.User.Commands;
using Infrastructure.Models;
using Infrastructure.Models.Enums;

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
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UserDto, AuthenticateResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Product maps
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, ProductResponse>();
            CreateMap<ProductDto, GetProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : string.Empty));
            CreateMap<ProductRequest, CreateProductCommand>();
            CreateMap<ProductRequest, UpdateProductCommand>();
            CreateMap<UpdateProductCommand, ProductDto>()
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<CreateProductCommand, ProductDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ProductStatus.Active))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            #endregion

            #region Category maps
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, GetCategoryResponse>();
            CreateMap<CategoryRequest, CreateCategoryCommand>();
            CreateMap<UpdateCategoryCommand, CategoryDto>();
            CreateMap<CreateCategoryCommand, CategoryDto>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

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
