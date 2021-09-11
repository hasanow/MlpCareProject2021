using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.CrossCuttingConcerns.Logging.Log4Net
{
    public class SerializableLogEvent
    {
        LoggingEvent loggingEvent;

        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            this.loggingEvent = loggingEvent;
        }

        public object Message => loggingEvent.MessageObject;
    }
}
