namespace Domain.Common.Exceptions
{
    public enum ErrorCodeEnum
    {
        [StringValue("Bad Request")]
        BadRequest = 400,
        [StringValue("Unauthorized, try login again")]
        Unauthorized = 401,
        [StringValue("Forbidden, user has no access to the request")]
        Forbidden = 403,

        #region 1000 - request required errors
        [StringValue("A valid Admin User request is required")]
        AdminUserRequestIsRequired = 1001,
        [StringValue("A valid Login User request is required")]
        LoginUserRequestIsRequired = 1002,
        [StringValue("A valid User request is required")]
        UserRequestIsRequired = 1003,
        [StringValue("A valid Role request is required")]
        RoleRequestIsRequired = 1004,
        [StringValue("A valid Get Product request is required")]
        GetProductRequestIsRequired = 1006,
        [StringValue("A valid Get Order request is required")]
        GetOrderRequestIsRequired = 1007,
        [StringValue("A valid Create Order request is required")]
        CreateOrderRequestIsRequired = 1008,
        [StringValue("A valid Category request is required")]
        CategoryRequestIsRequired = 1009,
        [StringValue("A valid Brand request is required")]
        BrandRequestIsRequired = 1010,
        #endregion

        #region 2000 - invalid errors
        [StringValue("Invalid User UserId")]
        InvalidUserId = 2001,
        [StringValue("Invalid Category UserId")]
        InvalidCategoryId = 2002,
        [StringValue("Invalid Order UserId")]
        InvalidOrderId = 2003,
        [StringValue("Invalid Product UserId")]
        InvalidProductId = 2004,
        [StringValue("Invalid Request")]
        InvalidRequest = 2005,
        [StringValue("Invalid Brand UserId")]
        InvalidBrandId = 2006,
        [StringValue("Invalid Role UserId")]
        InvalidRoleId = 2007,
        #endregion

        # region 3000 - not found errors
        [StringValue("Admin User role not found")]
        AdminUserRoleNotFound = 3001,
        [StringValue("User not found")]
        UserNotFound = 3002,
        [StringValue("User role not found")]
        UserRoleNotFound = 3003,
        [StringValue("Products not found")]
        ProductsNotFound = 3004,
        [StringValue("Category not found")]
        CategoryNotFound = 3005,
        [StringValue("Roles not found")]
        RolesNotFound = 3006,
        [StringValue("Order not found")]
        OrderNotFound = 3007,
        [StringValue("Brand not found")]
        BrandNotFound = 3008,
        #endregion

        # region 4000 - persistance errors 
        [StringValue("Could not generate token")]
        TokenNotGenerated = 4001,
        [StringValue("Could not login this user")]
        LoginFailed = 4002,
        #endregion
    }
}
