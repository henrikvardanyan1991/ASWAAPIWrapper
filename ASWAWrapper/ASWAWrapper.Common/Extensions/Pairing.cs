using System;
using System.Collections.Generic;
using System.Text;

namespace ASWAWrapper.Common.Extensions
{
    public static class Pairing
    {
        public static KeyValuePair<string, object> Of(string key, string value)
        {
            return new KeyValuePair<string, object>(key, value);
        }
    }
}
