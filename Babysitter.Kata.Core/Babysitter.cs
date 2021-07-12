using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Babysitter.Kata.Core
{
    // NOTE: The following assumptions have been made to supplement the given requirements
    //           * Bedtime is always before midnight, otherwise error
    //           * Don't deal with the headache of babysitting when daylight savings begins or ends

    // NOTE: The biggest unanswered question is how to deal with an hour of mixed rates.
    //       For example, working from 10-11pm and bedtime is 10:30pm. Does the babysitter
    //       charge the whole hour at the higher rate of $12/hour or split it so that the
    //       first half-hour is at $12/hour and the second at the reduced rate of $8/hour?
    //       I have chosen to resolve this by interperting the requirement of
    //           - gets paid for full hours (no fractional hours)
    //       in the following way (to maximize the babysitter's take home pay)
    //           * the start time will be rounded down to the nearest hour
    //           * the end time will be rounded up to the nearest hour
    //           * the bedtime will be rounded up to the nearest hour
    //       Note that this could substantially increase the gross income of the babysitter.
    //       If the babysitter started at 7:30pm and left at 10:30pm, that is a span of 
    //       3 hours, but the babysitter will charge for 4 hours.
    //       
    //       These business rules can be easily changed when further clarification on that 
    //       requirement is made.
    public class Babysitter
    {
        public int
        CalculateRate()
        {
            // sanity checks
            var minStart   = new DateTime(DateTime.Today           .Year, DateTime.Today           .Month, DateTime.Today           .Day, 17, 0, 0);
            var maxEnd     = new DateTime(DateTime.Today.AddDays(1).Year, DateTime.Today.AddDays(1).Month, DateTime.Today.AddDays(1).Day, 04, 0, 0);
            var midnight   = DateTime.Today.AddDays(1); // NOTE: "Today" is midnight
            var maxBedtime = midnight;

            if (this.StartTime < minStart    ) { return ERROR_START_TOO_EARLY;  }
            if (this.EndTime   > maxEnd      ) { return ERROR_END_TOO_LATE;     }
            if (this.BedTime   > maxBedtime  ) { return ERROR_BEDTIME_TOO_LATE; }
            if (this.StartTime > this.EndTime) { return ERROR_START_AFTER_END;  }


            // round down start time to nearest hour
            var startTime = this.StartTime.AddMinutes(00-this.StartTime.Minute).AddSeconds(00-this.StartTime.Second);

            // round up end time to nearest hour
            var endTime = this.EndTime;
            if (endTime.Minute != 0) { endTime = endTime.AddMinutes(60 - endTime.Minute); }
            if (endTime.Second != 0) { endTime = endTime.AddSeconds(60 - endTime.Second); }

            // round up end time to nearest hour
            var bedTime = this.BedTime;
            if (bedTime.Minute != 0) { bedTime = bedTime.AddMinutes(60 - bedTime.Minute); }
            if (bedTime.Second != 0) { bedTime = bedTime.AddSeconds(60 - bedTime.Second); }
            if (bedTime < startTime && startTime < midnight) { bedTime = startTime; } // don't calculate any time before actual start time

            var phase1Hours = 0; // start time to bedtime
            var phase2Hours = 0; // bedtime to midnight
            var phase3Hours = 0; // midnight to end of job

            if (bedTime >= endTime) { // leave before kids go to bed
                phase1Hours = (endTime - startTime).Hours;
                phase2Hours = 0;
                phase3Hours = 0;
            }

            if (bedTime < endTime) { // stay after kids go to bed
                phase1Hours = (bedTime - startTime).Hours;
            }
            if (bedTime < endTime && endTime <= midnight) { // stay after kids go to bed, but leave by midnight
                phase2Hours = (endTime - bedTime  ).Hours;
            }
            if (bedTime < endTime && endTime >  midnight) { // stay after kids go to bed, and stay after midnight
                phase2Hours = (midnight - bedTime ).Hours;
                phase3Hours = (endTime  - midnight).Hours;
            }

            if (phase1Hours > 0 || phase2Hours > 0 || phase3Hours > 0) {
                return (phase1Hours * 12) + (phase2Hours * 8) + (phase3Hours * 16);
            }

            return -1;
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime   { get; set; }
        public DateTime BedTime   { get; set; }

        public const int ERROR_START_TOO_EARLY  = -101;
        public const int ERROR_END_TOO_LATE     = -102;
        public const int ERROR_START_AFTER_END  = -103;
        public const int ERROR_BEDTIME_TOO_LATE = -104;
    }
}