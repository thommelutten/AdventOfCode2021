using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _11_Dumbo_Octopus.Test
{
    [TestClass]
    public class OctopusTest
    {
        [TestMethod]
        public void TestOctupusCreateEnergyHasFlashed()
        {
            Octopus octopus = new Octopus(energy: 5);
            Assert.AreEqual(5, octopus.Energy);
            Assert.IsFalse(octopus.HasFlashed);
        }

        [TestMethod]
        public void TestOctopusIncrementEnergy()
        {
            Octopus octopus = new Octopus(energy: 5);
            Assert.AreEqual(5, octopus.Energy);

            octopus.IncrementEnergy();
            Assert.AreEqual(6, octopus.Energy);
        }

        [TestMethod]
        public void TestOctopusShouldFlashAtEnergy10()
        {
            Octopus octopus = new Octopus(energy: 9);
            Assert.AreEqual(9, octopus.Energy);

            Assert.IsFalse(octopus.ShouldFlash());

            octopus.IncrementEnergy();

            Assert.IsTrue(octopus.ShouldFlash());
        }

        [TestMethod]
        public void TestOctopusFlash()
        {
            Octopus octopus = new Octopus(energy: 10);
            Assert.AreEqual(10, octopus.Energy);

            octopus.Flash();
            Assert.AreEqual(0, octopus.Energy);
        }

        [TestMethod]
        public void TestOctopusHasFlashed()
        {
            Octopus octopus = new Octopus(energy: 10);
            Assert.AreEqual(10, octopus.Energy);

            Assert.IsFalse(octopus.HasFlashed);

            octopus.Flash();
            Assert.IsTrue(octopus.HasFlashed);
        }

        [TestMethod]
        public void TestOctopusFlashUnableToAddEnergy()
        {
            Octopus octopus = new Octopus(energy: 10);

            octopus.Flash();
            Assert.AreEqual(0, octopus.Energy);

            octopus.IncrementEnergy();
            Assert.AreEqual(0, octopus.Energy);
        }

        [TestMethod]
        public void TestOctopusResetFlash()
        {
            Octopus octopus = new Octopus(energy: 10);
            Assert.IsFalse(octopus.HasFlashed);

            octopus.Flash();
            Assert.IsTrue(octopus.HasFlashed);

            octopus.ResetFlash();
            Assert.IsFalse(octopus.HasFlashed);
        }
    }
}
