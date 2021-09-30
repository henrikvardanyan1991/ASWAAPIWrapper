using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASWAWrapper.Models
{
    public class PayRequestModel
    {
        [JsonProperty("orderId")]
        public string OrderID { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
    }
}
