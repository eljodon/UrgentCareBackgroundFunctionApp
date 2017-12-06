using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrgentCareCommon.Models
{
    public class Waits
    {
        [JsonProperty(PropertyName = "next_available_visit")]
        public string NextAvailableVisit { get; set; }

        [JsonProperty(PropertyName = "current_wait")]
        public string CurrentWait { get; set; }

        [JsonProperty(PropertyName = "queue_length")]
        public int QueueLength { get; set; }

        [JsonProperty(PropertyName = "queue_total")]
        public int QueueTotal { get; set; }
    }

}
