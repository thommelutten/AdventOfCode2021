using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _8_Seven_Segment_Search
{
    public class SegmentDecoder
    {
        private Dictionary<string, int> SequenceToDigit = new Dictionary<string, int>();
        private Dictionary<int, string> DigitToSequence = new Dictionary<int, string>();

        private string FindCommonSegments(string firstDigit, string secondDigit)
        {
            var commonSegments = firstDigit.Intersect(secondDigit).ToArray();
            return new string(commonSegments);
        }

        private string FindUniqueSegmentsForFirstDigit(string firstDigit, string secondDigit)
        {
            var uniqueSegments = firstDigit.Where(segment => !secondDigit.Contains(segment)).ToArray();
            return new string(uniqueSegments);
        }

        public Dictionary<string, int> GetSequenceToDigitTable()
        {
            return SequenceToDigit;
        }

        public Dictionary<string, int> InitializeSequence(string inputSequence)
        {
            foreach (var input in inputSequence.Split(' '))
            {
                if (input == "|")
                    continue;

                var orderedInput = new string(input.OrderBy(c => c).ToArray());
                SequenceToDigit[orderedInput] = -1;
            } 

            return SequenceToDigit;
        }

        public void IndexUniqueDigits()
        {
            var sequenceOf1 = SequenceToDigit.Where(sequence => sequence.Key.Length == 2);
            if(sequenceOf1.Any())
                SequenceToDigit[sequenceOf1.First().Key] = 1;

            var sequenceOf4 = SequenceToDigit.Where(sequence => sequence.Key.Length == 4);
            if(sequenceOf4.Any())
                SequenceToDigit[sequenceOf4.First().Key] = 4;

            var sequenceOf7 = SequenceToDigit.Where(sequence => sequence.Key.Length == 3);
            if(sequenceOf7.Any())
                SequenceToDigit[sequenceOf7.First().Key] = 7;

            var sequenceOf8 = SequenceToDigit.Where(sequence => sequence.Key.Length == 7);
            if(sequenceOf8.Any())
                SequenceToDigit[sequenceOf8.First().Key] = 8;

        }

        public string GetSequenceForDigit(int digit)
        {
            if(SequenceToDigit.Where(sequenceAndDigit => sequenceAndDigit.Value == digit).Any())
                return SequenceToDigit.Where(sequenceAndDigit => sequenceAndDigit.Value == digit).First().Key;
            return string.Empty;
        }

        public int GetDigitForSequence(string sequence)
        {
            bool digitFound = SequenceToDigit.TryGetValue(sequence, out int digit);
            return digitFound ? digit : -1;
        }

        public bool CanFindDigit0()
        {
            return !string.IsNullOrEmpty(GetSequenceForDigit(4)) || !string.IsNullOrEmpty(GetSequenceForDigit(1)); 
        }

        public void FindDigit0()
        {
            var sequenceFor4 = GetSequenceForDigit(4);
            var sequenceFor1 = GetSequenceForDigit(1);

            var difference = FindUniqueSegmentsForFirstDigit(sequenceFor4, sequenceFor1);
            var sequenceFor0 = SequenceToDigit.Where(sequence => sequence.Key.Length == 6 
                && (
                    (sequence.Key.Contains(difference[0]) && !sequence.Key.Contains(difference[1])
                    ) || 
                    (sequence.Key.Contains(difference[1]) && !sequence.Key.Contains(difference[0])))).First();
            SequenceToDigit[sequenceFor0.Key] = 0;
        }

        public bool CanFindDigit9()
        {
            return !string.IsNullOrEmpty(GetSequenceForDigit(0)) && !string.IsNullOrEmpty(GetSequenceForDigit(1));
        }

        public void FindDigit9()
        {
            var sequenceFor4 = GetSequenceForDigit(4);

            var sequencesWith6Length = SequenceToDigit.Where(sequenceAndDigit => sequenceAndDigit.Key.Length == 6).ToList();
            sequencesWith6Length.RemoveAll(sequence => sequence.Value == 0);

            for(int index = 0; index < sequencesWith6Length.Count; index++)
            {
                var commonSegments = FindCommonSegments(sequencesWith6Length[index].Key, sequenceFor4);
                if (commonSegments.Length == 4)
                {
                    SequenceToDigit[sequencesWith6Length[index].Key] = 9;
                    break;
                }  
            }
        }

        public void FindDigit6()
        {
            var sequenceFor0 = GetSequenceForDigit(0);
            var sequenceFor9 = GetSequenceForDigit(9);

            var sequencesWith6Length = SequenceToDigit.Where(sequenceAndDigit => sequenceAndDigit.Key.Length == 6).ToList();
            sequencesWith6Length.RemoveAll(sequence => sequence.Key == sequenceFor0 || sequence.Key == sequenceFor9);

            if (sequencesWith6Length.Count > 1)
                throw new Exception();
            SequenceToDigit[sequencesWith6Length.First().Key] = 6;
        }

        public void FindDigit5()
        {
            var sequenceFor6 = GetSequenceForDigit(6);

            var sequencesWith5Length = SequenceToDigit.Where(sequenceAndDigit => sequenceAndDigit.Key.Length == 5).ToList();

            for (int index = 0; index < sequencesWith5Length.Count; index++)
            {
                var commonSegments = FindCommonSegments(sequencesWith5Length[index].Key, sequenceFor6);
                if (commonSegments.Length == 5)
                {
                    SequenceToDigit[sequencesWith5Length[index].Key] = 5;
                    break;
                }
            }
        }

        public void FindDigit3()
        {
            var sequenceFor6 = GetSequenceForDigit(5);

            var sequencesWith6Length = SequenceToDigit.Where(sequenceAndDigit => sequenceAndDigit.Key.Length == 5).ToList();

            for (int index = 0; index < sequencesWith6Length.Count; index++)
            {
                var commonSegments = FindCommonSegments(sequencesWith6Length[index].Key, sequenceFor6);
                if (commonSegments.Length == 4)
                {
                    SequenceToDigit[sequencesWith6Length[index].Key] = 3;
                    break;
                }
            }
        }

        public void FindDigit2()
        {
            var sequenceFor6 = GetSequenceForDigit(5);

            var sequencesWith6Length = SequenceToDigit.Where(sequenceAndDigit => sequenceAndDigit.Key.Length == 5).ToList();

            for (int index = 0; index < sequencesWith6Length.Count; index++)
            {
                var commonSegments = FindCommonSegments(sequencesWith6Length[index].Key, sequenceFor6);
                if (commonSegments.Length == 3)
                {
                    SequenceToDigit[sequencesWith6Length[index].Key] = 2;
                    break;
                }
            }
        }

        public void FindDigits()
        {
            IndexUniqueDigits();

            FindDigit0();
            FindDigit9();
            FindDigit6();

            FindDigit5();
            FindDigit3();
            FindDigit2();
        }

        public Dictionary<int, int> CalculateNumberCountParseSequence(string inputSequence)
        {
            var sequencesCoded = inputSequence.Split(' ');
            var outputSequence = sequencesCoded[11..];

            var orderedSequence = orderSequence(outputSequence);

            Dictionary<int, int> numbersCount = new Dictionary<int, int>();
            foreach(var sequenceCoded in orderedSequence)
            {
                int digit = SequenceToDigit[sequenceCoded];
                if (!numbersCount.ContainsKey(digit))
                    numbersCount.Add(digit, 1);
                else
                    numbersCount[digit] += 1;
            }

            return numbersCount;
        }

        public int CalculateOutput(string inputSequence)
        {
            var sequencesCoded = inputSequence.Split(' ');
            var outputSequence = sequencesCoded[11..];

            var orderedSequence = orderSequence(outputSequence);

            var outputNumber = string.Empty;
            foreach (var sequenceCoded in orderedSequence)
            {
                int digit = SequenceToDigit[sequenceCoded];
                outputNumber += digit.ToString();
            }
            return int.Parse(outputNumber);
        }

        private string[] orderSequence(string[] input)
        {
            for (int index = 0; index < input.Length; index++)
                input[index] = new string(input[index].OrderBy(c => c).ToArray());
            return input;
        }
    }
}
