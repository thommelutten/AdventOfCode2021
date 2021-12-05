using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4_Giant_Squid
{
    public class Board
    {
        string[][] rows;

        public Board(string[][] boardInput)
        {
            rows = boardInput;
        }

        public string[] GetRow(int index)
        {
            return rows[index];
        }

        public void UpdateRow(int index, string[] row)
        {
            rows[index] = row;
        }

        public string[] GetColumn(int columnIndex)
        {
            var column = new string[5];
            for(int rowIndex = 0; rowIndex < 5; rowIndex++)
                column[rowIndex] = rows[rowIndex][columnIndex];

            return column;
        }

        public bool HasBingo()
        {
            string[] row;
            string[] column;

            for(int index = 0; index < 5; index++)
            {
                row = GetRow(index);
                column = GetColumn(index);
                var rowHasBingo = row.All(number => number.Contains("x"));
                var columnHasBingo = column.All(number => number.Contains("x"));
                if (rowHasBingo || columnHasBingo)
                    return true;
            }

            return false;
        }

        public void CrossOutNumber(string drawnNumber)
        {
            for (int rowIndex = 0; rowIndex < 5; rowIndex++)
            {
                var row = GetRow(rowIndex);

                int numberIndex = Array.FindIndex(row, number => number == drawnNumber);

                if (numberIndex != -1)
                {
                    row[numberIndex] = "x";
                    UpdateRow(rowIndex, row);
                }
                    
            }
        }
    }
}
