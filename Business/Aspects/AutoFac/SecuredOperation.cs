using BaseProject.Utilities;
using BaseProject.Utilities.Interceptors;
using BaseProject.Utilities.IoC;
using Business.Contants;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Aspects.AutoFac
{
    public class SecuredOperation:MethodInterception
    {
        private IHttpContextAccessor httpContextAccessor;

        public SecuredOperation()
        {
            this.httpContextAccessor = ServiceTool.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var user = httpContextAccessor.HttpContext.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                return;
            }
            throw new AuthorizationException(Messages.AutorizationDenied);
        }
    }
}
