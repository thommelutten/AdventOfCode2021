using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_The_Treachery_of_Whales
{
    public class FuelCalculator
    {
        private bool ExpensiveStepsEnabled = false;

        public int CalculateFuelForBestHorizontalPosition(int[] crabs)
        {
            int bestHorizontalPosition = int.MaxValue;

            for(int horizontalPosition = 0; horizontalPosition < crabs.Max(); horizontalPosition++)
            {
                int fuelNeededToPosition = 0;
                foreach (var crabPosition in crabs)
                {
                    int fuelNeeded = Math.Abs(horizontalPosition - crabPosition);
                    if (ExpensiveStepsEnabled)
                        fuelNeeded = CalculateExpensiveSteps(fuelNeeded);
                    fuelNeededToPosition += fuelNeeded;
                }
                    

                if (fuelNeededToPosition < bestHorizontalPosition)
                    bestHorizontalPosition = fuelNeededToPosition;
            }
            return bestHorizontalPosition;
        }

        private int CalculateExpensiveSteps(int fuelNeeded)
        {
            int sum = 0;
            for (int i = 0; i <= fuelNeeded; i++)
                sum += i;
            return sum;
        }

        public void EnableExpensiveSteps()
        {
            ExpensiveStepsEnabled = true;
        }
    }
}
