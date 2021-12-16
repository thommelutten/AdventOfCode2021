using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace _12_Passage_Pathing.Test
{
    [TestClass]
    public class CaveRouteTest
    {
        [TestMethod]
        public void TestCaveRouteCreate()
        {
            CaveRoute caveRoute = new CaveRoute();
            Assert.IsNotNull(caveRoute);
        }

        [TestMethod]
        public void TestCaveRouteGetTraversedPath()
        {
            CaveRoute caveRoute = new CaveRoute();
            List<(string start, string destination)> traversedPath = caveRoute.TraversedPaths;
            Assert.AreEqual(0, traversedPath.Count);
        }

        [TestMethod]
        public void TestCaveRouteAddTraversedPath()
        {
            CaveRoute caveRoute = new CaveRoute();
            string start = "start";
            string destination = "a";

            caveRoute.AddPath(start, destination);

            List<(string start, string destination)> traversedPath = caveRoute.TraversedPaths;

            Assert.AreEqual(1, traversedPath.Count);
            Assert.AreEqual(start, traversedPath[0].start);
            Assert.AreEqual(destination, traversedPath[0].destination);
        }

        [TestMethod]
        public void TestCaveRouteHasReachedEndReturnTrue()
        {
            CaveRoute caveRoute = new CaveRoute();

            string start = "start";
            string end = "end";

            caveRoute.AddPath(start, end);

            Assert.IsTrue(caveRoute.HasReachedEnd());
        }

        [TestMethod]
        public void TestCaveRouteHasReachedEndReturnFalse()
        {
            CaveRoute caveRoute = new CaveRoute();

            string start = "start";
            string end = "a";

            caveRoute.AddPath(start, end);

            Assert.IsFalse(caveRoute.HasReachedEnd());
        }

        [TestMethod]
        public void TestCaveRouteHasPreviouslyVisitedCaveReturnTrue()
        {
            CaveRoute caveRoute = new CaveRoute();

            string start = "start";
            string end = "a";

            caveRoute.AddPath(start, end);

            start = "a";
            end = "b";

            caveRoute.AddPath(start, end);

            var shouldBeTrue = caveRoute.HasPreviouslyVisitedCave(start);

            Assert.IsTrue(shouldBeTrue);

            var shouldBeFalse = caveRoute.HasPreviouslyVisitedCave("c");

            Assert.IsFalse(shouldBeFalse);
        }

        [TestMethod]
        public void TestCaveRouteGetCurrentCave()
        {
            CaveRoute caveRoute = new CaveRoute();

            string start = "start";
            string end = "a";

            caveRoute.AddPath(start, end);

            var currentCave = caveRoute.GetCurrentCave();
            Assert.AreEqual("a", currentCave);
        }
    }
}
