using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ExtendedModels.MethodErrorHandling;

namespace BusinessLogicLayer.Owners.Logictics.Systems.Navigations.WebPage
{
    public interface INavigationWebPageBAL
    {
        Task<Response> Get();
        Task<Response> WebsitePageRoute();
    }
}
