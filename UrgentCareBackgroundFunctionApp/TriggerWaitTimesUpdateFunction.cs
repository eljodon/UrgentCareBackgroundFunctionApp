using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Configuration;

namespace UrgentCareBackgroundFunctionApp
{
    public static class TriggerWaitTimesUpdateFunction
    {
        [FunctionName("TriggerWaitTimesUpdateFunction")]
        [StorageAccount("AzureWebJobsStorage")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, [Queue("%WaitTimesUpdatesQueue%")] ICollector<string> queue, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            var groupIdList = ConfigurationManager.AppSettings["GroupIdList"].Trim().Split(new char[] { ',' });

            foreach(var groupId in groupIdList)
            {
                queue.Add(groupId);
            }
        }
    }
}
