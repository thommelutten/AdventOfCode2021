using System;
using System.Collections.Generic;
using System.Text;

namespace _9_Smoke_Basin
{
    public class Heightmap
    {
        public List<List<int>> Map { get; private set; }

        public void LoadMap(string[] mapInput)
        {
            Map = ParseMap(mapInput);
        }

        private List<List<int>> ParseMap(string[] mapInput)
        {
            List<List<int>> map = new List<List<int>>();
            foreach(var mapRow in mapInput)
            {
                List<int> parsedRow = new List<int>();
                foreach (var point in mapRow)
                    parsedRow.Add(int.Parse(point.ToString()));
                map.Add(parsedRow);
            }
            return map;
        }

        public List<int> GetRow(int index)
        {
            return Map[index];
        }

        public List<int> FindLowPointsFirstLine()
        {
            var firstRow = GetRow(0);
            var secondRow = GetRow(1);

            List<int> lowPoints = FindLowPointsInFirstRowWithTwoRows(firstRow, secondRow);

            return lowPoints;
        }

        private bool FirstNumberIsSmallerThanRest(int firstNumber, List<int> numbers)
        {
            foreach(var number in numbers)
            {
                if (number <= firstNumber) return false;
            }
            return true;
        }

        public List<int> FindLowPointsLastLine()
        {
            var lastRow = GetRow(Map.Count - 1);
            var secondToLastRow = GetRow(Map.Count - 2);

            List<int> lowPoints = FindLowPointsInFirstRowWithTwoRows(lastRow, secondToLastRow);

            return lowPoints;
        }

        public List<int> FindLowPointsInFirstRowWithTwoRows(List<int> firstRow, List<int> secondRow)
        {
            List<int> lowPoints = new List<int>();

            if (FirstNumberIsSmallerThanRest(firstRow[0], new List<int> { firstRow[1], secondRow[0] }))
                lowPoints.Add(firstRow[0]);

            for (var index = 1; index < firstRow.Count - 1; index++)
            {
                List<int> numbersToCompareAgainst = new List<int>
                {
                    firstRow[index-1], firstRow[index+1], secondRow[index]
                };

                if (FirstNumberIsSmallerThanRest(firstRow[index], numbersToCompareAgainst))
                    lowPoints.Add(firstRow[index]);
            }

            if (FirstNumberIsSmallerThanRest(firstRow[^1], new List<int> { firstRow[^2], secondRow[^1] }))
                lowPoints.Add(firstRow[^1]);

            return lowPoints;
        }

        public List<int> FindLowPointsInRow(int rowIndex)
        {
            var previousRow = GetRow(rowIndex - 1);
            var currentRow = GetRow(rowIndex);
            var nextRow = GetRow(rowIndex + 1);

            List<int> lowPoints = new List<int>();

            if (FirstNumberIsSmallerThanRest(currentRow[0], new List<int> { previousRow[0], currentRow[1], nextRow[0] }))
                lowPoints.Add(currentRow[0]);

            for(var index = 1; index < currentRow.Count-1; index++)
            {
                List<int> numbersToCompareAgainst = new List<int>
                {
                    previousRow[index], currentRow[index-1], currentRow[index+1], nextRow[index]
                };

                if (FirstNumberIsSmallerThanRest(currentRow[index], numbersToCompareAgainst))
                    lowPoints.Add(currentRow[index]);
            }

            if (FirstNumberIsSmallerThanRest(currentRow[^1], new List<int> { previousRow[^1], currentRow[^2], nextRow[^1] }))
                lowPoints.Add(currentRow[^1]);

            return lowPoints;
        }

        public int CalculateRiskLevel()
        {
            List<int> lowPoints = FindAllLowPoints();

            int riskLevel = 0;

            foreach (var lowPoint in lowPoints)
                riskLevel += lowPoint + 1;

            return riskLevel;
        }

        public List<int> FindAllLowPoints()
        {
            List<int> lowPoints = new List<int>();

            lowPoints = FindLowPointsFirstLine();

            for (int rowIndex = 1; rowIndex < Map.Count - 1; rowIndex++)
                lowPoints.AddRange(FindLowPointsInRow(rowIndex));

            lowPoints.AddRange(FindLowPointsLastLine());

            return lowPoints;
        }
    }
}
