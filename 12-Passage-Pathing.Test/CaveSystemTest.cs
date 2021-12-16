using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _12_Passage_Pathing.Test
{
    [TestClass]
    public class CaveSystemTest
    {
        [TestMethod]
        public void TestCaveSystemCreate()
        {
            CaveSystem caveSystem = new CaveSystem();
            Assert.IsNotNull(caveSystem);
        }

        [TestMethod]
        public void TestCaveSystemLoadRoutes()
        {
            CaveSystem caveSystem = new CaveSystem();

            string[] routes = LoadCaveRoutesFromFile("smallTest.txt");

            caveSystem.LoadRoutes(routes);

            Assert.AreEqual(7, caveSystem.CaveConnections.Count);

            for(int index = 0; index < routes.Length; index++)
            {
                Assert.IsTrue(routes[index].Contains(caveSystem.CaveConnections[index].start));
                Assert.IsTrue(routes[index].Contains(caveSystem.CaveConnections[index].destination));
            }            
        }

        [TestMethod]
        public void TestCaveSystemGetWalkedCaveRoutes()
        {
            CaveSystem caveSystem = new CaveSystem();

            string[] routes = LoadCaveRoutesFromFile("smallTest.txt");
            caveSystem.LoadRoutes(routes);

            Assert.AreEqual(0, caveSystem.WalkedCaveRoutes.Count);
        }

        [TestMethod]
        public void TestCaveSystemStartRoutes()
        {
            CaveSystem caveSystem = InitializeCaveSystem("smallTest.txt");

            caveSystem.StartRoutes();

            Assert.AreEqual(2, caveSystem.WalkedCaveRoutes.Count);

            Assert.AreEqual("start", caveSystem.WalkedCaveRoutes[0].TraversedPaths[0].start);
            Assert.AreEqual("A", caveSystem.WalkedCaveRoutes[0].TraversedPaths[0].destination);

            Assert.AreEqual("start", caveSystem.WalkedCaveRoutes[0].TraversedPaths[0].start);
            Assert.AreEqual("b", caveSystem.WalkedCaveRoutes[1].TraversedPaths[0].destination);
        }

        [TestMethod]
        public void TestCaveSystemMove()
        {
            CaveSystem caveSystem = InitializeCaveSystem("smallTest.txt");

            caveSystem.StartRoutes();
            caveSystem.Move();

            Assert.AreEqual(6, caveSystem.WalkedCaveRoutes.Count);
        }

        [TestMethod]
        public void TestCaveSystemMoveTwice()
        {
            CaveSystem caveSystem = InitializeCaveSystem("smallTest.txt");

            caveSystem.StartRoutes();
            caveSystem.Move();
            caveSystem.Move();

            Assert.AreEqual(8, caveSystem.WalkedCaveRoutes.Count);
        }

        [TestMethod]
        public void TestCaveSystemFindAllRoutesSmallTest()
        {
            CaveSystem caveSystem = InitializeCaveSystem("smallTest.txt");

            caveSystem.FindAllRoutes();

            Assert.AreEqual(10, caveSystem.WalkedCaveRoutes.Count);
        }

        [TestMethod]
        public void TestCaveSystemFindAllRoutesMiddleSizedTest()
        {
            CaveSystem caveSystem = InitializeCaveSystem("middleSizedTest.txt");

            caveSystem.FindAllRoutes();

            Assert.AreEqual(19, caveSystem.WalkedCaveRoutes.Count);
        }

        [TestMethod]
        public void TestCaveSystemFindAllRoutesLargeTest()
        {
            CaveSystem caveSystem = InitializeCaveSystem("largeTest.txt");

            caveSystem.FindAllRoutes();

            Assert.AreEqual(226, caveSystem.WalkedCaveRoutes.Count);
        }

        [TestMethod]
        public void TestCaveSystemFindAllRoutesFullTest()
        {
            CaveSystem caveSystem = InitializeCaveSystem("fullTest.txt");

            caveSystem.FindAllRoutes();

            Console.WriteLine(caveSystem.WalkedCaveRoutes.Count);
        }

        [TestMethod]
        public void TestCaveSystemAllowSingleSmallCaveToBeVisitedTwice()
        {
            CaveSystem caveSystem = new CaveSystem();

            caveSystem.AllowSingleSmallCaveToBeVisitedTwice();

            Assert.IsTrue(caveSystem.SingleSmallCaveCanBeVisitedTwice);
        }

        [TestMethod]
        public void TestCaveSystemFindAllRoutesAllowTwiceVisitation()
        {
            CaveSystem caveSystem = InitializeCaveSystem("smallTest.txt");

            caveSystem.AllowSingleSmallCaveToBeVisitedTwice();
            caveSystem.FindAllRoutes();

            Assert.AreEqual(36, caveSystem.WalkedCaveRoutes.Count);
        }

        [TestMethod]
        public void TestCaveSystemFindAllRoutesAllowTwiceVisitationMiddleSizedTest()
        {
            CaveSystem caveSystem = InitializeCaveSystem("middleSizedTest.txt");

            caveSystem.AllowSingleSmallCaveToBeVisitedTwice();
            caveSystem.FindAllRoutes();

            Assert.AreEqual(103, caveSystem.WalkedCaveRoutes.Count);
        }

        [TestMethod]
        public void TestCaveRouteHasVisitedASmallCaveTwice()
        {
            CaveSystem caveSystem = new CaveSystem();
            CaveRoute caveRoute = new CaveRoute();
            caveRoute.AddPath("start", "a");
            caveRoute.AddPath("a", "b");
            caveRoute.AddPath("b", "C");
            caveRoute.AddPath("C", "a");
            caveRoute.AddPath("a", "C");

            Assert.IsTrue(caveSystem.CaveRouteHasVisitedASmallCaveTwice(caveRoute));
        }

        [TestMethod]
        public void TestCaveSystemFindAllRoutesAllowTwiceVisitatioLargeTest()
        {
            CaveSystem caveSystem = InitializeCaveSystem("largeTest.txt");

            caveSystem.AllowSingleSmallCaveToBeVisitedTwice();
            caveSystem.FindAllRoutes();

            Assert.AreEqual(3509, caveSystem.WalkedCaveRoutes.Count);
        }

        [TestMethod]
        public void TestCaveSystemFindAllRoutesAllowTwiceVisitatioFullTest()
        {
            CaveSystem caveSystem = InitializeCaveSystem("fullTest.txt");

            caveSystem.AllowSingleSmallCaveToBeVisitedTwice();
            caveSystem.FindAllRoutes();

            Console.WriteLine(caveSystem.WalkedCaveRoutes.Count);
        }

        private CaveSystem InitializeCaveSystem(string path)
        {
            CaveSystem caveSystem = new CaveSystem();

            string[] routes = LoadCaveRoutesFromFile(path);
            caveSystem.LoadRoutes(routes);

            return caveSystem;
        }

        private string[] LoadCaveRoutesFromFile(string path)
        {
            string[] caveRoutes = System.IO.File.ReadAllLines(path);
            return caveRoutes;
        }

    }
}
