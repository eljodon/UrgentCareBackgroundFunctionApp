using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrgentCareCommon.Models
{
    public class Hospital
    {
        private Waits _waits;
        private int _currentWaitHigh;
        private int _currentWaitLow;

        [JsonProperty(PropertyName = "hospital_id")]
        public int HospitalId { get; set; }

        [JsonProperty(PropertyName = "waits")]
        public Waits Waits {
            get { return _waits; }
            set {
                _waits = value;
                if (_waits.CurrentWait.Equals("N/A"))
                {
                    _currentWaitHigh = -1;
                    _currentWaitLow = -1;
                }
                else
                {
                    string[] result = Waits.CurrentWait.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    _currentWaitLow = Int32.Parse(result[0]);
                    _currentWaitHigh = Int32.Parse(result[2]);
                }
            }
        }

        public int CurrentWaitRangeLow {
            get
            {
                return _currentWaitLow;
            }
        }

        public int CurrentWaitRangeHigh
        {
            get
            {
                return _currentWaitHigh;
            }
        }
    }
}
