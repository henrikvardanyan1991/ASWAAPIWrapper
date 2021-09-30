using ASWAWrapper.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASWAWrapper.Common.Helpers
{
    public static class ProblemReporter
    {
        public static void ReportUnauthorizedAccess(string message = null)
        {
            throw (message == null ? (new HttpException(401, "Unauthorized: Access is denied due to invalid credentials")) : (new HttpException(401, message)));
        }

        public static void ReportForbidden(string message = null)
        {
            throw (message == null ? (new HttpException(403, "Attempted to perform an unauthorized operation")) : (new HttpException(403, message)));
        }

        public static void ReportResourseNotfound(string message = null)
        {
            throw (message == null ? (new HttpException(404, "Resource not found")) : (new HttpException(404, message)));
        }

        public static void ReportBadRequest(string message = null)
        {
            throw (message == null ? (new HttpException(400, "Invalid data")) : (new HttpException(400, message)));
        }

        public static void ReportUnprocessableEntity(string message=null)
        {
            throw (message == null ? (new HttpException(422, "Invalid data")) : (new HttpException(422, message)));
        }

        public static void ReportServiceUnavailable(string message = null)
        {
            throw (message == null ? (new HttpException(503, "Problem occured: Internal Server Error")) : (new HttpException(503, message)));
        }

        public static void ReportInternalServerError(string message = null)
        {
            throw (message == null ? (new HttpException(500, "Problem occured: Internal Server Error")) : (new HttpException(500, message)));
        }
    }
}
