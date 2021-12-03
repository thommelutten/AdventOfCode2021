using System;
using System.Linq;

namespace _3_Binary_Diagnostics
{
    public class DiagnosticReportDecoder
    {
        public string FindCommonBitsAtPosition(string[] input, int position)
        {
            var oddOccurences = input.Count(bitSequence => bitSequence[position].Equals('0'));

            if (oddOccurences > (input.Length / 2))
            {
                return "0";
            } else if(((double)oddOccurences) == ((((double)input.Length) / 2)))
            {
                return string.Empty;
            }

            return "1";
        }

        public int CalculateGammaFromReport(string[] report)
        {
            string gammaInBits = string.Empty;

            var inputBitSequenceLength = report.First().Length;
            for (int bitIndex = 0; bitIndex < inputBitSequenceLength; bitIndex++)
                gammaInBits += FindCommonBitsAtPosition(report, bitIndex);

            int gamma = ConvertStringBinaryToInt(report.First());

            return gamma;
        }

        public int CalculateEpsilonFromReport(string[] report)
        {
            string epsilonInBits = string.Empty;

            var inputBitSequenceLength = report.First().Length;
            for (int bitIndex = 0; bitIndex < inputBitSequenceLength; bitIndex++)
            {
                var commonBit = FindCommonBitsAtPosition(report, bitIndex);
                epsilonInBits += commonBit.Equals("0") ? "1" : "0";
            }

            int epsilon = ConvertStringBinaryToInt(report.First());

            return epsilon;
        }

        public int CalculatePowerConsumptionFromReport(string[] report)
        {
            int gamma = CalculateGammaFromReport(report);
            int epsilon = CalculateEpsilonFromReport(report);

            int powerConsumption = gamma * epsilon;

            return powerConsumption;
        }

        public int CalculateOxygenRatingFromReport(string[] report)
        {
            var inputBitSequenceLength = report.First().Length;
            for (int bitIndex = 0; bitIndex < inputBitSequenceLength; bitIndex++)
            {
                var commonBit = FindCommonBitsAtPosition(report, bitIndex);

                commonBit = commonBit == "" ? "1" : commonBit;

                report = report.Where(bitSequence => bitSequence[bitIndex].ToString() == commonBit).ToArray();

                if (OnlyOneReadingLeft(report))
                    break;
            }

            int oxygenRating = ConvertStringBinaryToInt(report.First());
            return oxygenRating;
        }

        private bool OnlyOneReadingLeft(string[] report)
        {
            return report.Length == 1;
        }

        public int CalculateCO2RatingFromReport(string[] report)
        {
            var inputBitSequenceLength = report.First().Length;
            for (int bitIndex = 0; bitIndex < inputBitSequenceLength; bitIndex++)
            {
                var commonBit = FindCommonBitsAtPosition(report, bitIndex);

                commonBit = commonBit == "" ? "1" : commonBit;

                report = report.Where(bitSequence => bitSequence[bitIndex].ToString() != commonBit).ToArray();
                if (OnlyOneReadingLeft(report))
                    break;
            }

            int cO2Rating = ConvertStringBinaryToInt(report.First());
            return cO2Rating;
        }

        private int ConvertStringBinaryToInt(string binaryValue)
        {
            return Convert.ToInt32(binaryValue, 2);
        }

        public int CalculateLifeSupportRatingFromReport(string[] report)
        {
            int oxygenRating = CalculateOxygenRatingFromReport(report);
            int cO2Rating = CalculateCO2RatingFromReport(report);

            int lifeSupportRating = oxygenRating * cO2Rating;

            return lifeSupportRating;
        }
    }
}
