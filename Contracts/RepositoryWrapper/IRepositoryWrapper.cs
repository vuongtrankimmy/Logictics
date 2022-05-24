using Contracts.Owners.Logictics.Customers.Customer;
using Contracts.Owners.Logictics.Orders.Order;
using Contracts.Owners.Logictics.Products.Product;
using Contracts.Owners.Logictics.Products.ProductAllocationRequired;
using Contracts.Owners.Logictics.Products.ProductGroup;
using Contracts.Owners.Logictics.Roles.Role;
using Contracts.Owners.Logictics.Staffs.Staff;
using Contracts.Owners.Logictics.Staffs.StaffGroup;
using Contracts.Owners.Logictics.Staffs.StaffPermission;
using Contracts.Owners.Logictics.Systems.CodesTypes.CodesType;
using Contracts.Owners.Logictics.Systems.CodesTypes.CodesTypeGroup;
using Contracts.Owners.Logictics.Systems.Departments.Department;
using Contracts.Owners.Logictics.Systems.Navigations.WebHandler;
using Contracts.Owners.Logictics.Systems.Navigations.WebPage;
using Contracts.Owners.Logictics.Systems.Positions.Position;
using Contracts.Owners.Logictics.Systems.Products.ProductType;
using Contracts.Owners.Logictics.Systems.Staffs.StaffStatus;
using Contracts.Owners.Logictics.Systems.Units.Unit;

namespace Contracts.RepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        IRoleRepository RoleRepository { get; }
        IStaffRepository StaffRepository { get; }
        IStaffGroupRepository StaffGroupRepository { get; }
        IStaffPermissionRepository StaffPermissionRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductAllocationRequiredRepository ProductAllocationRequiredRepository { get; }
        IProductTypeRepository ProductTypeRepository { get; }
        IProductGroupRepository ProductGroupRepository { get; }
        IUnitRepository UnitRepository { get; }
        INavigationWebHandlerRepository NavigationWebHandlerRepository { get; }
        INavigationWebPageRepository NavigationWebPageRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IPositionRepository PositionRepository { get; }
        IStaffStatusRepository StaffStatusRepository { get; }
        ICodesTypeRepository CodesTypeRepository { get; }
        ICodesTypeGroupRepository CodesTypeGroupRepository { get; }

    }
}
