using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace _13_Transparent_Origami.Test
{
    [TestClass]
    public class PaperTest
    {
        [TestMethod]
        public void TestPaperCreate()
        {
            Paper paper = new Paper();
            Assert.IsNotNull(paper);
        }

        [TestMethod]
        public void TestPaperGetLines()
        {
            Paper paper = new Paper();
            List<List<bool>> lines = paper.Lines;

            Assert.AreEqual(0, lines.Count);
        }

        [TestMethod]
        public void TestPaperLoadDots()
        {
            Paper paper = new Paper();
            var dotsAndFoldingInstructions = LoadDotsAndFoldingInstructionsFromFile("smallTest.txt");

            paper.LoadDots(dotsAndFoldingInstructions.dots);

            List<List<bool>> lines = paper.Lines;

            var dotsOnFirstRow = lines[0].Where(isMarked => isMarked).Count();

            Assert.AreEqual(3, dotsOnFirstRow);
        }

        private (List<List<bool>> dots, List<(string axis, int lineNumber)> foldingInstructions) LoadDotsAndFoldingInstructionsFromFile(string path)
        {
            string[] fileLines = System.IO.File.ReadAllLines(path);

            List<List<bool>> dots = new List<List<bool>>();
            List<(string axis, int lineNumber)> foldingInstructions = new List<(string axis, int lineNumber)>();

            foreach(var fileLine in fileLines)
            {
                if (string.IsNullOrWhiteSpace(fileLine))
                    continue;

                if(fileLine.Contains("fold along"))
                {
                    var lineParts = fileLine.Split(" ");
                    var axisAndLineNumber = lineParts[2].Split("=");
                    foldingInstructions.Add((axisAndLineNumber[0], int.Parse(axisAndLineNumber[1])));
                }else
                {
                    var lineParts = fileLine.Split(",");
                    var columnNumber = int.Parse(lineParts[0]);
                    var rowNumber = int.Parse(lineParts[1]);

                    if (dots.Count <= rowNumber)
                    {
                        while (dots.Count <= rowNumber)
                            dots.Add(new List<bool>());
                    }
                        

                    if (dots[rowNumber].Count <= columnNumber)
                        while (dots[rowNumber].Count <= columnNumber)
                            dots[rowNumber].Add(false);

                    dots[rowNumber][columnNumber] = true;
                }
            }
            return (dots, foldingInstructions);

        }
    }
}
