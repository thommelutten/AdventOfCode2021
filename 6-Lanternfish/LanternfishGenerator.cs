using System;
using System.Collections.Generic;
using System.Text;

namespace _6_Lanternfish
{
    public class LanternfishGenerator
    {
        private readonly Dictionary<int, long> School = new Dictionary<int, long>();

        public LanternfishGenerator(int[] lanternfishSchool)
        {
            InitializeSchool();
            LoadSchool(lanternfishSchool);
        }

        private void InitializeSchool()
        {
            for (int schoolIndex = 0; schoolIndex <= 8; schoolIndex++)
                School[schoolIndex] = 0;
        }

        public void LoadSchool(int[] lanternfishSchool)
        {
            foreach (var fishInternalTimer in lanternfishSchool)
                School[fishInternalTimer] += 1;
        }
        
        public long CountFish()
        {
            long fish = 0;
            foreach (var value in School)
                fish += ((long)value.Value);
            return fish;
        }

        public void RunDays(int daysToRun)
        {
            for(int day = 0; day < daysToRun; day++)
            {
                var fishInternalTimer = day % School.Count;
                long newFishToCreate = School[fishInternalTimer];

                var dayNewFishShouldBeAddedTo = (fishInternalTimer + 7) % School.Count;
                School[dayNewFishShouldBeAddedTo] += newFishToCreate;
            }
        }
    }
}
