using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _3_Binary_Diagnostics.Test
{
    [TestClass]
    public class DiagnosticReportDecoderTest
    {
        [TestMethod]
        public void TestDiagnosticReportDecoderReadFirstBitForMultipleInput()
        {
            string[] input = new string[] { "00100", "11110", "11010" };
            var diagnosticReportDecoder = new DiagnosticReportDecoder();

            string decodedBits = diagnosticReportDecoder.FindCommonBitsAtPosition(input, 1);
            Assert.AreEqual("1", decodedBits);
        }

        [TestMethod]
        public void TestDiagnosticReportDecoderCalculateGamma()
        {
            string[] input = new string[] { "00100", "01110", "11010" };
            var diagnosticReportDecoder = new DiagnosticReportDecoder();

            int gamma = diagnosticReportDecoder.CalculateGammaFromReport(input);
            Assert.AreEqual(14, gamma);
        }

        [TestMethod]
        public void TestDiagnosticReportDecoderCalculateEpsilon()
        {
            string[] input = new string[] { "00100", "01110", "11010" };
            var diagnosticReportDecoder = new DiagnosticReportDecoder();

            int epsilon = diagnosticReportDecoder.CalculateEpsilonFromReport(input);
            Assert.AreEqual(17, epsilon);
        }

        [TestMethod]
        public void TestDiagnosticReportDecoderCalculatePowerConsumption()
        {
            string[] input = new string[] { "00100", "01110", "11010" };
            var diagnosticReportDecoder = new DiagnosticReportDecoder();

            int powerConsumption = diagnosticReportDecoder.CalculatePowerConsumptionFromReport(input);

            Assert.AreEqual(238, powerConsumption);
        }

        [TestMethod]
        public void TestDiagnosticReportDecoderCalculateGammaEpsilonAndPowerConsumptionFromSmallTest ()
        {
            string[] report = LoadReport("smallTest.txt");

            var diagnosticReportDecoder = new DiagnosticReportDecoder();

            int gamma = diagnosticReportDecoder.CalculateGammaFromReport(report);
            int epsilon = diagnosticReportDecoder.CalculateEpsilonFromReport(report);

            Assert.AreEqual(22, gamma);
            Assert.AreEqual(9, epsilon);

            int powerConsumption = diagnosticReportDecoder.CalculatePowerConsumptionFromReport(report);
            Assert.AreEqual(198, powerConsumption);

        }

        [TestMethod]
        public void TestDiagnosticReportDecoderCalculatePowerConsumptionFromReport()
        {
            string[] report = LoadReport("report.txt");

            var diagnosticReportDecoder = new DiagnosticReportDecoder();

            int powerConsumption = diagnosticReportDecoder.CalculatePowerConsumptionFromReport(report);
            Console.WriteLine(powerConsumption);
        }

        [TestMethod]
        public void TestDiagnosticReportDecoderCalculateOxygenRating()
        {
            string[] report = LoadReport("smallTest.txt");

            var diagnosticReportDecoder = new DiagnosticReportDecoder();

            int oxygenRating = diagnosticReportDecoder.CalculateOxygenRatingFromReport(report);

            Assert.AreEqual(23, oxygenRating);
        }

        [TestMethod]
        public void TestDiagnosticReportDecoderCalculateCO2Rating()
        {
            string[] report = LoadReport("smallTest.txt");

            var diagnosticReportDecoder = new DiagnosticReportDecoder();

            int cO2Rating = diagnosticReportDecoder.CalculateCO2RatingFromReport(report);

            Assert.AreEqual(10, cO2Rating);
        }

        [TestMethod]
        public void TestDiagnosticReportDecoderCalculateLifeSupportRating()
        {
            string[] report = LoadReport("smallTest.txt");

            var diagnosticReportDecoder = new DiagnosticReportDecoder();

            int lifeSupportRating = diagnosticReportDecoder.CalculateLifeSupportRatingFromReport(report);

            Assert.AreEqual(230, lifeSupportRating);
        }

        [TestMethod]
        public void TestDiagnosticReportDecoderCalculateLifeSupportRatingFromReport()
        {
            string[] report = LoadReport("report.txt");

            var diagnosticReportDecoder = new DiagnosticReportDecoder();

            int lifeSupportRating = diagnosticReportDecoder.CalculateLifeSupportRatingFromReport(report);
            Console.WriteLine(lifeSupportRating);
        }

        private string[] LoadReport(string filename)
        {
            string[] report = System.IO.File.ReadAllLines(filename);
            return report;
        }
    }
}
