using BaseProject.CrossCuttingConcerns.Caching;
using BaseProject.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseProject.Utilities.IoC;
using Castle.DynamicProxy;

namespace BaseProject.Aspects.AutoFac.Caching
{
    public class CacheRemoveAspet:MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspet(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.GetService<ICacheManager>();

        }
        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }



    }
}
