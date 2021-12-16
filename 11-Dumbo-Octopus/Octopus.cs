using System;
using System.Collections.Generic;
using System.Text;

namespace _11_Dumbo_Octopus
{
    public class Octopus
    {
        public int Energy { get; private set; }
        public bool HasFlashed { get; private set; } = false;

        public Octopus(int energy)
        {
            Energy = energy;

        }

        public void IncrementEnergy()
        {
            if(!HasFlashed)
                Energy += 1;
        }

        public bool ShouldFlash()
        {
            return Energy > 9;
        }

        public void Flash()
        {
            HasFlashed = true;
            Energy = 0;
        }

        public void ResetFlash()
        {
            HasFlashed = false;
        }
    }
}
