using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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


        private string[] LoadMapFromFile(string path)
        {
            string[] map = System.IO.File.ReadAllLines(path);
            return map;
        }
    }
}
