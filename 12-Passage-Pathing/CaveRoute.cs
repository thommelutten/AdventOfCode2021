using System;
using System.Collections.Generic;
using System.Text;

namespace _12_Passage_Pathing
{
    public class CaveRoute
    {
        public List<(string start, string destination)> TraversedPaths { get; private set; } = new List<(string start, string destination)>();

        public void AddPath(string start, string destination)
        {
            TraversedPaths.Add((start, destination));
        }

        public bool HasReachedEnd()
        {
            return TraversedPaths[^1].destination.Equals("end");
        }

        public bool HasPreviouslyVisitedCave(string destination)
        {
            foreach(var TraversedPath in TraversedPaths)
                if(TraversedPath.start.Equals(destination))
                    return true;
            return false;
        }

        public string GetCurrentCave()
        {
            return TraversedPaths[^1].destination;
        }

        public List<string> GetCavesPreviouslyVisited()
        {
            List<string> cavesVisited = new List<string>();

            foreach (var traversedPath in TraversedPaths)
                cavesVisited.Add(traversedPath.start);

            cavesVisited.Add(GetCurrentCave());
                

            return cavesVisited;
        }
    }
}
