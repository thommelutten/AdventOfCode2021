using System;
using System.Collections.Generic;
using System.Text;

namespace _5_Hydrothermal_Venture
{
    public class VentsGrid
    {
        private List<List<int>> Grid = new List<List<int>>();

        public bool UseDiagonal { get; set; }

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
            
            coordinates = FlipLineToPositiveDirection(coordinates);

            if (coordinates.startY == coordinates.endY)
            {
                AddHorizontalLine(coordinates);
            }
            else if (coordinates.startX == coordinates.endX)
            {
                AddVerticalLine(coordinates);
            }
            else if (UseDiagonal)
            {
                AddDiagonalLine(coordinates);
            }   

        }

        private bool LineNotDiagonal((int startX, int startY, int endX, int endY) coordinates)
        {
            return !(((coordinates.startX == coordinates.startY) && (coordinates.endX == coordinates.endY)) ||
                ((coordinates.startX == coordinates.endY) && (coordinates.endX == coordinates.startY)));
        }

        private void AddDiagonalLine((int startX, int startY, int endX, int endY) coordinates)
        {
            int positionsToPlace = coordinates.startX > coordinates.endX ? (coordinates.startX - coordinates.endX) : (coordinates.endX - coordinates.startX);

            bool backwardsDiagonal = false;
            if ((coordinates.endX < coordinates.startX) && (coordinates.endY < coordinates.startY))
                backwardsDiagonal = true;

            for (int positionCounter = 0; positionCounter <= positionsToPlace; positionCounter++)
            {
                int xCoordinate;
                int yCoordinate;
                if(backwardsDiagonal)
                {
                    if(coordinates.endX == coordinates.endY)
                    {
                        xCoordinate = coordinates.startX - positionCounter;
                        yCoordinate = coordinates.startY - positionCounter;
                    }
                    else
                    {
                        xCoordinate = coordinates.startX + positionCounter;
                        yCoordinate = coordinates.startY + positionCounter;
                    }
                    
                } else
                {
                    if(coordinates.startX < coordinates.endX)
                    {
                        xCoordinate = coordinates.startX + positionCounter;
                        yCoordinate = coordinates.startY + positionCounter;
                    } else
                    {
                        xCoordinate = coordinates.startX - positionCounter;
                        yCoordinate = coordinates.startY + positionCounter;
                    }
                    
                }

                if (!VerticalLineExist(yCoordinate))
                    CreateVerticalLines(yCoordinate);

                if (!HorizontalLineExist(yCoordinate, xCoordinate))
                    CreateHorizontalPositions(yCoordinate, xCoordinate);

                Grid[yCoordinate][xCoordinate] += 1;
            }
        }

        public (int startX, int startY, int endX, int endY) FlipLineToPositiveDirection((int startX, int startY, int endX, int endY) coordinates)
        {

            if(LineIsDiagonal(coordinates))
            {
                return FlipDiagonalLine(coordinates);
            }

            if (coordinates.startY < coordinates.startX &&
                coordinates.startX == coordinates.endY &&
                coordinates.startY == coordinates.endX)
            {
                return (coordinates.endY, coordinates.startY, coordinates.endX, coordinates.startX);
            }

            if (coordinates.startX > coordinates.endX)
            {
                return (coordinates.endX, coordinates.endY, coordinates.startX, coordinates.startY);
            }

            if(coordinates.startY > coordinates.endY)
            {
                return (coordinates.endX, coordinates.endY, coordinates.startX, coordinates.startY);
            }

            return coordinates;
        }

        private (int startX, int startY, int endX, int endY) FlipDiagonalLine((int startX, int startY, int endX, int endY) coordinates)
        {
            return (coordinates.endX, coordinates.endY, coordinates.startX, coordinates.startY);
        }

        private bool LineIsDiagonal((int startX, int startY, int endX, int endY) coordinates)
        {
            bool isDiagonal = false;

            // same coords
            if (
                (coordinates.startX == coordinates.startY) && (coordinates.endX == coordinates.endY)
                )
                isDiagonal = true;

            if (
                (coordinates.startX - coordinates.endX) == (coordinates.startY - coordinates.endY) ||
                (coordinates.endX - coordinates.startX) == (coordinates.endY - coordinates.startY)
                )
                isDiagonal = true;

            return isDiagonal;
        }

        private bool LineNegativeDescend((int startX, int startY, int endX, int endY) coordinates)
        {
            return (coordinates.startX > coordinates.endX || coordinates.startY > coordinates.endY);
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

            for (int index = coordinates.startX; index <= coordinates.endX; index++)
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
            return (Grid[endY].Count - 1) >= endX;
        }

        private void CreateVerticalLines(int endY)
        {
            var amountOfLinesToAdd = endY - Grid.Count;
            for (var indexToAdd = 0; indexToAdd <= amountOfLinesToAdd; indexToAdd++)
                Grid.Add(new List<int>());
        }

        private bool VerticalLineExist(int endY)
        {
            return (Grid.Count - 1) >= endY;
        }
    }
}
