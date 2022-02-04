using ASWAWrapper.Common.Services;
using ASWAWrapper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASWAWrapper.API.Controllers
{
    [Route("[controller]/[action]")]
    public class ASWARequestController : BaseController
    {
        private IASWARequestService _aswaRequestService;
        public ASWARequestController(IASWARequestService aswaRequestService)
        {
            _aswaRequestService = aswaRequestService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetVehicleResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehicle([FromBody] GetVehicleRequestModel model)
        {
            var response = await _aswaRequestService.GetVehicleAsync(model);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CheckContract([FromBody] CheckContractRequestModel model)
        {
            await _aswaRequestService.CheckContractAsync(model);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetInsuranceCompanies([FromQuery] string token)
        {
            var response = await _aswaRequestService.GetInsuranceCompaniesAsync(token);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetBonusMalus([FromQuery] int policyHolderId)
        {
            var response = await _aswaRequestService.GetBonusMalusAsync(policyHolderId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Prepay([FromBody] PrepayRequestModel model)
        {
            var response = await _aswaRequestService.PrepayAsync(model.Token, model.InsuranceCompanyID);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Pay([FromBody] PayRequestModel model)
        {
            await _aswaRequestService.PayAsync(model);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> CheckPay([FromQuery] string token)
        {
            var respnse = await _aswaRequestService.CheckPayAsync(token);
            return Ok(respnse);
        }

        [HttpGet]
        public async Task<IActionResult> GetContractStatus([FromQuery] string token)
        {
            string response = await _aswaRequestService.GetContractStatusAsync(token);
            return Ok(response);
        }
    }
}
