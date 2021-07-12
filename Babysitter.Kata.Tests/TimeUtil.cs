using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Babysitter.Kata.Tests
{
    static class TimeUtil
    {
        public static DateTime
        Today(int hour, int minute)
        {
            return TimeUtil.Today(hour, minute, 0);
        }

        public static DateTime
        Today(int hour, int minute, int second)
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hour, minute, second);
        }

        public static DateTime
        Tomorrow(int hour, int minute)
        {
            return TimeUtil.Tomorrow(hour, minute, 0);
        }

        public static DateTime
        Tomorrow(int hour, int minute, int second)
        {
            var tomorrow = DateTime.Today.AddDays(1);
            return new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, hour, minute, second);
        }
    }
}