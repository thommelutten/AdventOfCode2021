using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _4_Giant_Squid.Test
{
    [TestClass]
    public class BingoTest
    {
        [TestMethod]
        public void TestBingoAddBoard()
        {
            Bingo bingo = new Bingo();

            string[][] bingoBoardInput = {
                new string[] { "22", "13", "17", "11",  "0" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] { "1", "12", "20", "15", "19" }
            };

            Board board = new Board(bingoBoardInput);

            bingo.AddBoard(board);

            Assert.AreEqual(1, bingo.Boards.Count);
        }

        [TestMethod]
        public void TestBingoAddMultipleBoards()
        {
            Bingo bingo = new Bingo();

            string[][] bingoBoardInput = {
                new string[] { "22", "13", "17", "11",  "0" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] { "1", "12", "20", "15", "19" }
            };

            Board board = new Board(bingoBoardInput);

            bingo.AddBoard(board);
            bingo.AddBoard(board);

            Assert.AreEqual(2, bingo.Boards.Count);
        }


        [TestMethod]
        public void TestBingoDrawNumber()
        {
            Bingo bingo = new Bingo();

            bingo.AddBoard(CreateBingoBoard());

            bingo.DrawNumber("22");

            var board = bingo.Boards.First();
            var firstRow = board.GetRow(0);
            Assert.IsTrue(firstRow.Contains("x"));
        }

        [TestMethod]
        public void TestBingoGetGameRunning()
        {
            Bingo bingo = new Bingo();

            Assert.IsTrue(bingo.GameIsRunning());
        }

        [TestMethod]
        public void TestBingoGetWinningBoard()
        {
            Bingo bingo = new Bingo();
            bingo.AddBoard(CreateBingoBoard());

            Assert.IsTrue(bingo.GameIsRunning());

            bingo.DrawNumber("22");
            bingo.DrawNumber("13");
            bingo.DrawNumber("17");
            bingo.DrawNumber("11");
            bingo.DrawNumber("0");

            Assert.IsFalse(bingo.GameIsRunning());

            string[][] winningBoardExample = {
                new string[] {  "x",  "x",  "x",  "x",  "x" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] { "1", "12", "20", "15", "19" }
            };

            List<Board> winningBoards = bingo.GetWinningBoards();

            for (int rowIndex = 0; rowIndex < 5; rowIndex++)
            {
                string[] winningBoardRow = winningBoards.First().GetRow(rowIndex);
                for (int columnIndex = 0; columnIndex < 5; columnIndex++)
                    Assert.AreEqual(winningBoardRow[columnIndex], winningBoardExample[rowIndex][columnIndex]);
            }
        }

        [TestMethod]
        public void TestBingoRunSmallTestGame()
        {
            string[] gameFile = LoadGameFile("smallTest.txt");

            string[] drawnNumbers = gameFile[0].Split(',');

            List<Board> boards = CreateBoardsFromFile(gameFile);

            Bingo bingo = new Bingo();
            bingo.AddMultipleBoards(boards);
            foreach(var drawnNumber in drawnNumbers)
            {
                bingo.DrawNumber(drawnNumber);
                if (!bingo.GameIsRunning())
                {
                    List<Board> winningBoards = bingo.GetWinningBoards();

                    int sum = CalculateSumOfBoard(winningBoards.First());

                    int result = sum * int.Parse(drawnNumber);
                    Assert.AreEqual(4512, result);
                    Assert.AreEqual("24", drawnNumber);
                    break;
                }
            }
        }

        [TestMethod]
        public void TestBingoRunFullGame()
        {
            string[] gameFile = LoadGameFile("fullGame.txt");

            string[] drawnNumbers = gameFile[0].Split(',');

            List<Board> boards = CreateBoardsFromFile(gameFile);

            Bingo bingo = new Bingo();
            bingo.AddMultipleBoards(boards);

            foreach (var drawnNumber in drawnNumbers)
            {
                bingo.DrawNumber(drawnNumber);
                if (!bingo.GameIsRunning())
                {
                    List<Board> winningBoards = bingo.GetWinningBoards();

                    int sum = CalculateSumOfBoard(winningBoards.First());

                    int result = sum * int.Parse(drawnNumber);
                    Console.WriteLine(result);
                    break;
                }
            }
        }

        [TestMethod]
        public void TestBingoRunTestGamePrintLastBoardWin()
        {
            string[] gameFile = LoadGameFile("smallTest.txt");

            string[] drawnNumbers = gameFile[0].Split(',');

            List<Board> boards = CreateBoardsFromFile(gameFile);

            Bingo bingo = new Bingo();
            bingo.IgnoreGameState = true;
            bingo.AddMultipleBoards(boards);

            foreach (var drawnNumber in drawnNumbers)
            {
                bingo.DrawNumber(drawnNumber);

                if(!bingo.GameIsRunning())
                {
                    List<Board> winningBoards = bingo.GetWinningBoards();
                    foreach(var board in winningBoards)
                    {
                        int sum = CalculateSumOfBoard(board);
                        int result = sum * int.Parse(drawnNumber);
                        Console.WriteLine(result + ", " + drawnNumber);
                    }

                    foreach (var boardIndex in bingo.WinningBoardIndices)
                        bingo.RemoveBoard(boardIndex);

                    bingo.ClearWinningBoardIndices();
                    bingo.ContinueGame();
                }
                
            }            
        }

        [TestMethod]
        public void TestBingoRunFullGamePrintLastBoardWin()
        {
            string[] gameFile = LoadGameFile("fullGame.txt");

            string[] drawnNumbers = gameFile[0].Split(',');

            List<Board> boards = CreateBoardsFromFile(gameFile);

            Bingo bingo = new Bingo();
            bingo.IgnoreGameState = true;
            bingo.AddMultipleBoards(boards);

            foreach (var drawnNumber in drawnNumbers)
            {
                bingo.DrawNumber(drawnNumber);

                if (!bingo.GameIsRunning())
                {
                    List<Board> winningBoards = bingo.GetWinningBoards();
                    foreach (var board in winningBoards)
                    {
                        int sum = CalculateSumOfBoard(board);
                        int result = sum * int.Parse(drawnNumber);
                        Console.WriteLine(result + ", " + drawnNumber);
                    }

                    foreach (var boardIndex in bingo.WinningBoardIndices.OrderByDescending(number => number))
                        bingo.RemoveBoard(boardIndex);

                    bingo.ClearWinningBoardIndices();
                    bingo.ContinueGame();
                }

            }
        }

        private int CalculateSumOfBoard(Board board)
        {
            int totalSum = 0;
            for(int index = 0; index < 5; index++)
            {
                var row = board.GetRow(index);
                var numbers = row.Where(number => !number.Contains("x")).ToArray();

                foreach (var number in numbers)
                    totalSum += int.Parse(number);
            }
            return totalSum;
        }

        private List<Board> CreateBoardsFromFile(string[] gameFile)
        {
            List<Board> boards = new List<Board>();

            for (var lineIndex = 2; lineIndex < gameFile.Length-1; lineIndex += 6)
            {
                
                string[][] boardInput = new string[][] {
                ParseLine(gameFile[lineIndex]),
                ParseLine(gameFile[lineIndex + 1]),
                ParseLine(gameFile[lineIndex + 2]),
                ParseLine(gameFile[lineIndex + 3]),
                ParseLine(gameFile[lineIndex + 4])
                };


                boards.Add(
                    new Board(boardInput)
                );
            }

            return boards;
        }

        private string[] ParseLine(string line)
        {
            var lines = line.Replace("  ", " ").Split(' ');
            return lines.Where(number => !string.IsNullOrEmpty(number)).ToArray();
        }

        private string[] LoadGameFile(string path)
        {
            string[] input = System.IO.File.ReadAllLines(path);
            return input;
        }

        private Board CreateBingoBoard()
        {
            string[][] bingoBoardInput = {
                new string[] { "22", "13", "17", "11",  "0" },
                new string[] {  "8",  "2", "23",  "4", "24" },
                new string[] { "21",  "9", "14", "16",  "7" },
                new string[] {  "6", "10",  "3", "18",  "5" },
                new string[] { "1", "12", "20", "15", "19" }
            };

            return new Board(bingoBoardInput);
        }
    }
}
