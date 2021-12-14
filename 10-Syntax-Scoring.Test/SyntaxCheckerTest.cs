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

            List<string> fileLines = syntaxChecker.FileLines;

            Assert.AreEqual(10, fileLines.Count);
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

        [TestMethod]
        public void TestSyntaxCheckerDiscardIncompleteLines()
        {
            SyntaxChecker syntaxChecker = new SyntaxChecker();

            syntaxChecker.LoadFile(LoadFileLinesFromFile("smallTest.txt"));
            syntaxChecker.DiscardIncompleteLines();

            Assert.AreEqual(5, syntaxChecker.FileLines.Count);
        }

        [TestMethod]
        public void TestSyntaxCheckerCalculateMissingCharacters()
        {
            SyntaxChecker syntaxChecker = new SyntaxChecker();

            syntaxChecker.LoadFile(LoadFileLinesFromFile("smallTest.txt"));
            syntaxChecker.DiscardIncompleteLines();

            List<char> missingCharacters = syntaxChecker.CalculateMissingCharacters(0);

            List<char> correctMissingCharacters = new List<char> { '}', '}', ']', ']', ')', '}', ')', ']' };

            for (int index = 0; index < missingCharacters.Count; index++)
                Assert.AreEqual(correctMissingCharacters[index], missingCharacters[index]);
        }

        [TestMethod]
        public void TestSyntaxCheckerCalculateMissingCharacterScore()
        {
            SyntaxChecker syntaxChecker = new SyntaxChecker();

            syntaxChecker.LoadFile(LoadFileLinesFromFile("smallTest.txt"));
            syntaxChecker.DiscardIncompleteLines();

            long score = syntaxChecker.CalculateMissingCharactersMiddleScore();

            Assert.AreEqual(288957, score);
        }

        [TestMethod]
        public void TestSyntaxCheckerCalculateMissingCharacterScoreFullTest()
        {
            SyntaxChecker syntaxChecker = new SyntaxChecker();

            syntaxChecker.LoadFile(LoadFileLinesFromFile("fullTest.txt"));
            syntaxChecker.DiscardIncompleteLines();

            long score = syntaxChecker.CalculateMissingCharactersMiddleScore();

            Console.WriteLine(score);
        }


        private string[] LoadFileLinesFromFile(string path)
        {
            string[] fileLines = System.IO.File.ReadAllLines(path);
            return fileLines;
        }
    }
}
