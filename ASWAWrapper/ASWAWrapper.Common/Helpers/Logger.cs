using ASWAWrapper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace ASWAWrapper.Common.Helpers
{
    public class Logger
    {
        private IHostEnvironment _hostEnvironment;
        private IHttpContextAccessor _httpContextAccessor;

        public Logger(IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Log(LogType logType, string message = "", Exception exception = null)
        {
            try
            {
                string _logFolderName = logType.ToString().ToUpper();
                string _logFileName = string.Join("_", logType.ToString(), DateTime.Now.ToLongDateString(), DateTime.Now.Ticks, ".txt");
                string _fullFolderName = Path.Combine(_hostEnvironment.ContentRootPath, _logFolderName).Replace("\\", "/");
                string _fullFileName = Path.Combine(_fullFolderName, _logFileName).Replace("\\", "/");

                switch (logType)
                {
                    case LogType.Error:

                        if (exception != null)
                        {
                            Exception ex = exception;
                            message += ex.Message + "\n\nSatck Trace" + ex.StackTrace + "\n\nSource" + ex.Source;
                            message += "\nError occured on : " + _httpContextAccessor.HttpContext ?? $"{ _httpContextAccessor.HttpContext.Request.Path}";

                            message += "\n\nHost: " + _httpContextAccessor.HttpContext ?? $"{_httpContextAccessor.HttpContext.Request.Host.Host}";
                            message += "\n\nRemote IP:" + _httpContextAccessor.HttpContext ?? $"{_httpContextAccessor.HttpContext.Connection.RemoteIpAddress}";

                            if (ex.InnerException != null)
                            {
                                message += "\n\nSatck Trace :" + ex.InnerException.StackTrace + ex.Source;
                                message += "\n\nInnerExceptionMessage  :" + ex.InnerException.Message;
                            }
                        }

                        break;
                    case LogType.Warning:
                        break;
                    case LogType.Text:
                        break;
                    default:
                        break;
                }

                if (!Directory.Exists(_fullFolderName))
                {
                    Directory.CreateDirectory(_fullFolderName);
                }

                File.WriteAllText(_fullFileName, message);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
