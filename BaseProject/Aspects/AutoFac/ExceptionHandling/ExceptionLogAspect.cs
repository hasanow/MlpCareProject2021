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

namespace BaseProject.Aspects.AutoFac.ExceptionHandling
{
    public class ExceptionLogAspect:MethodInterception
    {
        private LoggerServiceBase loggerService;

        public ExceptionLogAspect(Type logger)
        {
            if (logger.BaseType != typeof(LoggerServiceBase))
                throw new Exception(AspectMessages.WrongValidationType);

            this.loggerService = (LoggerServiceBase)Activator.CreateInstance(logger);
        }

        protected override void OnException(IInvocation invocation,Exception e)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation) ;
            logDetailWithException.ExceptionMessage = e.Message;
            loggerService.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name=invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value=invocation.Arguments[i],
                    Type=invocation.Arguments[i].GetType().Name                    
                });
            }
            var logDetailWithException = new LogDetailWithException()
            {
                MethodName=invocation.Method.Name,
                LogParameters=logParameters
            } ;
            return logDetailWithException;
        }
    }
}
