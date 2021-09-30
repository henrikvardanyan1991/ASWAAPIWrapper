using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASWAWrapper.Models
{
    public class GetVehicleInfoPostModel
    {
        [Required(ErrorMessage = "LicensePlateNumberIsRequired")]
        public string LicensePlateNumber { get; set; }

        [Required(ErrorMessage = "DocumentNumberIsRequired")]
        public string DocumentNumber { get; set; }
    }
}
