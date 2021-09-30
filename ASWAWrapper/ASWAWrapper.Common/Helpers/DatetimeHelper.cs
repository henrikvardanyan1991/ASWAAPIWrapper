using System;
using System.Collections.Generic;
using System.Text;

namespace ASWAWrapper.Common.Helpers
{
    public class DatetimeHelper
    {
        public static long DatetimeToUnixParser(DateTime? myDate)
        {
            long unixTime = ((DateTimeOffset)myDate).ToUnixTimeSeconds();
            return unixTime;
        }

        public static DateTime UnixToDatetimeParser(long myUnixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(myUnixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
