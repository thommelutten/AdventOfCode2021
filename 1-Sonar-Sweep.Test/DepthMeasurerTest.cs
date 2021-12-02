using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sonar_Sweep.Test
{
    [TestClass]
    public class DepthMeasurerTest
    {
        [TestMethod]
        public void TestDepthMeasurerCountIncrements()
        {
            int[] report = new int[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            int timesDepthIncreased = DepthMeasurer.CountDepthIncrements(report, slidingWindow: 1);
            Assert.AreEqual(7, timesDepthIncreased);
        }

        [TestMethod]
        public void TestDepthMeasurerCountIncrementsFull()
        {
            int[] report = FileParser();
            int timesDepthIncreased = DepthMeasurer.CountDepthIncrements(report, slidingWindow: 1);
            Console.WriteLine(timesDepthIncreased);
        }

        [TestMethod]
        public void TestDepthMeasurererCountIncrementsWithSlidingWindow()
        {
            int[] report = new int[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            
            /**
             * A 399
             * B 408 - 1
             * C 418 - 2
             * D 410 - 2
             * E 407 - 2
             * F 447 - 3
             * G 509 - 4
             * H 529 - 5
             * I 523 - 5
             */
            int timesDepthIncreased =  DepthMeasurer.CountDepthIncrements(report, slidingWindow: 2);
            Assert.AreEqual(5, timesDepthIncreased);

        }

        [TestMethod]
        public void TestDepthMeasurerCountIncrementWithSlidingWindowFull()
        {
            int[] report = FileParser();
            int timesDepthIncreased = DepthMeasurer.CountDepthIncrements(report, slidingWindow: 3);
            Console.WriteLine(timesDepthIncreased);
        }

        public int[] FileParser()
        {
            string[] lines = System.IO.File.ReadAllLines("report.txt");
            int[] ints = Array.ConvertAll(lines, int.Parse);
            return ints;
        }
    }
}
