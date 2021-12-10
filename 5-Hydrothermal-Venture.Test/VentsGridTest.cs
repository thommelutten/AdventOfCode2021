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
        public void TestVentsGridFlipLineToPositiveHighestX()
        {
            VentsGrid ventsGrid = new VentsGrid();

            string line = "0,8 -> 8,0";
            var coordinates = ventsGrid.ParseLine(line);

            coordinates = ventsGrid.FlipLineToPositiveDirection(coordinates);
            Assert.AreEqual(8, coordinates.startX);
            Assert.AreEqual(0, coordinates.startY);
            Assert.AreEqual(0, coordinates.endX);
            Assert.AreEqual(8, coordinates.endY);
        }

        [TestMethod]
        public void TestVentsGridFlipLineToPositiveHighestX2064()
        {
            VentsGrid ventsGrid = new VentsGrid();

            string line = "6,4 -> 2,0";
            var coordinates = ventsGrid.ParseLine(line);

            coordinates = ventsGrid.FlipLineToPositiveDirection(coordinates);
            Assert.AreEqual(2, coordinates.startX);
            Assert.AreEqual(0, coordinates.startY);
            Assert.AreEqual(6, coordinates.endX);
            Assert.AreEqual(4, coordinates.endY);
        }

        [TestMethod]
        public void TestVentsGridFlipLineToPositiveHighestY()
        {
            VentsGrid ventsGrid = new VentsGrid();

            string line = "8,0 -> 0,8";
            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);
            
            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[0].Sum());
            Assert.AreEqual(1, grid[1].Sum());
            Assert.AreEqual(1, grid[2].Sum());
            Assert.AreEqual(1, grid[3].Sum());
            Assert.AreEqual(1, grid[4].Sum());
            Assert.AreEqual(1, grid[5].Sum());
            Assert.AreEqual(1, grid[6].Sum());
            Assert.AreEqual(1, grid[7].Sum());
            Assert.AreEqual(1, grid[8].Sum());
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
        public void TestVentsGridFullTestUseDiagonal()
        {
            string[] ventLinesInput = LoadTestFile("fullTest.txt");

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

            Console.WriteLine(positions);
        }

        [TestMethod]
        public void TestVentsGrid0959Line()
        {
            string line = "0,9 -> 5,9";
            VentsGrid ventsGrid = new VentsGrid();

            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);

            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[9][5]);
            Assert.AreEqual(1, grid[9][4]);
            Assert.AreEqual(1, grid[9][3]);
            Assert.AreEqual(1, grid[9][2]);
            Assert.AreEqual(1, grid[9][1]);
            Assert.AreEqual(1, grid[9][0]);
        }

        [TestMethod]
        public void TestVentsGrid8008Line()
        {
            string line = "8,0 -> 0,8";
            VentsGrid ventsGrid = new VentsGrid();

            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);

            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[8][0]);
            Assert.AreEqual(1, grid[7][1]);
            Assert.AreEqual(1, grid[6][2]);
            Assert.AreEqual(1, grid[5][3]);
            Assert.AreEqual(1, grid[4][4]);
            Assert.AreEqual(1, grid[3][5]);
            Assert.AreEqual(1, grid[2][6]);
            Assert.AreEqual(1, grid[1][7]);
            Assert.AreEqual(1, grid[0][8]);
        }

        [TestMethod]
        public void TestVentsGrid2221Line()
        {
            string line = "2,2 -> 2,1";
            VentsGrid ventsGrid = new VentsGrid();

            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);

            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[1][2]);
            Assert.AreEqual(1, grid[2][2]);
        }

        [TestMethod]
        public void TestVentsGrid7074Line()
        {
            string line = "7,0 -> 7,4";
            VentsGrid ventsGrid = new VentsGrid();

            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);

            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[0][7]);
            Assert.AreEqual(1, grid[1][7]);
            Assert.AreEqual(1, grid[2][7]);
            Assert.AreEqual(1, grid[3][7]);
            Assert.AreEqual(1, grid[4][7]);
        }

        [TestMethod]
        public void TestVentsGrid6420Line()
        {
            string line = "6,4 -> 2,0";
            VentsGrid ventsGrid = new VentsGrid();

            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);

            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[0][2]);
            Assert.AreEqual(1, grid[1][3]);
            Assert.AreEqual(1, grid[2][4]);
            Assert.AreEqual(1, grid[3][5]);
            Assert.AreEqual(1, grid[4][6]);
        }

        [TestMethod]
        public void TestVentsGrid9434Line()
        {
            string line = "9,4 -> 3,4";
            VentsGrid ventsGrid = new VentsGrid();

            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);

            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[4][3]);
            Assert.AreEqual(1, grid[4][4]);
            Assert.AreEqual(1, grid[4][5]);
            Assert.AreEqual(1, grid[4][6]);
            Assert.AreEqual(1, grid[4][7]);
            Assert.AreEqual(1, grid[4][8]);
            Assert.AreEqual(1, grid[4][9]);
        }

        [TestMethod]
        public void TestVentsGrid0929Line()
        {
            string line = "0,9 -> 2,9";
            VentsGrid ventsGrid = new VentsGrid();

            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);

            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[9][0]);
            Assert.AreEqual(1, grid[9][1]);
            Assert.AreEqual(1, grid[9][2]);
        }

        [TestMethod]
        public void TestVentsGrid3414Line()
        {
            string line = "3,4 -> 1,4";
            VentsGrid ventsGrid = new VentsGrid();

            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);

            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[4][1]);
            Assert.AreEqual(1, grid[4][2]);
            Assert.AreEqual(1, grid[4][3]);
        }

        [TestMethod]
        public void TestVentsGrid0088Line()
        {
            string line = "0,0 -> 8,8";
            VentsGrid ventsGrid = new VentsGrid();

            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);

            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[0][0]);
            Assert.AreEqual(1, grid[1][1]);
            Assert.AreEqual(1, grid[2][2]);
            Assert.AreEqual(1, grid[3][3]);
            Assert.AreEqual(1, grid[4][4]);
            Assert.AreEqual(1, grid[5][5]);
            Assert.AreEqual(1, grid[6][6]);
            Assert.AreEqual(1, grid[7][7]);
            Assert.AreEqual(1, grid[8][8]);
        }

        [TestMethod]
        public void TestVentsGrid5582Line()
        {
            string line = "5,5 -> 8,2";
            VentsGrid ventsGrid = new VentsGrid();

            var coordinates = ventsGrid.ParseLine(line);
            ventsGrid.UseDiagonal = true;
            ventsGrid.AddVentsLine(coordinates);

            var grid = ventsGrid.getGrid();
            Assert.AreEqual(1, grid[5][5]);
            Assert.AreEqual(1, grid[4][6]);
            Assert.AreEqual(1, grid[3][7]);
            Assert.AreEqual(1, grid[2][8]);
        }


        private string[] LoadTestFile(string path)
        {
            string[] input = System.IO.File.ReadAllLines(path);
            return input;
        }
    }
}
