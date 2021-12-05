using System;
using System.Collections.Generic;
using System.Linq;

namespace _4_Giant_Squid
{
    public class Bingo
    {
        public List<Board> Boards { get; private set; } = new List<Board>();

        private bool ActiveGame;
        public bool IgnoreGameState { get; set; } = false;
        public List<int> WinningBoardIndices { get; private set; } = new List<int>();

        public Bingo()
        {
            ActiveGame = true;
        }

        public void AddBoard(Board board)
        {
            Boards.Add(board);
        }

        public void AddMultipleBoards(List<Board> boards)
        {
            Boards = boards;
        }

        public void DrawNumber(string number)
        {
            foreach (var board in Boards)
            {
                if (ActiveGame || IgnoreGameState)
                {
                    board.CrossOutNumber(number);
                    if (board.HasBingo())
                    {
                        ActiveGame = false;
                        WinningBoardIndices.Add(Boards.IndexOf(board));
                    }
                }
            }
        }

        public void ContinueGame()
        {
            ActiveGame = true;
        }

        public bool GameIsRunning()
        {
            return ActiveGame;
        }

        public List<Board> GetWinningBoards()
        {
            List<Board> winningBoards = new List<Board>();
            foreach (var index in WinningBoardIndices)
                winningBoards.Add(Boards.ElementAt(index));
            return winningBoards;
        }

        public void RemoveBoard(int boardIndex)
        {
            Boards.RemoveAt(boardIndex);
        }

        public void ClearWinningBoardIndices()
        {
            WinningBoardIndices.Clear();
        }
    }
}
