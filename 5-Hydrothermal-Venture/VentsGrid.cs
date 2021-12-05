using System;
using System.Collections.Generic;
using System.Text;

namespace _5_Hydrothermal_Venture
{
    public class VentsGrid
    {
        private int[][] Grid = new int[][] { };
        public void AddVentsLine(string line)
        {      
        }

        public int[][] getGrid()
        {
            return Grid;
        }

        public (int startX, int startY, int endX, int endY) ParseLine(string line)
        {
            string[] parts = line.Split(' ');
            string[] startCoordinates = parts[0].Split(',');
            string[] endCoordinates = parts[2].Split(',');

            int startX = int.Parse(startCoordinates[0]);
            int startY = int.Parse(startCoordinates[1]);
            int endX = int.Parse(endCoordinates[0]);
            int endY = int.Parse(endCoordinates[1]);

            return (startX, startY, endX, endY);
        }
    }
}
