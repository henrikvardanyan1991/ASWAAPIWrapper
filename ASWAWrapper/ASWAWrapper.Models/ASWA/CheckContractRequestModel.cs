using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ASWAWrapper.Models
{
    public class CheckContractRequestModel
    {
        private string _useTypeId;
        [JsonProperty("useTypeId")]
        public string UseTypeID
        {
            get
            {
                return _useTypeId;
            }
            set

            {
                _useTypeId = $"{value}";
            }
        }

        private string _phone;
        [JsonProperty("phone")]
        public string Phone
        {
            get
            {
                return _phone;
            }
            set

            {
                _phone = $"{value}";
            }
        }

        private string _email;

        [JsonProperty("email")]
        public string Email
        {
            get
            {
                return _email;
            }
            set

            {
                _email = $"{value}";
            }
        }

        [JsonProperty("endDate")]
        public long EndDate { get; set; }
        

        [JsonProperty("startDate")]
        public long StartDate { get; set; }
        

        private string _bankAccountNumber;
        [JsonProperty("bankAccountNumber")]
        public string BankAccountNumber
        {
            get
            {
                return _bankAccountNumber;
            }
            set

            {
                _bankAccountNumber = $"{value}";
            }
        }

        private string _bankId;
        [JsonProperty("bankId")]
        public string BankID
        {
            get
            {
                return _bankId;
            }
            set

            {
                _bankId = $"{value}";
            }
        }

        [IgnoreDataMember]
        public string Token { get; set; }

    }
}
