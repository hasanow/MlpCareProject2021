using BaseProject.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using BaseProject.Utilities.Interceptors;
using BaseProject.Utilities.IoC;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Aspects.AutoFac.Performance
{
    public class PerformanceAspect:MethodInterception
    {
        private int interval;
        private Stopwatch stopWatch;

        public PerformanceAspect(int interval)
        {
            this.interval = interval;
            stopWatch = ServiceTool.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            stopWatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (stopWatch.Elapsed.TotalSeconds > interval)
            {
                new DatabaseLogger().Warn($"Performance:{invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{stopWatch.Elapsed.TotalSeconds}");
            }
            stopWatch.Reset();
        }

    }
}
