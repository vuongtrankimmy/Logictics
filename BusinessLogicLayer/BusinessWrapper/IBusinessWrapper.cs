using BusinessLogicLayer.Owners.Logictics.Customers.Customer;
using BusinessLogicLayer.Owners.Logictics.Products.Product;
using BusinessLogicLayer.Owners.Logictics.Products.ProductAllocationRequired;
using BusinessLogicLayer.Owners.Logictics.Staffs.Authentication.Signin;
using BusinessLogicLayer.Owners.Logictics.Staffs.Staff;
using BusinessLogicLayer.Owners.Logictics.Staffs.StaffGroup;
using BusinessLogicLayer.Owners.Logictics.Systems.Navigations.WebHandler;
using BusinessLogicLayer.Owners.Logictics.Systems.Navigations.WebPage;

namespace BusinessLogicLayer.BusinessWrapper
{
    public interface IBusinessWrapper
    {
        #region Customers
        ICustomerBal CustomerBal { get; }
        #endregion

        IStaffBal StaffBal { get; }
        IStaffGroupBal StaffGroupBal { get; }
        ISigninAuthenticationBAL SigninAuthenticationBAL { get; }
        INavigationWebHandlerBAL NavigationWebHandlerBAL { get; }
        INavigationWebPageBAL NavigationWebPageBAL { get; }
        IProductBal ProductBal { get; }
        IProductAllocationRequiredBal ProductAllocationRequiredBal { get; }
    }
}
