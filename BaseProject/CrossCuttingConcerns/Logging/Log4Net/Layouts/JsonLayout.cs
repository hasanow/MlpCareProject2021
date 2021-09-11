﻿using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.CrossCuttingConcerns.Logging.Log4Net.Layouts
{
    public class JsonLayout:LayoutSkeleton
    {
        public override void ActivateOptions()
        {
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var logevent = new SerializableLogEvent(loggingEvent);
            var json = JsonConvert.SerializeObject(logevent.Message, Formatting.Indented);
            writer.WriteLine(json);
        }
    }
}
