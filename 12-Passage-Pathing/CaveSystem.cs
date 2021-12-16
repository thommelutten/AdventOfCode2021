using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _12_Passage_Pathing
{
    public class CaveSystem
    {
        public List<(string start, string destination)> CaveConnections { get; private set; } = new List<(string start, string destination)>();
        public List<CaveRoute> WalkedCaveRoutes { get; set; } = new List<CaveRoute>();

        public bool SingleSmallCaveCanBeVisitedTwice { get; private set; }  = false;

        public void LoadRoutes(string[] routes)
        {
            foreach(var route in routes)
            {
                var startAndDestination = route.Split('-');
                CaveConnections.Add((startAndDestination[0], startAndDestination[1]));
            }
        }

        public void StartRoutes()
        {
            foreach(var connectionFromStart in CaveConnections.Where(connection => connection.start.Equals("start") || connection.destination.Equals("start")))
            {
                CaveRoute caveRoute = new CaveRoute();
                if(connectionFromStart.start.Equals("start"))
                    caveRoute.AddPath(connectionFromStart.start, connectionFromStart.destination);
                else
                    caveRoute.AddPath(connectionFromStart.destination, connectionFromStart.start);

                WalkedCaveRoutes.Add(caveRoute);
            }
        }

        public void Move()
        {
            List<CaveRoute> updatedWalkedCaveRoutes = new List<CaveRoute>();

            foreach (var walkedCaveRoute in WalkedCaveRoutes)
            {
                if (walkedCaveRoute.HasReachedEnd())
                {
                    updatedWalkedCaveRoutes.Add(walkedCaveRoute);
                    continue;
                }

                if (walkedCaveRoute.TraversedPaths.Count == 7)
                {
                    var a = 2;
                }

                var availableRoutesFromCurrentCave = CaveConnections.Where(caveConnection => caveConnection.start.Equals(walkedCaveRoute.GetCurrentCave()) || caveConnection.destination.Equals(walkedCaveRoute.GetCurrentCave())).ToList();
                
                foreach (var availableRoute in availableRoutesFromCurrentCave)
                {
                    string start = availableRoute.start;
                    string destination = availableRoute.destination;

                    if (walkedCaveRoute.GetCurrentCave().Equals(destination))
                    {
                        destination = availableRoute.start;
                        start = availableRoute.destination;
                    }

                    if (destination.Equals("start"))
                        continue;

                    if(SingleSmallCaveCanBeVisitedTwice)
                    {
                        if (CaveRouteHasVisitedASmallCaveTwice(walkedCaveRoute) && 
                            IsSmallCave(destination) && 
                            walkedCaveRoute.HasPreviouslyVisitedCave(destination))
                            continue;
                    } 
                    else
                    {
                        if (walkedCaveRoute.HasPreviouslyVisitedCave(destination) &&
                        IsSmallCave(destination))
                            continue;
                    }

                    CaveRoute updatedRoute = new CaveRoute();
                    foreach (var route in walkedCaveRoute.TraversedPaths)
                        updatedRoute.AddPath(route.start, route.destination);

                    
                    updatedRoute.AddPath(start, destination);
                    
                    updatedWalkedCaveRoutes.Add(updatedRoute);
                }
                
            }
            WalkedCaveRoutes = updatedWalkedCaveRoutes;
        }

        public bool CaveRouteHasVisitedASmallCaveTwice(CaveRoute walkedCaveRoute)
        {
            var cavesVisited = walkedCaveRoute.GetCavesPreviouslyVisited();

            var smallCaves = cavesVisited.Where(cave => IsSmallCave(cave)).ToList();

            var uniqueSmallCaves = smallCaves.Distinct().ToList();

            foreach (var uniqueSmallCave in uniqueSmallCaves)
                if(smallCaves.Where(smallCave => smallCave.Equals(uniqueSmallCave)).Count() > 1) {
                    return true;
                }

            return false;
        }

        public bool IsSmallCave(string destination)
        {
            foreach(char c in destination)
            {
                if (char.IsUpper(c)) 
                    return false;
            }

            return true;

        }

        public void FindAllRoutes()
        {
            StartRoutes();
            while (NotAllRoundsDiscovered())
                Move();
        }

        private bool NotAllRoundsDiscovered()
        {
            return WalkedCaveRoutes.Where(caveRoute => !caveRoute.HasReachedEnd()).Any();
        }

        public void AllowSingleSmallCaveToBeVisitedTwice()
        {
            SingleSmallCaveCanBeVisitedTwice = true;
        }
    }
}
