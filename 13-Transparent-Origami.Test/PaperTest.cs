using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

        [TestMethod]
        public void TestPaperLoadInstructions()
        {
            Paper paper = new Paper();
            var dotsAndFoldingInstructions = LoadDotsAndFoldingInstructionsFromFile("smallTest.txt");
            paper.LoadFoldingInstructions(dotsAndFoldingInstructions.foldingInstructions);

            var foldingInstructions = paper.FoldingInstructions;
            Assert.AreEqual(2, foldingInstructions.Count);

            Assert.AreEqual("y", foldingInstructions[0].axis);
            Assert.AreEqual(7, foldingInstructions[0].lineNumber);

            Assert.AreEqual("x", foldingInstructions[1].axis);
            Assert.AreEqual(5, foldingInstructions[1].lineNumber);
        }

        [TestMethod]
        public void TestPaperFoldHorizontal()
        {
            Paper paper = InitializePaper("smallTest.txt");

            List<List<bool>> lines = paper.Lines;
            var dotsOnRow = lines[0].Where(isMarked => isMarked).Count();
            Assert.AreEqual(3, dotsOnRow);

            paper.FoldHorizontalAt(7);

            lines = paper.Lines;
            dotsOnRow = lines[0].Where(isMarked => isMarked).Count();
            Assert.AreEqual(5, dotsOnRow);
            Assert.IsTrue(lines[0][0]);
            Assert.IsTrue(lines[0][2]);
            Assert.IsTrue(lines[0][3]);
            Assert.IsTrue(lines[0][6]);
            Assert.IsTrue(lines[0][9]);

            dotsOnRow = lines[1].Where(isMarked => isMarked).Count();
            Assert.AreEqual(2, dotsOnRow);
            Assert.IsTrue(lines[1][0]);
            Assert.IsTrue(lines[1][4]);

            dotsOnRow = lines[2].Where(isMarked => isMarked).Count();
            Assert.AreEqual(2, dotsOnRow);
            Assert.IsTrue(lines[2][6]);
            Assert.IsTrue(lines[2][10]);

            dotsOnRow = lines[3].Where(isMarked => isMarked).Count();
            Assert.AreEqual(2, dotsOnRow);
            Assert.IsTrue(lines[3][0]);
            Assert.IsTrue(lines[3][4]);
            
            dotsOnRow = lines[4].Where(isMarked => isMarked).Count();
            Assert.AreEqual(6, dotsOnRow);
            Assert.IsTrue(lines[4][1]);
            Assert.IsTrue(lines[4][3]);
            Assert.IsTrue(lines[4][6]);
            Assert.IsTrue(lines[4][8]);
            Assert.IsTrue(lines[4][9]);
            Assert.IsTrue(lines[4][10]);


            Assert.AreEqual(7, lines.Count);
        }

        [TestMethod]
        public void TestPaperFoldVertical()
        {
            Paper paper = InitializePaper("smallTest.txt");
            paper.FoldHorizontalAt(7);
            paper.FoldVerticalAt(5);

            List<List<bool>> lines = paper.Lines;
            var dotsOnRow = lines[0].Where(isMarked => isMarked).Count();
            Assert.AreEqual(5, dotsOnRow);
            Assert.IsTrue(lines[0][0]);
            Assert.IsTrue(lines[0][1]);
            Assert.IsTrue(lines[0][2]);
            Assert.IsTrue(lines[0][3]);
            Assert.IsTrue(lines[0][4]);
            

            dotsOnRow = lines[1].Where(isMarked => isMarked).Count();
            Assert.AreEqual(2, dotsOnRow);

            dotsOnRow = lines[2].Where(isMarked => isMarked).Count();
            Assert.AreEqual(2, dotsOnRow);

            dotsOnRow = lines[3].Where(isMarked => isMarked).Count();
            Assert.AreEqual(2, dotsOnRow);

            dotsOnRow = lines[4].Where(isMarked => isMarked).Count();
            Assert.AreEqual(5, dotsOnRow);
        }

        [TestMethod]
        public void TestPaperFold()
        {
            Paper paper = InitializePaper("smallTest.txt");

            List<List<bool>> lines = paper.Lines;
            var dotsOnFirstRow = lines[0].Where(isMarked => isMarked).Count();
            Assert.AreEqual(3, dotsOnFirstRow);

            paper.Fold();

            lines = paper.Lines;
            dotsOnFirstRow = lines[0].Where(isMarked => isMarked).Count();
            Assert.AreEqual(5, dotsOnFirstRow);
        }

        [TestMethod]
        public void TestPaperReverseLine()
        {
            List<bool> line = new List<bool> { false, false, true };

            Paper paper = new Paper();

            var reversedLine = paper.ReverseLine(line);

            Assert.IsTrue(line[0]);
            Assert.IsFalse(line[1]);
            Assert.IsFalse(line[2]);
        }

        [TestMethod]
        public void TestPaperReverseLines()
        {
            Paper paper = new Paper();

            List<bool> line = new List<bool> { false, false, true };
            List<bool> line2 = new List<bool> { true, false, true };
            List<List<bool>> lines = new List<List<bool>>() { line, line2 };

            lines = paper.ReverseLines(lines);

            Assert.IsTrue(lines[0][0]);
            Assert.IsFalse(lines[0][1]);
            Assert.IsFalse(lines[0][2]);

            Assert.IsTrue(lines[1][0]);
            Assert.IsFalse(lines[1][1]);
            Assert.IsTrue(lines[1][2]);
        }

        [TestMethod]
        public void TestPaperFoldAllInstructions()
        {
            Paper paper = InitializePaper("smallTest.txt");
            paper.FoldAllInstructions();

            List<List<bool>> lines = paper.Lines;
            var dotsOnRow = lines[0].Where(isMarked => isMarked).Count();
            Assert.AreEqual(5, dotsOnRow);

            dotsOnRow = lines[1].Where(isMarked => isMarked).Count();
            Assert.AreEqual(2, dotsOnRow);

            dotsOnRow = lines[2].Where(isMarked => isMarked).Count();
            Assert.AreEqual(2, dotsOnRow);

            dotsOnRow = lines[3].Where(isMarked => isMarked).Count();
            Assert.AreEqual(2, dotsOnRow);

            dotsOnRow = lines[4].Where(isMarked => isMarked).Count();
            Assert.AreEqual(5, dotsOnRow);
        }

        [TestMethod]
        public void TestPaperCountDots()
        {
            Paper paper = InitializePaper("smallTest.txt");
            paper.FoldAllInstructions();

            int dots = paper.CountDots();

            Assert.AreEqual(16, dots);
        }

        [TestMethod]
        public void TestPaperSingleFoldCountDotsFullTest()
        {
            Paper paper = InitializePaper("fullTest.txt");
            paper.Fold();

            int dots = paper.CountDots();

            Console.WriteLine(dots);
        }

        [TestMethod]
        public void TestPaperPrintCode()
        {
            Paper paper = InitializePaper("smallTest.txt");
            paper.FoldAllInstructions();
            List<string> code = paper.DecipherCode();
            Assert.AreEqual("#####", code[0]);
            Assert.AreEqual("#      #", code[1]);
            Assert.AreEqual("#      #", code[2]);
            Assert.AreEqual("#      #", code[3]);
            Assert.AreEqual("#####", code[4]);
            Assert.AreEqual("          ", code[5]);
            Assert.AreEqual("          ", code[6]);

            foreach (var decipheredLine in code)
                Console.WriteLine(decipheredLine);
        }

        [TestMethod]
        public void TestPaperDecipherCodeFullTest()
        {
            Paper paper = InitializePaper("fullTest.txt");
            paper.FoldAllInstructions();

            List<string> decipheredLines = paper.DecipherCode();

            foreach (var decipheredLine in decipheredLines)
                Console.WriteLine(decipheredLine);
        }

        private Paper InitializePaper(string path)
        {
            Paper paper = new Paper();
            var dotsAndFoldingInstructions = LoadDotsAndFoldingInstructionsFromFile(path);
            paper.LoadDots(dotsAndFoldingInstructions.dots);
            paper.LoadFoldingInstructions(dotsAndFoldingInstructions.foldingInstructions);
            return paper;
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
