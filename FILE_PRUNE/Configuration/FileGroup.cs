using System;
using System.Collections.Generic;

namespace FILE_PRUNE.Configuration
{
    [Serializable]
    public class FileGroup
    {
        public List<SettingsConfig> SettingsConfiguration { get; set; }
    }
}
