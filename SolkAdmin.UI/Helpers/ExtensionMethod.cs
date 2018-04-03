using SolkAdmin.Common;
using System;

namespace SolkAdmin.UI.Helpers
{
    public static class ExtensionMethod
    {
        public static DateTime GetAdjustedTime(this DateTime Time)
        {
            double offset = ConfigurationHelper.TIME_OFFSET;
            DateTime adjustedTime = Time.AddHours(offset);
            return adjustedTime;
        }
    }
}