using System;

namespace Babysitter.Kata.Tests
{
    class Test
    {
        public bool
        Execute()
        {
            var babysitter = new Babysitter.Kata.Core.Babysitter() { StartTime = this.StartTime, EndTime = this.EndTime, BedTime = this.BedTime };
            if (babysitter != null) {
                var result = babysitter.CalculateRate();
                if (result == this.ExpectedValue) {
                    return true;
                }
            }
            return false;
        }

        public DateTime StartTime     { get; set; }
        public DateTime EndTime       { get; set; }
        public DateTime BedTime       { get; set; }
        public int      ExpectedValue { get; set; }
    }
}