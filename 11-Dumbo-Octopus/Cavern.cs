using System;
using System.Collections.Generic;
using System.Text;

namespace _11_Dumbo_Octopus
{
    public class Cavern
    {
        public List<List<Octopus>> Octopodes { get; private set; } = new List<List<Octopus>>();
        public int OctopodesThatHasFlashed { get; private set; } = 0;

        public List<List<Octopus>> GetOctopodes()
        {
            return Octopodes;
        }

        public void LoadOctopodes(List<List<int>> octopodesInput)
        {
            List<List<Octopus>> octopodes = new List<List<Octopus>>();

            foreach(var octopodeRowFromInput in octopodesInput)
            {
                List<Octopus> octopodeRow = new List<Octopus>();
                foreach(var octopusEnergyValue in octopodeRowFromInput)
                {
                    Octopus octopus = new Octopus(octopusEnergyValue);
                    octopodeRow.Add(octopus);
                }
                octopodes.Add(octopodeRow);
            }

            Octopodes = octopodes;
        }

        public void Step()
        {
            for (int verticalIndex = 0; verticalIndex < Octopodes.Count; verticalIndex++)
                for (int horizontalIndex = 0; horizontalIndex < Octopodes[0].Count; horizontalIndex++)
                    Octopodes[verticalIndex][horizontalIndex].IncrementEnergy();
        }

        public bool OctopodesCanFlash()
        {
            foreach(var octopodeRow in Octopodes)
                foreach(var octopus in octopodeRow)
                    if(octopus.Energy > 9 && !octopus.HasFlashed)
                    {
                        return true;
                    }

            return false;
        }

        public List<(int row, int column)> GetIndicesOfOctopodesThatCanFlash()
        {
            List<(int row, int column)> indicesOfOctopodesThatCanFlash = new List<(int row, int column)>();

            for (int verticalIndex = 0; verticalIndex < Octopodes.Count; verticalIndex++)
                for (int horizontalIndex = 0; horizontalIndex < Octopodes[0].Count; horizontalIndex++)
                {
                    var octopus = Octopodes[verticalIndex][horizontalIndex];
                    if (octopus.Energy > 9 && !octopus.HasFlashed)
                        indicesOfOctopodesThatCanFlash.Add((verticalIndex, horizontalIndex));
                }

            return indicesOfOctopodesThatCanFlash;
        }

        public void StepAndCheckForFlashes()
        {
            ResetOctopodesFlash();
            Step();

            while (OctopodesCanFlash())
            {
                List<(int row, int column)> octopodesThatCanFlash = GetIndicesOfOctopodesThatCanFlash();
                OctopodesThatHasFlashed += octopodesThatCanFlash.Count;

                foreach(var octopusIndex in octopodesThatCanFlash)
                {
                    Octopodes[octopusIndex.row][octopusIndex.column].Flash();
                    UpdateNeighboringOctopodes(octopusIndex);
                }
            }
        }

        private void ResetOctopodesFlash()
        {
            for (int verticalIndex = 0; verticalIndex < Octopodes.Count; verticalIndex++)
                for (int horizontalIndex = 0; horizontalIndex < Octopodes[0].Count; horizontalIndex++)
                    Octopodes[verticalIndex][horizontalIndex].ResetFlash();
        }

        private void UpdateNeighboringOctopodes((int row, int column) octopusIndex)
        {
            if(octopusIndex.row > 0 && octopusIndex.row < Octopodes.Count - 1)
            {
                if(octopusIndex.column > 0 && octopusIndex.column < Octopodes[octopusIndex.row].Count - 1)
                {
                    Octopodes[octopusIndex.row - 1][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row - 1][octopusIndex.column].IncrementEnergy();
                    Octopodes[octopusIndex.row - 1][octopusIndex.column + 1].IncrementEnergy();

                    Octopodes[octopusIndex.row][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row][octopusIndex.column + 1].IncrementEnergy();

                    Octopodes[octopusIndex.row + 1][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row + 1][octopusIndex.column].IncrementEnergy();
                    Octopodes[octopusIndex.row + 1][octopusIndex.column + 1].IncrementEnergy();
                }
                else if(octopusIndex.column == 0)
                {
                    Octopodes[octopusIndex.row - 1][octopusIndex.column].IncrementEnergy();
                    Octopodes[octopusIndex.row - 1][octopusIndex.column + 1].IncrementEnergy();

                    Octopodes[octopusIndex.row][octopusIndex.column + 1].IncrementEnergy();

                    Octopodes[octopusIndex.row + 1][octopusIndex.column].IncrementEnergy();
                    Octopodes[octopusIndex.row + 1][octopusIndex.column + 1].IncrementEnergy();
                }
                else
                {
                    Octopodes[octopusIndex.row - 1][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row - 1][octopusIndex.column].IncrementEnergy();

                    Octopodes[octopusIndex.row][octopusIndex.column - 1].IncrementEnergy();

                    Octopodes[octopusIndex.row + 1][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row + 1][octopusIndex.column].IncrementEnergy();
                }

            } else if(octopusIndex.row == 0)
            {
                if (octopusIndex.column > 0 && octopusIndex.column < Octopodes[octopusIndex.row].Count - 1)
                {
                    Octopodes[octopusIndex.row][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row][octopusIndex.column + 1].IncrementEnergy();

                    Octopodes[octopusIndex.row + 1][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row + 1][octopusIndex.column].IncrementEnergy();
                    Octopodes[octopusIndex.row + 1][octopusIndex.column + 1].IncrementEnergy();
                }
                else if (octopusIndex.column == 0)
                {
                    Octopodes[octopusIndex.row][octopusIndex.column + 1].IncrementEnergy();

                    Octopodes[octopusIndex.row + 1][octopusIndex.column].IncrementEnergy();
                    Octopodes[octopusIndex.row + 1][octopusIndex.column + 1].IncrementEnergy();
                }
                else
                {
                    Octopodes[octopusIndex.row][octopusIndex.column - 1].IncrementEnergy();

                    Octopodes[octopusIndex.row + 1][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row + 1][octopusIndex.column].IncrementEnergy();
                }
            } else
            {
                if (octopusIndex.column > 0 && octopusIndex.column < Octopodes[octopusIndex.row].Count - 1)
                {
                    Octopodes[octopusIndex.row - 1][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row - 1][octopusIndex.column].IncrementEnergy();
                    Octopodes[octopusIndex.row - 1][octopusIndex.column + 1].IncrementEnergy();

                    Octopodes[octopusIndex.row][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row][octopusIndex.column + 1].IncrementEnergy();
                }
                else if (octopusIndex.column == 0)
                {
                    Octopodes[octopusIndex.row - 1][octopusIndex.column].IncrementEnergy();
                    Octopodes[octopusIndex.row - 1][octopusIndex.column + 1].IncrementEnergy();

                    Octopodes[octopusIndex.row][octopusIndex.column + 1].IncrementEnergy();
                }
                else
                {
                    Octopodes[octopusIndex.row - 1][octopusIndex.column - 1].IncrementEnergy();
                    Octopodes[octopusIndex.row - 1][octopusIndex.column].IncrementEnergy();

                    Octopodes[octopusIndex.row][octopusIndex.column - 1].IncrementEnergy();
                }
            }
        }

        public bool OctopodesFlashedSyncronously()
        {
            foreach (var octopodeRow in Octopodes)
                foreach (var octopus in octopodeRow)
                    if (!octopus.HasFlashed)
                        return false;

            return true; ;
        }
    }
}
