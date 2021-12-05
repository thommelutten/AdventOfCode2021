using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4_Giant_Squid.Test
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void TestCreateBoard()
        {
            string[][] bingoBoardInput = {
                new string[] { "22", "13", "17", "11",  "0" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] {  "1", "12", "20", "15", "19" }
            };

            Board board = new Board(bingoBoardInput);

            Assert.IsNotNull(board);
        }


        [TestMethod]
        public void TestGetRow()
        {
            string[][] bingoBoardInput = {
                new string[] { "22", "13", "17", "11",  "0" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] {  "1", "12", "20", "15", "19" }
            };

            Board board = new Board(bingoBoardInput);

            string[] row = board.GetRow(0);

            for (int rowIndex = 0; rowIndex < row.Length; rowIndex++)
                Assert.AreEqual(bingoBoardInput[0][rowIndex], row[rowIndex]);
        }

        [TestMethod]
        public void TestUpdateRow()
        {
            string[][] bingoBoardInput = {
                new string[] { "22", "13", "17", "11",  "0" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] {  "1", "12", "20", "15", "19" }
            };

            Board board = new Board(bingoBoardInput);
            
            string[] row = board.GetRow(0);

            Assert.AreEqual("22", row[0]);

            row[0] = "x";

            board.UpdateRow(0, row);

            string[] updatedRow = board.GetRow(0);

            Assert.AreEqual("x", updatedRow[0]);
        }

        [TestMethod]
        public void TestCrossOutNumber()
        {
            string[][] bingoBoardInput = {
                new string[] { "22", "13", "17", "11",  "0" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] {  "1", "12", "20", "15", "19" }
            };

            Board board = new Board(bingoBoardInput);

            board.CrossOutNumber("22");

            var row = board.GetRow(0);

            Assert.IsFalse(row.Any(number => number.Equals("22")));
        }

        [TestMethod]
        public void TestGetColumn()
        {
            string[][] bingoBoardInput = {
                new string[] { "22", "13", "17", "11",  "0" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] {  "1", "12", "20", "15", "19" }
            };

            Board board = new Board(bingoBoardInput);

            string[] column = board.GetColumn(0);

            for (int rowIndex = 0; rowIndex < column.Length; rowIndex++)
                Assert.AreEqual(bingoBoardInput[rowIndex][0], column[rowIndex]);
        }
        
        [TestMethod]
        public void TestHasBingoReturnFalse()
        {
            string[][] bingoBoardInput = {
                new string[] { "22", "13", "17", "11",  "0" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] {  "1", "12", "20", "15", "19" }
            };

            Board board = new Board(bingoBoardInput);

            bool boardHasBingo = board.HasBingo();

            Assert.IsFalse(boardHasBingo);
        }

        [TestMethod]
        public void TestHasBingoReturnTrueVertical()
        {
            string[][] bingoBoardInput = {
                new string[] {  "x", "13", "17", "11",  "0" },
                new string[] {  "x",  "2", "23",  "4", "24" },
                new string[] {  "x",  "9", "14", "16",  "7" },
                new string[] {  "x", "10",  "3", "18",  "5" },
                new string[] {  "x", "12", "20", "15", "19" }
            };

            Board board = new Board(bingoBoardInput);

            bool boardHasBingo = board.HasBingo();

            Assert.IsTrue(boardHasBingo);
        }

        [TestMethod]
        public void TestHasBingoReturnTrueHorizontal()
        {
            string[][] bingoBoardInput = {
                new string[] {  "x",  "x",  "x",  "x",  "x" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] {  "1", "12", "20", "15", "19" }
            };

            Board board = new Board(bingoBoardInput);

            bool boardHasBingo = board.HasBingo();

            Assert.IsTrue(boardHasBingo);
        }

    }
}
