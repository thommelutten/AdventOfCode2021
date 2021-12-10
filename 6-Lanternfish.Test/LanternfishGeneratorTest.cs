using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _6_Lanternfish.Test
{
    [TestClass]
    public class LanternfishGeneratorTest
    {
        [TestMethod]
        public void TestLanternfishGeneratorLoadSchoolCountFish()
        {
            int[] lanternfishSchool = new int[] { 3, 4, 3, 1, 2 };
            LanternfishGenerator lanternfishGenerator = new LanternfishGenerator(lanternfishSchool);

            Assert.AreEqual(5, lanternfishGenerator.CountFish());
        }

        [TestMethod]
        public void TestLanternfishGeneratorSpawnFish18Days()
        {
            int[] lanternfishSchool = new int[] { 3, 4, 3, 1, 2 };
            LanternfishGenerator lanternfishGenerator = new LanternfishGenerator(lanternfishSchool);

            lanternfishGenerator.RunDays(18);

            Assert.AreEqual(26, lanternfishGenerator.CountFish());
        }

        [TestMethod]
        public void TestLanternfishGeneratorSpawnFishSmallTest80Days()
        {
            int[] lanternfishSchool = new int[] { 3, 4, 3, 1, 2 };
            LanternfishGenerator lanternfishGenerator = new LanternfishGenerator(lanternfishSchool);

            lanternfishGenerator.RunDays(80);

            Assert.AreEqual(5934, lanternfishGenerator.CountFish());
        }

        [TestMethod]
        public void TestLanternfishGeneratorSpawnFishSmallTest256Days()
        {
            int[] lanternfishSchool = new int[] { 3, 4, 3, 1, 2 };
            LanternfishGenerator lanternfishGenerator = new LanternfishGenerator(lanternfishSchool);

            lanternfishGenerator.RunDays(256);

            Assert.AreEqual(26984457539, lanternfishGenerator.CountFish());
        }

        [TestMethod]
        public void TestLanternfishGeneratorSpawnFishFullTest()
        {
            int[] lanternfishSchool = LoadSchool("fullTest.txt");
            LanternfishGenerator lanternfishGenerator = new LanternfishGenerator(lanternfishSchool);

            lanternfishGenerator.RunDays(80);

            Console.WriteLine(lanternfishGenerator.CountFish());
        }

        [TestMethod]
        public void TestLanternfishGeneratorSpawnFishFull256DaysTest()
        {
            int[] lanternfishSchool = LoadSchool("fullTest.txt");
            LanternfishGenerator lanternfishGenerator = new LanternfishGenerator(lanternfishSchool);

            lanternfishGenerator.RunDays(256);

            Console.WriteLine(lanternfishGenerator.CountFish());
        }

        private int[] LoadSchool(string path)
        {
            string[] input = System.IO.File.ReadAllLines(path);
            int[] inputAsInts = Array.ConvertAll(input[0].Split(','), inputValue => int.Parse(inputValue));
            return inputAsInts;
        }
    }
}
