using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UrgentCareCommon.Models;
using System.Configuration;
using UrgentCareCommon.Helpers;
using Newtonsoft.Json.Linq;
using System.Text;

namespace UrgentCareBackgroundFunctionApp
{
    public static class UpdateUrgentCareFacilityWaitTimesFunction
    {
        [FunctionName("UpdateWaitTimes")]
        [StorageAccount("AzureWebJobsStorage")]
        public static async Task QueueTrigger([QueueTrigger("%WaitTimesUpdatesQueue%")]string myQueueItem,
            IBinder inputBinder,
            [DocumentDB("%DocDBDatabase%", "%DocDBCollection%", ConnectionStringSetting = "DocDBConnectionString")] IAsyncCollector<dynamic> collector,
            TraceWriter log)
        {
            log.Info($"Queue trigger function processed a message: {myQueueItem}");

            // read the clockwise data
            var hospitals = await GetWaitsAsync(myQueueItem);

            // read the urgent care facilities
            var documentDbAttribute = new DocumentDBAttribute(ConfigurationManager.AppSettings["DocDBDatabase"], ConfigurationManager.AppSettings["DocDBCollection"])
            {
                ConnectionStringSetting = "DocDBConnectionString",
                SqlQuery = $"SELECT * FROM uc WHERE uc.hospitalId IN ({string.Join(",", hospitals.Select(h => h.HospitalId))}) ORDER BY uc._ts desc"
                // -- UCOMMENT FOR CASE USING OWN ID ---
                //SqlQuery = $"SELECT * FROM uc WHERE uc.id IN ({string.Join(",", hospitals.Select(h => $"\"{h.HospitalId}\""))}) ORDER BY uc._ts desc"
            };

            var inputDocuments = await inputBinder.BindAsync<IEnumerable<UrgentCareCenter>>(documentDbAttribute);

            // update the wait times
            foreach (var urgentCareFacility in inputDocuments)
            {
                var hospital = hospitals.SingleOrDefault(h => h.HospitalId == urgentCareFacility.HospitalId);
                // -- UCOMMENT FOR CASE USING OWN ID ---
                //var hospital = hospitals.SingleOrDefault(h => h.HospitalId == int.Parse(urgentCareFacility.Id));
                urgentCareFacility.CurrentWaitRangeHigh = hospital.CurrentWaitRangeHigh;
                urgentCareFacility.CurrentWaitRangeLow = hospital.CurrentWaitRangeLow;

                await collector.AddAsync(urgentCareFacility);
            }
        }

        private static async Task<IEnumerable<Hospital>> GetWaitsAsync(string groupId)
        {
            var requestUri = $"{ConfigurationManager.AppSettings["CWRootUri"]}/v1/waits?group_id={groupId}";

            var content = await HttpHelper.SendGetRequestAsync(requestUri, authHeaderName: "authToken", authHeaderValue: ConfigurationManager.AppSettings["CWApiKey"]);

            var responseContent = JObject.Parse(content);
            var hospitalContent = responseContent["hospitals"].ToString(Formatting.None);

            return JsonConvert.DeserializeObject<IEnumerable<Hospital>>(hospitalContent);
        }
    }
}
