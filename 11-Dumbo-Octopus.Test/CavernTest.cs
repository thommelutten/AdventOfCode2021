using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace _11_Dumbo_Octopus.Test
{
    [TestClass]
    public class CavernTest
    {
        [TestMethod]
        public void TestCavernCreate()
        {
            Cavern cavern = new Cavern();
            Assert.AreEqual(0, cavern.Octopodes.Count);
        }

        [TestMethod]
        public void TestCavernLoadOctopodes()
        {
            Cavern cavern = new Cavern();
            cavern.LoadOctopodes(LoadOctopodesFromFile("smallTest.txt"));

            Assert.AreEqual(10, cavern.Octopodes.Count);

            foreach (var octopodeRow in cavern.Octopodes)
                Assert.AreEqual(10, octopodeRow.Count);
        }

        [TestMethod]
        public void TestCavernTakeStep()
        {
            Cavern cavern = new Cavern();

            var octopodeEnergyLevels = LoadOctopodesFromFile("smallTest.txt");
            cavern.LoadOctopodes(octopodeEnergyLevels);

            var oldOctopodesValue = cavern.Octopodes;

            cavern.Step();

            var newOctopodesValue = cavern.Octopodes;

            for (var verticalIndex = 0; verticalIndex < oldOctopodesValue.Count; verticalIndex++)
                for (var horizontalIndex = 0; horizontalIndex < oldOctopodesValue[0].Count; horizontalIndex++)
                    Assert.AreEqual((octopodeEnergyLevels[verticalIndex][horizontalIndex] + 1), newOctopodesValue[verticalIndex][horizontalIndex].Energy);
        }

        [TestMethod]
        public void TestCavernOctopodesCanFlash()
        {
            Cavern cavern = new Cavern();

            var octopodeEnergyLevels = LoadOctopodesFromFile("smallTest.txt");
            cavern.LoadOctopodes(octopodeEnergyLevels);

            cavern.Step();
            var octopodesCanFlash = cavern.OctopodesCanFlash();

            Assert.IsFalse(octopodesCanFlash);

            cavern.Step();

            octopodesCanFlash = cavern.OctopodesCanFlash();

            Assert.IsTrue(octopodesCanFlash);
        }

        [TestMethod]
        public void TestCavernGetIndexOfOctopodesThatCanFlash()
        {
            Cavern cavern = new Cavern();

            var octopodeEnergyLevels = LoadOctopodesFromFile("smallTest.txt");
            cavern.LoadOctopodes(octopodeEnergyLevels);

            cavern.Step();
            cavern.Step();

            var indicesOfOctopodesThatCanFlash = cavern.GetIndicesOfOctopodesThatCanFlash();

            Assert.AreEqual(13, indicesOfOctopodesThatCanFlash.Count);
        }

        [TestMethod]
        public void TestCavernStepAndCheckForFlashes()
        {
            Cavern cavern = InitializeCavern("smallTest.txt");

            cavern.StepAndCheckForFlashes();
            List<List<Octopus>> octopodes = cavern.GetOctopodes();

            foreach (var octopodeRow in octopodes)
                foreach (var octopus in octopodeRow)
                    Assert.IsFalse(octopus.Energy == 0);
        }

        [TestMethod]
        public void TestCavernTwoStepsAndCheckForFlashes()
        {
            Cavern cavern = InitializeCavern("smallTest.txt");

            cavern.StepAndCheckForFlashes();
            cavern.StepAndCheckForFlashes();

            Assert.AreEqual(35, cavern.OctopodesThatHasFlashed);
        }

        [TestMethod]
        public void TestCavernTenStepsAndCheckForFlashes()
        {
            Cavern cavern = InitializeCavern("smallTest.txt");

            for(int steps = 0; steps < 10; steps++)
                cavern.StepAndCheckForFlashes();

            Assert.AreEqual(204, cavern.OctopodesThatHasFlashed);
        }

        [TestMethod]
        public void TestCavernHundredStepsAndCheckForFlashes()
        {
            Cavern cavern = InitializeCavern("smallTest.txt");

            for (int steps = 0; steps < 100; steps++)
                cavern.StepAndCheckForFlashes();

            Assert.AreEqual(1656, cavern.OctopodesThatHasFlashed);
        }

        [TestMethod]
        public void TestCavernHundredStepsAndCheckForFlashesFullTest()
        {
            Cavern cavern = InitializeCavern("fullTest.txt");

            for (int steps = 0; steps < 100; steps++)
                cavern.StepAndCheckForFlashes();

            Console.WriteLine(cavern.OctopodesThatHasFlashed);
        }

        [TestMethod]
        public void TestCavernCheckForSyncronizedFlash()
        {
            Cavern cavern = InitializeCavern("smallTest.txt");
            int stepsRequired = 0;
            for(int steps = 1; steps < 200; steps++)
            {
                cavern.StepAndCheckForFlashes();
                if (cavern.OctopodesFlashedSyncronously())
                {
                    stepsRequired = steps;
                    break;
                }
            }
            Assert.AreEqual(195, stepsRequired);
        }

        [TestMethod]
        public void TestCavernCheckForSyncronizedFlashFullTest()
        {
            Cavern cavern = InitializeCavern("fullTest.txt");

            // We don't know how many steps to take. Set max to 1000 steps instead of a while(true)
            for (int steps = 1; steps < 1000; steps++)
            {
                cavern.StepAndCheckForFlashes();
                if (cavern.OctopodesFlashedSyncronously())
                {
                    Console.WriteLine(steps);
                    break;
                }
            }
        }

        private Cavern InitializeCavern(string path)
        {
            Cavern cavern = new Cavern();

            var octopodeEnergyLevels = LoadOctopodesFromFile(path);
            cavern.LoadOctopodes(octopodeEnergyLevels);

            return cavern;
        }

        private List<List<int>> LoadOctopodesFromFile(string path)
        {
            string[] octopodesFromFile = System.IO.File.ReadAllLines(path);

            List<List<int>> octopodesList = new List<List<int>>();

            foreach(var octopodeLine in octopodesFromFile)
            {
                List<int> octopodeRow = new List<int>();
                foreach (char value in octopodeLine)
                    octopodeRow.Add(int.Parse(value.ToString()));
                octopodesList.Add(octopodeRow);
            }

            return octopodesList;
        }
    }
}
