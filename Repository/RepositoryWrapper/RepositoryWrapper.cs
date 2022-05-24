using Contracts.AQL;
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
using Contracts.RepositoryWrapper;
using Repository.Owners.Logictics.Customers.Customer;
using Repository.Owners.Logictics.Orders.Order;
using Repository.Owners.Logictics.Products.Product;
using Repository.Owners.Logictics.Products.ProductAllocationRequired;
using Repository.Owners.Logictics.Products.ProductGroup;
using Repository.Owners.Logictics.Roles.Role;
using Repository.Owners.Logictics.Staffs.Staff;
using Repository.Owners.Logictics.Staffs.StaffGroup;
using Repository.Owners.Logictics.Staffs.StaffPermission;
using Repository.Owners.Logictics.Systems.CodesTypeGroups.CodesTypeGroupGroup;
using Repository.Owners.Logictics.Systems.CodesTypes.CodesType;
using Repository.Owners.Logictics.Systems.Departments.Department;
using Repository.Owners.Logictics.Systems.Navigations.WebHandler;
using Repository.Owners.Logictics.Systems.Navigations.WebPage;
using Repository.Owners.Logictics.Systems.Positions.Position;
using Repository.Owners.Logictics.Systems.Products.ProductType;
using Repository.Owners.Logictics.Systems.Staffs.StaffStatus;
using Repository.Owners.Logictics.Systems.Units.Unit;

namespace Repository.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        private readonly IQuery _query;
        public RepositoryWrapper(IQuery query)
        {
            _query = query;
        }
        #region Customer
        ICustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository { get { return _customerRepository ??= new CustomerRepository(_query); } }
        #endregion
        #region Order
        IOrderRepository _orderRepository;
        public IOrderRepository OrderRepository { get { return _orderRepository ??= new OrderRepository(_query); } }
        #endregion
        #region Products
        IProductRepository _productRepository;
        public IProductRepository ProductRepository { get { return _productRepository ??= new ProductRepository(_query); } }
        IProductAllocationRequiredRepository _productAllocationRequiredRepository;
        public IProductAllocationRequiredRepository ProductAllocationRequiredRepository { get { return _productAllocationRequiredRepository ??= new ProductAllocationRequiredRepository(_query); } }

        IProductGroupRepository _productGroupRepository;
        public IProductGroupRepository ProductGroupRepository { get { return _productGroupRepository ??= new ProductGroupRepository(_query); } }
        #endregion
        #region Roles
        IRoleRepository _roleRepository;
        public IRoleRepository RoleRepository { get { return _roleRepository ??= new RoleRepository(_query); } }
        #endregion
        #region Staffs
        IStaffRepository _staffRepository;
        public IStaffRepository StaffRepository { get { return _staffRepository ??= new StaffRepository(_query); } }
        IStaffGroupRepository _staffGroupRepository;
        public IStaffGroupRepository StaffGroupRepository { get { return _staffGroupRepository ??= new StaffGroupRepository(_query); } }
        IStaffPermissionRepository _staffPermissionRepository;
        public IStaffPermissionRepository StaffPermissionRepository { get { return _staffPermissionRepository ??= new StaffPermissionRepository(_query); } }
        #endregion
        #region Systems
        #region Products
        IProductTypeRepository _productTypeRepository;
        public IProductTypeRepository ProductTypeRepository { get { return _productTypeRepository ??= new ProductTypeRepository(_query); } }
        #endregion        
        #region Units
        IUnitRepository _unitRepository;
        public IUnitRepository UnitRepository { get { return _unitRepository ??= new UnitRepository(_query); } }
        #endregion
        #region Navigations
        INavigationWebHandlerRepository _navigationWebHandlerRepository;
        public INavigationWebHandlerRepository NavigationWebHandlerRepository { get { return _navigationWebHandlerRepository ??= new NavigationWebHandlerRepository(_query); } }
        INavigationWebPageRepository _navigationWebPageRepository;
        public INavigationWebPageRepository NavigationWebPageRepository { get { return _navigationWebPageRepository ??= new NavigationWebPageRepository(_query); } }
        #endregion
        #region Departments
        IDepartmentRepository departmentRepository;
        public IDepartmentRepository DepartmentRepository { get { return departmentRepository ??= new DepartmentRepository(_query); } }
        #endregion
        #region Positions
        IPositionRepository positionRepository;
        public IPositionRepository PositionRepository { get { return positionRepository ??= new PositionRepository(_query); } }
        #endregion
        #region Staffs Status
        IStaffStatusRepository staffStatusRepository;
        public IStaffStatusRepository StaffStatusRepository { get { return staffStatusRepository ??= new StaffStatusRepository(_query); } }
        #endregion
        #region Code Type
        ICodesTypeRepository codesTypeRepository;
        public ICodesTypeRepository CodesTypeRepository { get { return codesTypeRepository ??= new CodesTypeRepository(_query); } }
        #endregion
        #region Code Type Group
        ICodesTypeGroupRepository codesTypeGroupRepository;
        public ICodesTypeGroupRepository CodesTypeGroupRepository { get { return codesTypeGroupRepository ??= new CodesTypeGroupRepository(_query); } }
        #endregion
        #endregion

    }
}
