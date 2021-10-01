using ASWAWrapper.Common.Helpers;
using ASWAWrapper.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ASWAWrapper.Common.Services
{
    public class ASWARequestService : IASWARequestService
    {
        private readonly IConfiguration _configuration;
        private readonly Logger _logger;
        private string BaseUrl
        {
            get
            {
                return _configuration["ASWAConfig:URL"];
            }
        }

        private string Token
        {
            get
            {
                return _configuration["ASWAConfig:Token"];

            }
        }

        public ASWARequestService(
            IConfiguration configuration,
            Logger logger
            )
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task CheckContractAsync(CheckContractRequestModel model)
        {
            string action = $"/webservicekiosk/contract/info/{model.Token}";
            string responseString = await PutAsync(model, action);
            if (!string.IsNullOrWhiteSpace(responseString))
            {
                JObject jObject = JObject.Parse(responseString);

                if (jObject["code"] != null)
                {
                    string message = jObject["message"].ToString();
                    ProblemReporter.ReportUnprocessableEntity(message);
                }
            }
        }

        public async Task<Double> GetBonusMalusAsync(Int32 policyHolderId)
        {
            string action = $"/webservicekiosk/bonusmalus/{policyHolderId}";
            string responseString = await GetAsync(action);
            if (responseString.StartsWith("{") && responseString.EndsWith("}")
           || responseString.StartsWith("[") && responseString.EndsWith("]"))
            {
                JObject jObject = JObject.Parse(responseString);
                if (jObject["code"] != null)
                {
                    string message = jObject["message"].ToString();
                    ProblemReporter.ReportUnprocessableEntity(message);
                }
            }

            return double.Parse(responseString);
        }

        public async Task<List<GetInsuranceCompanyResponseModel>> GetInsuranceCompaniesAsync(String token)
        {
            List<GetInsuranceCompanyResponseModel> response = null;
            string action = $"/webservicekiosk/insuranceCompany/premium/{token}";

            string responseString = await GetAsync(action);

            if (!string.IsNullOrWhiteSpace(responseString))
            {
                var jtoken = JToken.Parse(responseString);

                if (jtoken is JArray)
                {
                    response = JsonConvert.DeserializeObject<List<GetInsuranceCompanyResponseModel>>(responseString);
                }
                else if (jtoken is JObject)
                {
                    JObject jObject = JObject.Parse(responseString);
                    if (jObject["code"] != null)
                    {
                        string message = jObject["message"].ToString();
                        ProblemReporter.ReportUnprocessableEntity(message);
                    }
                }

            }

            return response;
        }

        public async Task<GetVehicleResponseModel> GetVehicleAsync(GetVehicleRequestModel model)
        {
            GetVehicleResponseModel response = null;
            string action = $"/webservicekiosk/contract/draft?plateNumber={model.LicensePlateNumber }&idNumber={ model.DocumentNumber}";
            string responseString = await PostAsync(null, action);
            JObject jObject = JObject.Parse(responseString);
            if (jObject["code"] != null)
            {
                string message = jObject["message"].ToString();
                ProblemReporter.ReportUnprocessableEntity(message);
            }
            response = JsonConvert.DeserializeObject<GetVehicleResponseModel>(responseString);
            return response;
        }

        public async Task<String> PrepayAsync(String token, int companyID)
        {
            string action = $"/webservicekiosk/contract/prepare/{token}?icId={companyID}&isTcAccepted=true";
            string response = await PutAsync(null, action);
            return response;

        }



        public async Task PayAsync(PayRequestModel model)
        {
            string action = $"/webservicekiosk/payment/{model.Token}";
            await PostAsync(model, action);
        }

        public async Task<Boolean> CheckPayAsync(String token)
        {
            string action = $"/webservicekiosk/payment/{token}";
            string responseString = await GetAsync(action);
            if (responseString.StartsWith("{") && responseString.EndsWith("}")
           || responseString.StartsWith("[") && responseString.EndsWith("]"))
            {
                JObject jObject = JObject.Parse(responseString);
                if (jObject["code"] != null)
                {
                    string message = jObject["message"].ToString();
                    ProblemReporter.ReportUnprocessableEntity(message);
                }
            }

            return bool.Parse(responseString);
        }

        private async Task<string> GetAsync(string action)
        {
            string responseString = string.Empty;

            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate,
                  chain, sslPolicyErrors) =>
                    { return true; };
                    handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                    using (HttpClient httpClient = new HttpClient(handler))
                    {
                        httpClient.DefaultRequestHeaders.Add("X-Token", Token);
                        httpClient.Timeout = TimeSpan.FromSeconds(50000);
                        string url = $"{BaseUrl}{action}";
                        var response = await httpClient.GetAsync(url);

                        if (!response.IsSuccessStatusCode)
                        {
                            responseString = await response.Content.ReadAsStringAsync();
                            _logger.Log(LogType.Error, responseString);
                            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                            {
                                ProblemReporter.ReportInternalServerError(responseString);
                            }
                        }
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                }

            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                _logger.Log(LogType.Error, exception: ex);
                ProblemReporter.ReportServiceUnavailable(ex.Message);
            }

            return responseString;
        }

        private async Task<string> PostAsync(object model, string action)
        {
            string responseString = string.Empty;

            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate,
                chain, sslPolicyErrors) =>
                    { return true; };
                    handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                    using (HttpClient httpClient = new HttpClient(handler))
                    {
                        httpClient.DefaultRequestHeaders.Add("X-Token", Token);
                        httpClient.Timeout = TimeSpan.FromSeconds(50000);
                        string requestJson = string.Empty;
                        if (model != null)
                        {
                            requestJson = JsonConvert.SerializeObject(model);
                        }
                        var httpContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync($"{BaseUrl}{action}", httpContent);
                        if (!response.IsSuccessStatusCode)
                        {
                            responseString = await response.Content.ReadAsStringAsync();
                            _logger.Log(LogType.Error, responseString);
                            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                            {
                                ProblemReporter.ReportInternalServerError(responseString);
                            }

                        }


                        responseString = await response.Content.ReadAsStringAsync();
                    }

                }

            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                _logger.Log(LogType.Error, exception: ex);
                ProblemReporter.ReportServiceUnavailable(ex.Message);
            }

            return responseString;
        }

        private async Task<string> PutAsync(object model, string action)
        {
            string responseString = string.Empty;

            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate,
                chain, sslPolicyErrors) =>
                    { return true; };
                    handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                    using (HttpClient httpClient = new HttpClient(handler))
                    {
                        httpClient.DefaultRequestHeaders.Add("X-Token", Token);
                        httpClient.Timeout = TimeSpan.FromSeconds(50000);
                        string requestJson = string.Empty;
                        if (model != null)
                        {
                            requestJson = JsonConvert.SerializeObject(model);
                        }
                        var httpContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                        var response = await httpClient.PutAsync($"{BaseUrl}{action}", httpContent);

                        if (!response.IsSuccessStatusCode)
                        {
                            responseString = await response.Content.ReadAsStringAsync();
                            _logger.Log(LogType.Error, responseString);
                            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                            {
                                ProblemReporter.ReportInternalServerError(responseString);
                            }

                            //ProblemReporter.ReportUnprocessableEntity(responseString);
                        }
                        responseString = await response.Content.ReadAsStringAsync();

                    }

                }

            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                _logger.Log(LogType.Error, exception: ex);
                ProblemReporter.ReportServiceUnavailable(ex.Message);
            }

            return responseString;
        }


    }
}
