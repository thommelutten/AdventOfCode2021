using System;
using System.Collections.Generic;
using System.Text;

namespace _13_Transparent_Origami
{
    public class Paper
    {
        public List<List<bool>> Lines { get; private set; } = new List<List<bool>>();

        public void LoadDots(List<List<bool>> dots)
        {
            Lines = dots;
        }
    }
}
