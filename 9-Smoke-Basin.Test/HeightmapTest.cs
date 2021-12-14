using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _9_Smoke_Basin.Test
{
    [TestClass]
    public class HeightmapTest
    {
        [TestMethod]
        public void TestCreateHeightmap()
        {
            Heightmap heightmap = new Heightmap();
            Assert.IsNotNull(heightmap);
        }

        [TestMethod]
        public void TestHeightmapLoadMap()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            Assert.AreEqual(5, heightmap.Map.Count);
            Assert.AreEqual(10, heightmap.Map[0].Count);
        }

        [TestMethod]
        public void TestHeightmapGetRow()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            List<int> mapRow = heightmap.GetRow(0);

            List<int> correctRow = new List<int>() { 2, 1, 9, 9, 9, 4, 3, 2, 1, 0 };
            foreach (var correctItem in correctRow)
                Assert.IsTrue(mapRow.Contains(correctItem));

            Assert.AreEqual(correctRow.Count, mapRow.Count);
        }

        [TestMethod]
        public void TestHeightmapFindLowPointsFirstLine()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            List<int> lowPoints = heightmap.FindLowPointsFirstLine();

            Assert.AreEqual(2, lowPoints.Count);
            Assert.AreEqual(1, lowPoints[0]);
            Assert.AreEqual(0, lowPoints[1]);
        }

        [TestMethod]
        public void TestHeightmapFindLowPointsLastLine()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            List<int> lowPoints = heightmap.FindLowPointsLastLine();

            Assert.AreEqual(1, lowPoints.Count);
            Assert.AreEqual(5, lowPoints[0]);
        }

        [TestMethod]
        public void TestHeightmapFindLowPointsInRow()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            List<int> lowPoints = heightmap.FindLowPointsInRow(2);

            Assert.AreEqual(1, lowPoints.Count);
            Assert.AreEqual(5, lowPoints[0]);
        }

        [TestMethod]
        public void TestHeightmapFindAllLowPoints()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            List<int> lowPoints = heightmap.FindAllLowPoints();

            Assert.AreEqual(4, lowPoints.Count);
            Assert.AreEqual(1, lowPoints[0]);
            Assert.AreEqual(0, lowPoints[1]);
            Assert.AreEqual(5, lowPoints[2]);
            Assert.AreEqual(5, lowPoints[3]);
        }

        [TestMethod]
        public void TestHeightmapCalculateRiskLevels()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            int riskLevel = heightmap.CalculateRiskLevel();

            Assert.AreEqual(15, riskLevel);
        }

        [TestMethod]
        public void TestHeightmapCalculateRiskLevelsFullTest()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("fullTest.txt");
            heightmap.LoadMap(mapInput);

            int riskLevel = heightmap.CalculateRiskLevel();

            Console.WriteLine(riskLevel);
        }

        [TestMethod]
        public void TestHeightmapGetLowPointIndicesForSingleRow()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            List<int> lowPoints = heightmap.FindLowPointsFirstLine();

            var lowPointCoordinates1 = heightmap.LowPointIndices[0];
            var lowPointCoordinates2 = heightmap.LowPointIndices[1];

            Assert.AreEqual(heightmap.Map[lowPointCoordinates1.row][lowPointCoordinates1.column], lowPoints[0]);
            Assert.AreEqual(heightmap.Map[lowPointCoordinates2.row][lowPointCoordinates2.column], lowPoints[1]);
        }

        [TestMethod]
        public void TestHeightmapFindAdjacentHeights()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            List<(int row, int column)> positions = heightmap.FindAdjacentHeights((0, 1));

            Assert.AreEqual(3, positions.Count);
        }

        [TestMethod]
        public void TestHeightmapFindAdjacentHeightsRightCorner()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            List<(int row, int column)> positions = heightmap.FindAdjacentHeights((0, 9));
            Assert.AreEqual(9, positions.Count);
        }

        [TestMethod]
        public void TestHeightmapCalculateBasinSize()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            int size = heightmap.CalculateBasinSize((0, 1));

            Assert.AreEqual(3, size);
        }

        [TestMethod]
        public void TestHeightmapFindBiggestBasin()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("smallTest.txt");
            heightmap.LoadMap(mapInput);

            List<int> sizes = heightmap.FindSizeOfThreeLargestBasins();

            int[] correctSizes = new int[] { 14, 9, 9 };
            for (int index = 0; index < correctSizes.Length; index++)
                Assert.AreEqual(correctSizes[index], sizes[index]);
            int sum = sizes.Aggregate((number1, number2) => number1 * number2);

            Assert.AreEqual(1134, sum);
        }

        [TestMethod]
        public void TestHeightmapFindBiggestBasinSumFullTest()
        {
            Heightmap heightmap = new Heightmap();
            string[] mapInput = LoadMapFromFile("fullTest.txt");
            heightmap.LoadMap(mapInput);

            List<int> sizes = heightmap.FindSizeOfThreeLargestBasins();

            int sum = sizes.Aggregate((number1, number2) => number1 * number2);

            Console.WriteLine(sum);

        }

        private string[] LoadMapFromFile(string path)
        {
            string[] map = System.IO.File.ReadAllLines(path);
            return map;
        }
    }
}
