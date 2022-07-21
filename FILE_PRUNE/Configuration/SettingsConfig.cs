using System;
using System.Collections.Generic;

namespace FILE_PRUNE.Configuration
{
    [Serializable]
    public class SettingsConfig
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public string GUID { get; set; }
        public List<string> AppsToAdd { get; set; }
    }
}
