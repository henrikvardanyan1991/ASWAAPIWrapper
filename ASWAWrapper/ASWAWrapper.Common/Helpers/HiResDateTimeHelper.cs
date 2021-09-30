using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ASWAWrapper.Common.Helpers
{
   public static class HiResDateTimeHelper
    {
        private static Int64 lastTimeStamp = DateTime.UtcNow.Ticks;

        public static Int64 UtcNowTicks
        {
            get
            {
                Int64 original;
                Int64 newValue;
                do
                {
                    original = lastTimeStamp;
                    Int64 now = DateTime.UtcNow.Ticks;
                    newValue = Math.Max(now, original + 1);
                } while (Interlocked.CompareExchange(ref lastTimeStamp, newValue, original) != original);

                return newValue;
            }
        }
    }
}
