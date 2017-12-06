using Microsoft.Azure.Documents.Spatial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrgentCareCommon.Models
{
    public class UrgentCareCenter
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("hospitalId")]
        public int HospitalId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("location")]
        public Point Location { get; set; }
        [JsonProperty("crossStreetPrimary")]
        public string CrossStreetPrimary { get; set; }
        [JsonProperty("crossStreetSecondary")]
        public string CrossStreetSecondary { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }  //TODO -- break this up into address line 1 and line 2
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("zip")]
        public string Zip { get; set; }
        [JsonProperty("timeOpenToday")]
        public DateTime TimeOpenToday { get; set; }
        [JsonProperty("timeCloseToday")]
        public DateTime TimeCloseToday { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("currentWaitRangeLow")]
        public int CurrentWaitRangeLow { get; set; }
        [JsonProperty("currentWaitRangeHigh")]
        public int CurrentWaitRangeHigh { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("website")]
        public string Website { get; set; }
    }
}
