using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace _5_Hydrothermal_Venture.Test
{
    [TestClass]
    public class VentsGridTest
    {
        [TestMethod]
        public void TestCreateVentsGrid()
        {
            VentsGrid ventsGrid = new VentsGrid();

            int[][] ventsGridList = ventsGrid.getGrid();

            int vents = 0;
            foreach (int[] row in ventsGridList)
                row.Where(amountOfVents => amountOfVents > 0);
            Assert.AreEqual(0, vents);
        }

        [TestMethod]
        public void TestParseLine()
        {
            VentsGrid ventsGrid = new VentsGrid();
            string line = "0,0 -> 0,9";

            var coordinates = ventsGrid.ParseLine(line);

            Assert.AreEqual(0, coordinates.startX);
            Assert.AreEqual(0, coordinates.startY);
            Assert.AreEqual(0, coordinates.endX);
            Assert.AreEqual(9, coordinates.endY);
        }

        /*[TestMethod]
        public void TestAddLineToVentsGrid()
        {
            VentsGrid ventsGrid = new VentsGrid();
            string line = "0,0 -> 0,9";

            ventsGrid.AddVentsLine(line);

            int[][] ventsGridList = ventsGrid.getGrid();
            int vents = 0;
            foreach (int[] row in ventsGridList)
                row.Where(amountOfVents => amountOfVents > 0);
            Assert.AreEqual(9, vents);
        }*/
    }
}
