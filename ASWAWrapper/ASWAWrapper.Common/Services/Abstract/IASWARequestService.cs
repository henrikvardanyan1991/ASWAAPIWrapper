using ASWAWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASWAWrapper.Common.Services
{
    public interface IASWARequestService
    {
        Task<GetVehicleResponseModel> GetVehicleAsync(GetVehicleRequestModel model);

        Task CheckContractAsync(CheckContractRequestModel model);

        Task<List<GetInsuranceCompanyResponseModel>> GetInsuranceCompaniesAsync(string token);

        Task<double> GetBonusMalusAsync(int policyHolderId);

        Task<string> PrepayAsync(string token, int companyID);

        Task<bool> CheckPayAsync(string token);

        Task PayAsync(PayRequestModel model);
    }
}
