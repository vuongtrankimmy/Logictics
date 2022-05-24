using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ExtendedModels.MethodErrorHandling;

namespace BusinessLogicLayer.Owners.Logictics.Systems.Navigations.WebHandler
{
    public interface INavigationWebHandlerBAL
    {
        Task<Response> Get();
    }
}
