using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASWAWrapper.Models
{
    public class GetInsuranceCompanyResponseModel
    {
       
            [JsonProperty("premium")]
            public decimal Amount { get; set; }
            [JsonProperty("icId")]
            public int ID { get; set; }
        
    }
}
