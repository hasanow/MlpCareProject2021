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
    public class CacheAspect:MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration=60)
        {
            this._duration = duration;
            this._cacheManager = ServiceTool.GetService<ICacheManager>();
        }
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {

                Type returnType = invocation.Method.ReturnType;
                var value=_cacheManager.Get(key);
                    if (value == null)
                        value=_cacheManager.Get(key);
                invocation.ReturnValue = value;
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
