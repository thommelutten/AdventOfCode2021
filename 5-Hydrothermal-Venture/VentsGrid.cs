using System;
using System.Collections.Generic;
using System.Text;

namespace _5_Hydrothermal_Venture
{
    public class VentsGrid
    {
        private List<List<int>> Grid = new List<List<int>>();

        public List<List<int>> getGrid()
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

        public void AddVentsLine((int startX, int startY, int endX, int endY) coordinates)
        {
            if (coordinates.startY == coordinates.endY)
                AddHorizontalLine(coordinates);

            if (coordinates.startX == coordinates.endX)
                AddVerticalLine(coordinates);

        }

        private void AddVerticalLine((int startX, int startY, int endX, int endY) coordinates)
        {
            if (!VerticalLineExist(coordinates.endY))
                CreateVerticalLines(coordinates.endY);

            

            for (int index = coordinates.startY; index <= coordinates.endY; index++)
            {
                if (!HorizontalLineExist(index, coordinates.endX))
                    CreateHorizontalPositions(index, coordinates.endX);
                Grid[index][coordinates.endX] += 1;
            }
        }

        private void AddHorizontalLine((int startX, int startY, int endX, int endY) coordinates)
        {
            if (!VerticalLineExist(coordinates.endY))
                CreateVerticalLines(coordinates.endY);

            if(!HorizontalLineExist(coordinates.endY, coordinates.endX))
                CreateHorizontalPositions(coordinates.endY, coordinates.endX);

            for (int index = coordinates.startX; index < coordinates.endX; index++)
                Grid[coordinates.startY][index] += 1;
        }

        private void CreateHorizontalPositions(int index, int endX)
        {
            var amountOfPositionsToAdd = endX - Grid[index].Count;
            for (var indexToAdd = 0; indexToAdd <= amountOfPositionsToAdd; indexToAdd++)
                Grid[index].Add(0);
        }

        private bool HorizontalLineExist(int endY, int endX)
        {
            return Grid[endY].Count - 1 >= endX;
        }

        private void CreateVerticalLines(int endY)
        {
            var amountOfLinesToAdd = endY - Grid.Count;
            for (var indexToAdd = 0; indexToAdd <= amountOfLinesToAdd; indexToAdd++)
                Grid.Add(new List<int>());
        }

        private bool VerticalLineExist(int endY)
        {
            return Grid.Count-1 >= endY;
        }
    }
}
