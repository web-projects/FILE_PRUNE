using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace FILE_PRUNE.Helpers
{
    public static class SupportedApps
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SupportedApplications
        {
            [StringValue("IPA5.AppManager.Core")]
            AppManager,
            [StringValue("IPA5.Broker.Core")]
            Broker,
            [StringValue("IPA5.Diagnostics.CollectorService.Core")]
            CollectorService,
            [StringValue("IPA5.DataService.Core")]
            DataService,
            [StringValue("IPA5.DAL.Core")]
            DAL,
            [StringValue("IPA5.LoggingService.Core")]
            LoggingService,
            [StringValue("IPA5.DAL.Monitor.Core")]
            Monitor,
            [StringValue("IPA5.PackageDeployService.Core")]
            PackageDeployService,
            [StringValue("IPA5.PackageDeployUpdater.Core")]
            PackageDeployUpdater,
            [StringValue("IPA5.Receiver.Core")]
            Receiver,
            [StringValue("IPA5.SecurityToken.Core")]
            SecurityToken,
            [StringValue("IPA5.Servicer.Core")]
            Servicer,
            [StringValue("IPA5.TCLink.ServiceProcessor.Core")]
            ServiceProcessor
        }
    }
}
