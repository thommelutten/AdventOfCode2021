using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _8_Seven_Segment_Search.Test
{
    [TestClass]
    public class SegmentDecoderTest
    {
        [TestMethod]
        public void TestSegmentDecoderParse()
        {
            var inputSequence = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab";
            SegmentDecoder segmentDecoder = new SegmentDecoder();
            segmentDecoder.InitializeSequence(inputSequence);
            Dictionary<string, int> sequenceToDigitTable = segmentDecoder.GetSequenceToDigitTable();

            foreach(var input in inputSequence.Split(' '))
            {
                var orderedInput = new string(input.OrderBy(c => c).ToArray());
                Assert.IsTrue(sequenceToDigitTable.ContainsKey(orderedInput));
            }
        }

        [TestMethod]
        public void TestSegmentDecoderFindDigit0()
        {
            var inputSequence = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab";
            SegmentDecoder segmentDecoder = new SegmentDecoder();
            segmentDecoder.InitializeSequence(inputSequence);
            segmentDecoder.IndexUniqueDigits();

            bool canFindDigit0 = segmentDecoder.CanFindDigit0();
            Assert.IsTrue(canFindDigit0);

            segmentDecoder.FindDigit0();
            Dictionary<string, int> sequenceToDigitTable = segmentDecoder.GetSequenceToDigitTable();
            Assert.IsTrue(sequenceToDigitTable.ContainsValue(0));
        }
        [TestMethod]
        public void TestSegmentDecoderFindDigit9()
        {
            var inputSequence = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab";
            SegmentDecoder segmentDecoder = new SegmentDecoder();
            segmentDecoder.InitializeSequence(inputSequence);
            segmentDecoder.IndexUniqueDigits();

            bool canFindDigit0 = segmentDecoder.CanFindDigit0();
            Assert.IsTrue(canFindDigit0);

            segmentDecoder.FindDigit0();

            bool canFindDigit9 = segmentDecoder.CanFindDigit9();
            Assert.IsTrue(canFindDigit9);

            segmentDecoder.FindDigit9();

            Dictionary<string, int> sequenceToDigitTable = segmentDecoder.GetSequenceToDigitTable();
            Assert.IsTrue(sequenceToDigitTable.ContainsValue(9));
        }

        [TestMethod]
        public void TestSegmentDecoderFindDigit6()
        {
            var inputSequence = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab";
            SegmentDecoder segmentDecoder = new SegmentDecoder();
            segmentDecoder.InitializeSequence(inputSequence);
            segmentDecoder.IndexUniqueDigits();

            segmentDecoder.FindDigit0();
            segmentDecoder.FindDigit9();

            segmentDecoder.FindDigit6();

            Dictionary<string, int> sequenceToDigitTable = segmentDecoder.GetSequenceToDigitTable();
            Assert.IsTrue(sequenceToDigitTable.ContainsValue(6));
        }

        [TestMethod]
        public void TestSegmentDecoderFindDigit5()
        {
            var inputSequence = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab";
            SegmentDecoder segmentDecoder = new SegmentDecoder();
            segmentDecoder.InitializeSequence(inputSequence);
            segmentDecoder.IndexUniqueDigits();

            segmentDecoder.FindDigit0();
            segmentDecoder.FindDigit9();
            segmentDecoder.FindDigit6();

            segmentDecoder.FindDigit5();

            Dictionary<string, int> sequenceToDigitTable = segmentDecoder.GetSequenceToDigitTable();
            Assert.IsTrue(sequenceToDigitTable.ContainsValue(5));
        }

        [TestMethod]
        public void TestSegmentDecoderFindDigit3()
        {
            var inputSequence = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab";
            SegmentDecoder segmentDecoder = new SegmentDecoder();
            segmentDecoder.InitializeSequence(inputSequence);
            segmentDecoder.IndexUniqueDigits();

            segmentDecoder.FindDigit0();
            segmentDecoder.FindDigit9();
            segmentDecoder.FindDigit6();

            segmentDecoder.FindDigit5();
            segmentDecoder.FindDigit3();

            Dictionary<string, int> sequenceToDigitTable = segmentDecoder.GetSequenceToDigitTable();
            Assert.IsTrue(sequenceToDigitTable.ContainsValue(3));
        }

        [TestMethod]
        public void TestSegmentDecoderFindDigit2()
        {
            var inputSequence = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab";
            SegmentDecoder segmentDecoder = new SegmentDecoder();
            segmentDecoder.InitializeSequence(inputSequence);
            segmentDecoder.IndexUniqueDigits();

            segmentDecoder.FindDigit0();
            segmentDecoder.FindDigit9();
            segmentDecoder.FindDigit6();

            segmentDecoder.FindDigit5();
            segmentDecoder.FindDigit3();
            segmentDecoder.FindDigit2();

            Dictionary<string, int> sequenceToDigitTable = segmentDecoder.GetSequenceToDigitTable();
            Assert.IsTrue(sequenceToDigitTable.ContainsValue(2));
        }

        [TestMethod]
        public void TestSegmentDecoderFindDigits()
        {
            var inputSequence = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab";
            SegmentDecoder segmentDecoder = new SegmentDecoder();
            segmentDecoder.InitializeSequence(inputSequence);
            segmentDecoder.FindDigits();
            Dictionary<string, int> sequenceToDigitTable = segmentDecoder.GetSequenceToDigitTable();
            var digitsNotSet = sequenceToDigitTable.Values.Count(value => value == -1);
            Assert.AreEqual(0, digitsNotSet);
        }

        [TestMethod]
        public void TestSegmentDecoderLoadSequenceFullLine()
        {
            var inputSequence = "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe";
            SegmentDecoder segmentDecoder = new SegmentDecoder();
            segmentDecoder.InitializeSequence(inputSequence);
            segmentDecoder.FindDigits();
            Dictionary<string, int> sequenceToDigitTable = segmentDecoder.GetSequenceToDigitTable();
            var digitsNotSet = sequenceToDigitTable.Values.Count(value => value == -1);

            Assert.AreEqual(0, digitsNotSet);
        }

        [TestMethod]
        public void TestSegmentDecoderParseSequenceSmallTest()
        {
            var inputSequences = LoadSequences("smallTest.txt");
            var total = 0;
            foreach(var inputSequence in inputSequences)
            {
                SegmentDecoder segmentDecoder = new SegmentDecoder();
                segmentDecoder.InitializeSequence(inputSequence);
                segmentDecoder.FindDigits();

                Dictionary<int, int> occurences = segmentDecoder.CalculateNumberCountParseSequence(inputSequence);

                occurences.TryGetValue(1, out int ones);
                occurences.TryGetValue(4, out int fours);
                occurences.TryGetValue(7, out int sevens);
                occurences.TryGetValue(8, out int eights);
                total += ones + fours + sevens + eights;
            }
            
            Console.WriteLine(total);
            
        }

        [TestMethod]
        public void TestSegmentDecoderParseSequenceFullTest()
        {
            var inputSequences = LoadSequences("fullTest.txt");
            var total = 0;
            foreach (var inputSequence in inputSequences)
            {
                SegmentDecoder segmentDecoder = new SegmentDecoder();
                segmentDecoder.InitializeSequence(inputSequence);
                segmentDecoder.FindDigits();

                Dictionary<int, int> occurences = segmentDecoder.CalculateNumberCountParseSequence(inputSequence);

                occurences.TryGetValue(1, out int ones);
                occurences.TryGetValue(4, out int fours);
                occurences.TryGetValue(7, out int sevens);
                occurences.TryGetValue(8, out int eights);
                total += ones + fours + sevens + eights;
            }
            Console.WriteLine(total);
        }

        [TestMethod]
        public void TestSegmentDecoderCalculateOutputFullTest()
        {
            var inputSequences = LoadSequences("fullTest.txt");
            var total = 0;
            foreach (var inputSequence in inputSequences)
            {
                SegmentDecoder segmentDecoder = new SegmentDecoder();
                segmentDecoder.InitializeSequence(inputSequence);
                segmentDecoder.FindDigits();

                total += segmentDecoder.CalculateOutput(inputSequence);

            }
            Console.WriteLine(total);
        }

        public string[] LoadSequences(string path)
        {
            string[] input = System.IO.File.ReadAllLines(path);
            return input;
        }
    }
}
