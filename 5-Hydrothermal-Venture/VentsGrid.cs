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


            bool startXParsed = int.TryParse(startCoordinates[0], out int startX);
            bool startYParsed = int.TryParse(startCoordinates[1], out int startY);
            bool endXParsed = int.TryParse(endCoordinates[0], out int endX);
            bool endYParsed = int.TryParse(endCoordinates[1], out int endY);

            if (!(startXParsed || startYParsed || endXParsed || endYParsed)) throw new Exception();
            return (startX, startY, endX, endY);
        }

        public void AddVentsLine((int startX, int startY, int endX, int endY) coordinates)
        {
            if(LineIsStraigth(coordinates))
                coordinates = FlipLineToPositiveDirection(coordinates);

            if (LineIsHorizontal(coordinates))
            {
                AddHorizontalLine(coordinates);
            }
            else if (LineIsVertical(coordinates))
            {
                AddVerticalLine(coordinates);
            }
            else if (UseDiagonal)
            {
                AddDiagonalLine(coordinates);
            }   

        }

        private bool LineIsHorizontal((int startX, int startY, int endX, int endY) coordinates)
        {
            return (coordinates.startY == coordinates.endY) && (coordinates.startX != coordinates.endX);
        }

        private bool LineIsVertical((int startX, int startY, int endX, int endY) coordinates)
        {
            return (coordinates.startX == coordinates.endX) && (coordinates.startY != coordinates.endY);
        }

        public bool LineIsStraigth((int startX, int startY, int endX, int endY) coordinates)
        {
            return ((coordinates.startX == coordinates.endX) && (coordinates.startY != coordinates.endY)) ||
                ((coordinates.startY == coordinates.endY) && (coordinates.startX != coordinates.endX));
        }

        public bool IsMixedDiagonalLineGoingTowardsY((int startX, int startY, int endX, int endY) coordinates)
        {
            // 8,0 -> 0,8
            return ((coordinates.startX - coordinates.endX) == (coordinates.endY - coordinates.startY)) &&
                (coordinates.startX > coordinates.startY);
        }

        public bool IsMixedDiagonalLineGoingTowardsX((int startX, int startY, int endX, int endY) coordinates)
        {
            // 0,8 -> 8,0
            return ((coordinates.endX - coordinates.startX) == (coordinates.startY - coordinates.endY)) &&
                coordinates.startY >= coordinates.startX;
        }

        private void AddDiagonalLine((int startX, int startY, int endX, int endY) coordinates)
        {
            int positionsToPlace = coordinates.startX > coordinates.endX ? (coordinates.startX - coordinates.endX) : (coordinates.endX - coordinates.startX);

            for (int positionCounter = 0; positionCounter <= positionsToPlace; positionCounter++)
            {
                int xCoordinate;
                int yCoordinate;

                if (IsMixedDiagonalLineGoingTowardsX(coordinates))
                {
                    xCoordinate = coordinates.startX + positionCounter;
                    yCoordinate = coordinates.startY - positionCounter;
                } 
                else if(IsMixedDiagonalLineGoingTowardsY(coordinates))
                {
                    xCoordinate = coordinates.startX - positionCounter;
                    yCoordinate = coordinates.startY + positionCounter;
                } 
                else if(IsHomogenousPositiveDiagonalLine(coordinates))
                {
                    xCoordinate = coordinates.startX + positionCounter;
                    yCoordinate = coordinates.startY + positionCounter;
                } 
                else if(IsHomogenousNegativeDiagonalLine(coordinates))
                {
                    xCoordinate = coordinates.startX - positionCounter;
                    yCoordinate = coordinates.startY - positionCounter;
                }
                else
                {
                    throw new Exception();
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

        public bool IsHomogenousPositiveDiagonalLine((int startX, int startY, int endX, int endY) coordinates)
        {
            return ((coordinates.startX - coordinates.endX) == (coordinates.startY - coordinates.endY)) &&
                (coordinates.startX < coordinates.endX) && (coordinates.startY < coordinates.endY);
        }

        public bool IsHomogenousNegativeDiagonalLine((int startX, int startY, int endX, int endY) coordinates)
        {
            return ((coordinates.startX - coordinates.endX) == (coordinates.startY - coordinates.endY)) &&
                (coordinates.startX > coordinates.endX) && (coordinates.startY > coordinates.endY);
        }

        private (int startX, int startY, int endX, int endY) FlipDiagonalLine((int startX, int startY, int endX, int endY) coordinates)
        {
            return (coordinates.endX, coordinates.endY, coordinates.startX, coordinates.startY);
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
