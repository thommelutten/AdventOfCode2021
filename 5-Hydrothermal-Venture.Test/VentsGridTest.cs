using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _5_Hydrothermal_Venture.Test
{
    [TestClass]
    public class VentsGridTest
    {
        [TestMethod]
        public void TestVentsGridCreateVentsGrid()
        {
            VentsGrid ventsGrid = new VentsGrid();

            List<List<int>> ventsGridList = ventsGrid.getGrid();

            Assert.IsFalse(ventsGridList.Any());
        }

        [TestMethod]
        public void TestVentsGridParseLine()
        {
            VentsGrid ventsGrid = new VentsGrid();
            string line = "0,0 -> 9,0";

            var coordinates = ventsGrid.ParseLine(line);

            Assert.AreEqual(0, coordinates.startX);
            Assert.AreEqual(0, coordinates.startY);
            Assert.AreEqual(9, coordinates.endX);
            Assert.AreEqual(0, coordinates.endY);
        }

        [TestMethod]
        public void TestVentsGridAddVentsLineHorizontal()
        {
            VentsGrid ventsGrid = new VentsGrid();

            string line = "0,0 -> 9,0";
            var coordinates = ventsGrid.ParseLine(line);

            ventsGrid.AddVentsLine(coordinates);

            List<List<int>> grid = ventsGrid.getGrid();

            Assert.AreEqual(10, grid[0].Sum());
        }

        [TestMethod]
        public void TestVentsGridAddVentsLineVertical()
        {
            VentsGrid ventsGrid = new VentsGrid();

            string line = "0,0 -> 0,9";
            var coordinates = ventsGrid.ParseLine(line);

            ventsGrid.AddVentsLine(coordinates);

            List<List<int>> grid = ventsGrid.getGrid();

            Assert.AreEqual(1, grid[0].Sum());
            Assert.AreEqual(1, grid[1].Sum());
            Assert.AreEqual(1, grid[2].Sum());
            Assert.AreEqual(1, grid[3].Sum());
            Assert.AreEqual(1, grid[4].Sum());
            Assert.AreEqual(1, grid[5].Sum());
            Assert.AreEqual(1, grid[6].Sum());
            Assert.AreEqual(1, grid[7].Sum());
            Assert.AreEqual(1, grid[8].Sum());
            Assert.AreEqual(1, grid[9].Sum());
        }

        [TestMethod]
        public void TestVentsGridFlipLineToPositiveDirection()
        {
            VentsGrid ventsGrid = new VentsGrid();

            string line = "3,2 -> 0,1";
            var coordinates = ventsGrid.ParseLine(line);

            coordinates = ventsGrid.FlipLineToPositiveDirection(coordinates);

            Assert.AreEqual(0, coordinates.startX);
            Assert.AreEqual(1, coordinates.startY);
            Assert.AreEqual(3, coordinates.endX);
            Assert.AreEqual(2, coordinates.endY);
        }

        [TestMethod]
        public void TestVentsGridSmallTest()
        {
            string[] ventLinesInput = LoadTestFile("smallTest.txt");

            VentsGrid ventsGrid = new VentsGrid();
            foreach(var ventLine in ventLinesInput)
            {
                var coordinates = ventsGrid.ParseLine(ventLine);
                ventsGrid.AddVentsLine(coordinates);
            }

            List<List<int>> grid = ventsGrid.getGrid();

            var positions = 0;
            foreach(var gridLine in grid)
            {
                positions += gridLine.Where(position => position >= 2).Count();
            }

            Assert.AreEqual(5, positions);
        }

        [TestMethod]
        public void TestVentsGridFullTest()
        {
            string[] ventLinesInput = LoadTestFile("fullTest.txt");

            VentsGrid ventsGrid = new VentsGrid();
            foreach (var ventLine in ventLinesInput)
            {
                var coordinates = ventsGrid.ParseLine(ventLine);
                ventsGrid.AddVentsLine(coordinates);
            }

            List<List<int>> grid = ventsGrid.getGrid();

            var positions = 0;
            foreach (var gridLine in grid)
            {
                positions += gridLine.Where(position => position >= 2).Count();
            }

            Console.WriteLine(positions);
        }

        [TestMethod]
        public void TestVentsGridDiagonalLine()
        {
            VentsGrid ventsGrid = new VentsGrid();
            ventsGrid.UseDiagonal = true;
            string line = "1,1 -> 3,3";
            var coordinates = ventsGrid.ParseLine(line);

            ventsGrid.AddVentsLine(coordinates);

            List<List<int>> grid = ventsGrid.getGrid();

            Assert.AreEqual(0, grid[0].Sum());
            Assert.AreEqual(1, grid[1].Sum());
            Assert.AreEqual(1, grid[2].Sum());
            Assert.AreEqual(1, grid[3].Sum());
        }

        [TestMethod]
        public void TestVentsGridTwoDiagonalLines()
        {
            VentsGrid ventsGrid = new VentsGrid();
            ventsGrid.UseDiagonal = true;
            string line = "1,1 -> 3,3";
            var coordinates = ventsGrid.ParseLine(line);

            ventsGrid.AddVentsLine(coordinates);

            string line2 = "2,3 -> 3,4";

            coordinates = ventsGrid.ParseLine(line2);

            ventsGrid.AddVentsLine(coordinates);

            List<List<int>> grid = ventsGrid.getGrid();

            Assert.AreEqual(0, grid[0].Sum());
            Assert.AreEqual(1, grid[1].Sum());
            Assert.AreEqual(2, grid[2].Sum());
            Assert.AreEqual(1, grid[3].Sum());
        }

        [TestMethod]
        public void TestVentsGridFlipLineToPositiveDirectionDiagonal()
        {
            VentsGrid ventsGrid = new VentsGrid();

            string line = "3,3 -> 1,1";
            var coordinates = ventsGrid.ParseLine(line);

            coordinates = ventsGrid.FlipLineToPositiveDirection(coordinates);

            Assert.AreEqual(1, coordinates.startX);
            Assert.AreEqual(1, coordinates.startY);
            Assert.AreEqual(3, coordinates.endX);
            Assert.AreEqual(3, coordinates.endY);
        }

        [TestMethod]
        public void TestVentsGridSmallTestUseDiagonal()
        {
            string[] ventLinesInput = LoadTestFile("smallTest.txt");

            VentsGrid ventsGrid = new VentsGrid();
            ventsGrid.UseDiagonal = true;
            foreach (var ventLine in ventLinesInput)
            {
                var coordinates = ventsGrid.ParseLine(ventLine);
                ventsGrid.AddVentsLine(coordinates);
            }

            List<List<int>> grid = ventsGrid.getGrid();

            var positions = 0;
            foreach (var gridLine in grid)
            {
                positions += gridLine.Where(position => position >= 2).Count();
            }

            Assert.AreEqual(12, positions);
        }

        [TestMethod]
        public void TestVentsGridFullTestWithDiagonalLines()
        {
            string[] ventLinesInput = LoadTestFile("fullTest.txt");

            VentsGrid ventsGrid = new VentsGrid();
            foreach (var ventLine in ventLinesInput)
            {
                var coordinates = ventsGrid.ParseLine(ventLine);
                ventsGrid.AddVentsLine(coordinates);
            }

            List<List<int>> grid = ventsGrid.getGrid();

            var positions = 0;
            foreach (var gridLine in grid)
            {
                positions += gridLine.Where(position => position >= 2).Count();
            }

            Console.WriteLine(positions);
        }

        private string[] LoadTestFile(string path)
        {
            string[] input = System.IO.File.ReadAllLines(path);
            return input;
        }
    }
}
