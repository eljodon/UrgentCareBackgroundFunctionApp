using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrgentCareCommon.Models
{
    public class ClockwiseLocation
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "full_address")]
        public string FullAddress { get; set; }

        [JsonProperty(PropertyName = "todays_business_hours")]
        public string TodaysHours { get; set; }

        public string PhoneNumber { get; set; }

        public string CurrentWait { get; set; }

        public string Distance { get; set; }

        public string ImageUrl { get; set; }

        public string Website { get; set; }
    }
}
