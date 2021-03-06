﻿using System.Collections.Generic;

namespace Wirehome.Core.ServiceHost.Configuration
{
    public class ServiceConfiguration
    {
        public bool IsEnabled { get; set; } = true;

        public string Version { get; set; }

        public Dictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();
    }
}
