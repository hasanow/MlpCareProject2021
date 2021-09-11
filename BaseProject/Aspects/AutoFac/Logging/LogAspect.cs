using BaseProject.CrossCuttingConcerns.Logging;
using BaseProject.CrossCuttingConcerns.Logging.Log4Net;
using BaseProject.Messages;
using BaseProject.Utilities.Interceptors;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Aspects.AutoFac.Logging
{
    public class LogAspect:MethodInterception
    {

        private LoggerServiceBase loggerService;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
                throw new Exception(AspectMessages.WrongValidationType);

            this.loggerService = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }
        protected override void OnBefore(IInvocation invocation)
        {
            loggerService.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {

            var logParameters = new List<LogParameter>();

            var maxi = invocation.Arguments.Length;
            for (int i = 0; i < maxi; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
            return logDetail;

        }


    }
}
