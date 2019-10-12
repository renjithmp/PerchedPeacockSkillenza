using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerchedPeacockWebApplication.Utility
{
    public class BookingUtility
    {
        public static int CalculateBookingCharge(DateTime startTime, DateTime endTime, int hourlyCharge)
        {
            TimeSpan timeSpan = endTime - startTime;
            var hourDiff = Math.Ceiling(Convert.ToDecimal(timeSpan.Hours));
            return Convert.ToInt32(hourDiff)*hourlyCharge;
        }
    }
}
