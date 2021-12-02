using System;
using System.Collections.Generic;
using System.Text;

namespace _2_Dive_
{
    public class Submarine
    {
        public int HorizontalPosition { get; private set; }
        public int Depth { get; private set; }

        public int Aim { get; private set; }

        public bool AimEnabled { get; private set; } = false;

        public void Move(string directions)
        {
            string[] directionAndLength = directions.Split(' ');

            var direction = directionAndLength[0];
            var directionLength = int.Parse(directionAndLength[1]);

            if (direction.Equals("forward"))
            {
                AdjustHorizontalPosition(directionLength);
                if (AimEnabled)
                    AdjustDepthPosition(Aim * directionLength);
            }

            if (direction.Equals("down"))
            {
                if (AimEnabled)
                {
                    AdjustAim(directionLength);
                } 
                else
                {
                    AdjustDepthPosition(directionLength);
                }
            }
                
            if (direction.Equals("up"))
            {
                if (AimEnabled)
                {
                    AdjustAim(-directionLength);
                } 
                else
                {
                    AdjustDepthPosition(-directionLength);
                }
            }
        }

        private void AdjustAim(int change)
        {
            Aim += change;
        }

        private void AdjustDepthPosition(int movement)
        {
            Depth += movement;
        }

        private void AdjustHorizontalPosition(int movement)
        {
            HorizontalPosition += movement;
        }

        public void RunMoveInstructions(string[] moveInstructions)
        {
            foreach (var moveInstruction in moveInstructions)
                Move(moveInstruction);
        }

        public void EnableAim()
        {
            AimEnabled = true;
        }
    }
}
