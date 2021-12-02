using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sonar_Sweep
{
    public class DepthMeasurer
    {
        public static int CountDepthIncrements(int[] report, int slidingWindow)
        {
            int countDepthIncrements = 0;
            for(int reportIndex = slidingWindow; reportIndex < report.Length; reportIndex++)
            {
                var startIndex = reportIndex - slidingWindow;
                var endIndex = reportIndex;

                var firstMeasurement = report[startIndex..endIndex].Sum();
                var secondMeasurement = report[(startIndex + 1)..(endIndex + 1)].Sum();

                if (secondMeasurement > firstMeasurement)
                    countDepthIncrements++;
            }
            return countDepthIncrements;
        }
    }
}
