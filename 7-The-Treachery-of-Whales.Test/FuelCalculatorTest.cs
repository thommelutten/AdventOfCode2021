using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _7_The_Treachery_of_Whales.Test
{
    [TestClass]
    public class FuelCalculatorTest
    {
        [TestMethod]
        public void TestFuelCalculatorCalculateFuelForBestHorizontalPosition()
        {
            FuelCalculator fuelCalculator = new FuelCalculator();

            int[] crabs = { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

            int fuelNeeded = fuelCalculator.CalculateFuelForBestHorizontalPosition(crabs);

            Assert.AreEqual(37, fuelNeeded);
        }

        [TestMethod]
        public void TestFuelCalculatorCalculateFuelForBestHorizontalPositionFull()
        {
            FuelCalculator fuelCalculator = new FuelCalculator();

            int[] crabs = LoadCrabPositions("fullTest.txt");

            int fuelNeeded = fuelCalculator.CalculateFuelForBestHorizontalPosition(crabs);

            Console.WriteLine(fuelNeeded);
        }

        [TestMethod]
        public void TestFuelCalculatorCalculateFuelForBestHorizontalPositionExpensiveStepsEnabled()
        {
            FuelCalculator fuelCalculator = new FuelCalculator();
            fuelCalculator.EnableExpensiveSteps();

            int[] crabs = { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

            int fuelNeeded = fuelCalculator.CalculateFuelForBestHorizontalPosition(crabs);

            Assert.AreEqual(168, fuelNeeded);
        }

        [TestMethod]
        public void TestFuelCalculatorCalculateFuelForBestHorizontalPositionExpensiveStepsEnabledFull()
        {
            FuelCalculator fuelCalculator = new FuelCalculator();
            fuelCalculator.EnableExpensiveSteps();

            int[] crabs = LoadCrabPositions("fullTest.txt");

            int fuelNeeded = fuelCalculator.CalculateFuelForBestHorizontalPosition(crabs);

            Console.WriteLine(fuelNeeded);
        }

        public int[] LoadCrabPositions(string path)
        {
            string[] input = System.IO.File.ReadAllLines(path);
            int[] inputAsInts = Array.ConvertAll(input[0].Split(','), inputValue => int.Parse(inputValue));
            return inputAsInts;
        }
    }
}
