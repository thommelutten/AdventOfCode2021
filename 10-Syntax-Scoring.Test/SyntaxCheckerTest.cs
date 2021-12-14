using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _10_Syntax_Scoring.Test
{
    [TestClass]
    public class SyntaxCheckerTest
    {
        [TestMethod]
        public void TestCreateSyntaxChecker()
        {
            SyntaxChecker syntaxChecker = new SyntaxChecker();
            Assert.IsNotNull(syntaxChecker);
        }

        [TestMethod]
        public void TestSyntaxCheckerLoadFile()
        {
            SyntaxChecker syntaxChecker = new SyntaxChecker();

            syntaxChecker.LoadFile(LoadFileLinesFromFile("smallTest.txt"));

            string[] fileLines = syntaxChecker.FileLines;

            Assert.AreEqual(10, fileLines.Length);
        }

        [TestMethod]
        public void TestSyntaxCheckerCheckLineReturnIllegalCharacter()
        {
            SyntaxChecker syntaxChecker = new SyntaxChecker();

            syntaxChecker.LoadFile(LoadFileLinesFromFile("smallTest.txt"));

            char illegalCharacter = syntaxChecker.CheckLine(2);

            Assert.AreEqual('}', illegalCharacter);
        }

        [TestMethod]
        public void TestSyntaxCheckerCheckLineReturn0()
        {
            SyntaxChecker syntaxChecker = new SyntaxChecker();

            syntaxChecker.LoadFile(LoadFileLinesFromFile("smallTest.txt"));

            char illegalCharacter = syntaxChecker.CheckLine(0);

            Assert.AreEqual('0', illegalCharacter);
        }

        [TestMethod]
        public void TestSyntaxCheckerCalculateScore()
        {
            SyntaxChecker syntaxChecker = new SyntaxChecker();

            syntaxChecker.LoadFile(LoadFileLinesFromFile("smallTest.txt"));
            int score = syntaxChecker.CalculateScore();

            Assert.AreEqual(26397, score);
        }

        [TestMethod]
        public void TestSyntaxCheckerCalculateScoreFullTest()
        {
            SyntaxChecker syntaxChecker = new SyntaxChecker();

            syntaxChecker.LoadFile(LoadFileLinesFromFile("fullTest.txt"));
            int score = syntaxChecker.CalculateScore();

            Console.WriteLine(score);
        }


        private string[] LoadFileLinesFromFile(string path)
        {
            string[] fileLines = System.IO.File.ReadAllLines(path);
            return fileLines;
        }
    }
}
