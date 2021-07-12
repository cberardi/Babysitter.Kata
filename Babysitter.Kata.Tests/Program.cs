using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Babysitter.Kata.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine();

            var tests = new Test[] {
                // ERRORS
                new Test() { StartTime = TimeUtil.Today(16, 59), EndTime = TimeUtil.Tomorrow(04, 00), BedTime = TimeUtil.Today   (20, 00), ExpectedValue = Core.Babysitter.ERROR_START_TOO_EARLY  }, // 1
                new Test() { StartTime = TimeUtil.Today(16, 00), EndTime = TimeUtil.Tomorrow(04, 00), BedTime = TimeUtil.Today   (20, 00), ExpectedValue = Core.Babysitter.ERROR_START_TOO_EARLY  }, // 2
                new Test() { StartTime = TimeUtil.Today(17, 00), EndTime = TimeUtil.Tomorrow(04, 01), BedTime = TimeUtil.Today   (20, 00), ExpectedValue = Core.Babysitter.ERROR_END_TOO_LATE     }, // 3
                new Test() { StartTime = TimeUtil.Today(17, 00), EndTime = TimeUtil.Tomorrow(05, 00), BedTime = TimeUtil.Today   (20, 00), ExpectedValue = Core.Babysitter.ERROR_END_TOO_LATE     }, // 4
                new Test() { StartTime = TimeUtil.Today(17, 00), EndTime = TimeUtil.Tomorrow(18, 00), BedTime = TimeUtil.Today   (20, 00), ExpectedValue = Core.Babysitter.ERROR_END_TOO_LATE     }, // 5
                new Test() { StartTime = TimeUtil.Today(20, 00), EndTime = TimeUtil.Today   (18, 00), BedTime = TimeUtil.Today   (20, 00), ExpectedValue = Core.Babysitter.ERROR_START_AFTER_END  }, // 6
                new Test() { StartTime = TimeUtil.Today(17, 00), EndTime = TimeUtil.Tomorrow(04, 00), BedTime = TimeUtil.Tomorrow(02, 00), ExpectedValue = Core.Babysitter.ERROR_BEDTIME_TOO_LATE }, // 7

                // VALID
                new Test() { StartTime = TimeUtil.Today   (17, 00), EndTime = TimeUtil.Tomorrow(04, 00), BedTime = TimeUtil.Today(20, 00), ExpectedValue = 132 }, // 8
                new Test() { StartTime = TimeUtil.Today   (17, 01), EndTime = TimeUtil.Tomorrow(03, 59), BedTime = TimeUtil.Today(20, 00), ExpectedValue = 132 }, // 9

                new Test() { StartTime = TimeUtil.Today   (19, 30), EndTime = TimeUtil.Today   (22, 30), BedTime = TimeUtil.Today(23, 00), ExpectedValue =  48 }, // 10
                new Test() { StartTime = TimeUtil.Today   (19, 30), EndTime = TimeUtil.Today   (22, 30), BedTime = TimeUtil.Today(21, 00), ExpectedValue =  40 }, // 11
                new Test() { StartTime = TimeUtil.Today   (19, 30), EndTime = TimeUtil.Today   (22, 30), BedTime = TimeUtil.Today(21, 30), ExpectedValue =  44 }, // 12
                new Test() { StartTime = TimeUtil.Today   (21, 30), EndTime = TimeUtil.Today   (22, 30), BedTime = TimeUtil.Today(21, 00), ExpectedValue =  16 }, // 13
                new Test() { StartTime = TimeUtil.Today   (21, 30), EndTime = TimeUtil.Today   (22, 30), BedTime = TimeUtil.Today(21, 30), ExpectedValue =  20 }, // 14

                new Test() { StartTime = TimeUtil.Today   (19, 00), EndTime = TimeUtil.Tomorrow(00, 00), BedTime = TimeUtil.Today(22, 00), ExpectedValue =  52 }, // 15
                new Test() { StartTime = TimeUtil.Today   (19, 00), EndTime = TimeUtil.Tomorrow(00, 00), BedTime = TimeUtil.Today(22, 30), ExpectedValue =  56 }, // 16

                new Test() { StartTime = TimeUtil.Tomorrow(02, 00), EndTime = TimeUtil.Tomorrow(04, 00), BedTime = TimeUtil.Today(22, 00), ExpectedValue =  32 }, // 17
                new Test() { StartTime = TimeUtil.Today   (23, 00), EndTime = TimeUtil.Tomorrow(02, 00), BedTime = TimeUtil.Today(22, 00), ExpectedValue =  40 }, // 18
                new Test() { StartTime = TimeUtil.Today   (23, 00), EndTime = TimeUtil.Tomorrow(02, 00), BedTime = TimeUtil.Today(22, 30), ExpectedValue =  40 }, // 19
                new Test() { StartTime = TimeUtil.Today   (22, 15), EndTime = TimeUtil.Tomorrow(02, 00), BedTime = TimeUtil.Today(22, 30), ExpectedValue =  52 }, // 20
                new Test() { StartTime = TimeUtil.Today   (22, 30), EndTime = TimeUtil.Tomorrow(02, 00), BedTime = TimeUtil.Today(22, 00), ExpectedValue =  48 }, // 21

                new Test() { StartTime = TimeUtil.Today   (21, 00), EndTime = TimeUtil.Today   (23, 30), BedTime = TimeUtil.Today(22, 00), ExpectedValue =  28 }, // 22
                new Test() { StartTime = TimeUtil.Today   (21, 00), EndTime = TimeUtil.Today   (23, 30), BedTime = TimeUtil.Today(22, 15), ExpectedValue =  32 }, // 23
                new Test() { StartTime = TimeUtil.Today   (21, 00), EndTime = TimeUtil.Today   (23, 30), BedTime = TimeUtil.Today(22, 30), ExpectedValue =  32 }, // 24
            };

            for (int i = 0; i < tests.Length; i++) { 
                var result = tests[i].Execute();

                Console.WriteLine("Test {0} of {1} {2}", (i + 1), tests.Length, (result ? "passed" : "*FAILED*"));
            }

            Console.WriteLine();
            Console.WriteLine("All done...");
            Console.ReadLine();
        }
    }
}