using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _2_Dive_.Test
{
    [TestClass]
    public class SubmarineTest
    {
        [TestMethod]
        public void TestInitializeSubmarineHorizontalAndDepth0()
        {
            Submarine submarine = new _2_Dive_.Submarine();
            Assert.AreEqual(0, submarine.HorizontalPosition);
            Assert.AreEqual(0, submarine.Depth);
        }

        [TestMethod]
        public void TestSubmarineIncreaseHorizontalPosition()
        {
            Submarine submarine = new _2_Dive_.Submarine();
            submarine.Move("forward 5");
            Assert.AreEqual(5, submarine.HorizontalPosition);
        }

        [TestMethod]
        public void TestSubmarineIncreaseDepth()
        {
            Submarine submarine = new _2_Dive_.Submarine();
            submarine.Move("down 3");
            Assert.AreEqual(3, submarine.Depth);
        }

        [TestMethod]
        public void TestSubmarineDecreaseDepth()
        {
            Submarine submarine = new _2_Dive_.Submarine();
            submarine.Move("up 3");
            Assert.AreEqual(-3, submarine.Depth);
        }

        [TestMethod]
        public void TestSubmarineRunMoveInstructions()
        {
            string[] moveInstructions = new string[] { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };
            Submarine submarine = new _2_Dive_.Submarine();
            submarine.RunMoveInstructions(moveInstructions);

            Assert.AreEqual(15, submarine.HorizontalPosition);
            Assert.AreEqual(10, submarine.Depth);

            Assert.AreEqual(150, submarine.HorizontalPosition * submarine.Depth);
        }

        [TestMethod]
        public void TestSubmarineRunMoveInstructionsFull()
        {
            string[] moveInstructions = LoadInstructions();
            Submarine submarine = new _2_Dive_.Submarine();
            submarine.RunMoveInstructions(moveInstructions);

            Console.WriteLine(submarine.HorizontalPosition * submarine.Depth);
        }

        [TestMethod]
        public void TestSubmarineEnableAim()
        {
            Submarine submarine = new _2_Dive_.Submarine();
            submarine.EnableAim();
            Assert.IsTrue(submarine.AimEnabled);
        }

        [TestMethod]
        public void TestSubmarineIncreaseHorizontalPositionWithAim0()
        {
            Submarine submarine = new _2_Dive_.Submarine();
            submarine.EnableAim();
            submarine.Move("forward 2");

            Assert.AreEqual(0, submarine.Aim);
            Assert.AreEqual(2, submarine.HorizontalPosition);
        }

        [TestMethod]
        public void TestSubmarineIncreaseDepthIncreaseAim()
        {
            Submarine submarine = new _2_Dive_.Submarine();
            submarine.EnableAim();
            submarine.Move("down 2");

            Assert.AreEqual(2, submarine.Aim);

            Assert.AreEqual(0, submarine.Depth);
        }

        [TestMethod]
        public void TestSubmarineIncreaseDepthIncreaseAimIncreaseHorizontalMultipliesWithAim()
        {
            Submarine submarine = new _2_Dive_.Submarine();
            submarine.EnableAim();
            submarine.Move("down 2");

            Assert.AreEqual(0, submarine.Depth);

            submarine.Move("forward 2");

            Assert.AreEqual(2, submarine.Aim);
            Assert.AreEqual(2, submarine.HorizontalPosition);
            Assert.AreEqual(4, submarine.Depth);
        }

        [TestMethod]
        public void TestSubmarineRunMoveInstructionsWithAimEnabled()
        {
            string[] moveInstructions = new string[] { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };

            Submarine submarine = new _2_Dive_.Submarine();
            submarine.EnableAim();
            submarine.RunMoveInstructions(moveInstructions);

            Assert.AreEqual(15, submarine.HorizontalPosition);
            Assert.AreEqual(60, submarine.Depth);

            Assert.AreEqual(900, submarine.HorizontalPosition * submarine.Depth);
        }

        [TestMethod]
        public void TestSubmarineRunMoveInstructionsFullWithAimEnabled()
        {
            string[] moveInstructions = LoadInstructions();
            Submarine submarine = new _2_Dive_.Submarine();

            submarine.EnableAim();
            submarine.RunMoveInstructions(moveInstructions);

            Console.WriteLine(submarine.HorizontalPosition * submarine.Depth);
        }

        public string[] LoadInstructions()
        {
            string[] instructions = System.IO.File.ReadAllLines("instructions.txt");
            return instructions;
        }
    }
}
