using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASWAWrapper.Models
{
    public class GetVehicleResponseModel
    {
        [JsonProperty("vehicleMark")]
        public string VehicleMark { get; set; }

        [JsonProperty("vehicleModel")]
        public string VehicleModel { get; set; }

        [JsonProperty("horsePower")]
        public string HorsePower { get; set; }

        [JsonProperty("suggestedStartDate")]
        public long? SuggestedStartDate { get; set; }

        [JsonProperty("policyHolderId")]
        public int PolicyHolderId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        [JsonProperty("policyHolderType")]
        public string PolicyHolderType { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
