using BusinessLogicLayer.BusinessWrapper;
using BusinessLogicLayer.Owners.Logictics.Customers.Customer;
using BusinessLogicLayer.Owners.Logictics.Products.Product;
using BusinessLogicLayer.Owners.Logictics.Products.ProductAllocationRequired;
using BusinessLogicLayer.Owners.Logictics.Staffs.Authentication.Signin;
using BusinessLogicLayer.Owners.Logictics.Staffs.Staff;
using BusinessLogicLayer.Owners.Logictics.Staffs.StaffGroup;
using BusinessLogicLayer.Owners.Logictics.Systems.Navigations.WebHandler;
using BusinessLogicLayer.Owners.Logictics.Systems.Navigations.WebPage;
using Contracts.RepositoryWrapper;
using Contracts.TokenManager;
using DataAccessLayer.Owners.Logictics.Customers.Customer;
using DataAccessLayer.Owners.Logictics.Products.Product;
using DataAccessLayer.Owners.Logictics.Products.ProductAllocationRequired;
using DataAccessLayer.Owners.Logictics.Staffs.Authentication.Signin;
using DataAccessLayer.Owners.Logictics.Staffs.Staff;
using DataAccessLayer.Owners.Logictics.Staffs.StaffGroup;
using DataAccessLayer.Owners.Logictics.Systems.Navigations.WebHandler;
using DataAccessLayer.Owners.Logictics.Systems.Navigations.WebPage;
using Entities.ExtendedModels.Localize;
using Microsoft.Extensions.Localization;

namespace DataAccessLayer.BusinessWrapper
{
    public class BusinessWrapper : IBusinessWrapper
    {
        readonly ITokenManager tokenManager;
        private readonly IRepositoryWrapper repository;
        private readonly IStringLocalizer<Resource> localizer;
        public BusinessWrapper(IRepositoryWrapper repository, IStringLocalizer<Resource> localizer, ITokenManager tokenManager)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.tokenManager = tokenManager;
        }
        #region Customers
        ICustomerBal customerBal;
        public ICustomerBal CustomerBal { get { return customerBal ??= new CustomerBal(localizer, repository); } }
        #endregion
        #region Staffs
        ISigninAuthenticationBAL signinAuthenticationBAL;
        public ISigninAuthenticationBAL SigninAuthenticationBAL { get { return signinAuthenticationBAL ??= new SigninAuthenticationDAL(localizer, repository, tokenManager); } }

        IStaffBal staffBal;
        public IStaffBal StaffBal { get { return staffBal ??= new StaffDal(localizer, repository); } }

        IStaffGroupBal staffGroupBal;
        public IStaffGroupBal StaffGroupBal { get { return staffGroupBal ??= new StaffGroupDal(localizer, repository); } }
        #endregion
        #region System Navigations
        INavigationWebHandlerBAL navigationWebHandlerBAL;
        public INavigationWebHandlerBAL NavigationWebHandlerBAL
        {
            get
            {
                return navigationWebHandlerBAL ??= new NavigationWebHandlerDAL(localizer, repository);
            }
        }
        INavigationWebPageBAL navigationWebPageBAL;
        public INavigationWebPageBAL NavigationWebPageBAL
        {
            get
            {
                return navigationWebPageBAL ??= new NavigationWebPageDAL(localizer, repository);
            }
        }
        #endregion

        #region Departments + Positions

        #endregion

        #region Products
        IProductBal productBal;
        public IProductBal ProductBal
        {
            get
            {
                return productBal ??= new ProductDal(localizer, repository);
            }
        }

        IProductAllocationRequiredBal productAllocationRequiredBal;
        public IProductAllocationRequiredBal ProductAllocationRequiredBal
        {
            get
            {
                return productAllocationRequiredBal ??= new ProductAllocationRequiredDal(localizer, repository);
            }
        }
        #endregion
    }
}
