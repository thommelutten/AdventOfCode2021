using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _13_Transparent_Origami
{
    public class Paper
    {
        public List<List<bool>> Lines { get; private set; } = new List<List<bool>>();
        public List<(string axis, int lineNumber)> FoldingInstructions { get; set; }

        public void LoadDots(List<List<bool>> dots)
        {
            Lines = dots;
        }

        public void LoadFoldingInstructions(List<(string axis, int lineNumber)> foldingInstructions)
        {
            FoldingInstructions = foldingInstructions;
        }

        public void Fold()
        {
            var foldingInstruction = FoldingInstructions[0];

            if (foldingInstruction.axis.Equals("x"))
                FoldVerticalAt(foldingInstruction.lineNumber);
            else
                FoldHorizontalAt(foldingInstruction.lineNumber);

            FoldingInstructions.RemoveAt(0);
        }

        public void FoldHorizontalAt(int rowNumberToFoldAt)
        {
            var reversedBottomPartOfPaper = new List<List<bool>>();

            for(int paperLineIndex = Lines.Count-1; paperLineIndex > rowNumberToFoldAt; paperLineIndex--)
            {
                var paperLine = new List<bool>();
                for (int positionOnLine = 0; positionOnLine < Lines[paperLineIndex].Count; positionOnLine++)
                    paperLine.Add(Lines[paperLineIndex][positionOnLine]);
                reversedBottomPartOfPaper.Add(paperLine);
                Lines.RemoveAt(paperLineIndex);
            }

            Lines.RemoveAt(Lines.Count - 1);

            for(int paperLineIndex = 0; paperLineIndex < reversedBottomPartOfPaper.Count; paperLineIndex++)
            {
                for(int positionOnLine = 0; positionOnLine < reversedBottomPartOfPaper[paperLineIndex].Count; positionOnLine++)
                {
                    if (Lines[paperLineIndex].Count <= positionOnLine)
                        Lines[paperLineIndex].Add(false);

                    if (Lines[paperLineIndex][positionOnLine])
                        continue;

                    Lines[paperLineIndex][positionOnLine] = reversedBottomPartOfPaper[paperLineIndex][positionOnLine];
                }
            }
        }

        public void FoldVerticalAt(int columnNumberToFoldAt)
        {
            var rightSideOfPaper = new List<List<bool>>();

            for(int paperLineIndex = 0; paperLineIndex < Lines.Count; paperLineIndex++)
            {
                var paperLine = new List<bool>(new bool[columnNumberToFoldAt + 1]);
                for(int positionOnLine = columnNumberToFoldAt; positionOnLine < Lines[paperLineIndex].Count; positionOnLine++)
                {
                    paperLine[(positionOnLine - columnNumberToFoldAt)] = Lines[paperLineIndex][positionOnLine];
                }
                rightSideOfPaper.Add(paperLine);
            }

            var reversedRightSideOfPaper = ReverseLines(rightSideOfPaper);

            for(int paperlineIndex = 0; paperlineIndex < Lines.Count;  paperlineIndex++)
            {
                for(int positionOnLine = 0; positionOnLine < reversedRightSideOfPaper[paperlineIndex].Count; positionOnLine++)
                {
                    if (Lines[paperlineIndex].Count <= positionOnLine)
                        Lines[paperlineIndex].Add(false);

                    if (Lines[paperlineIndex][positionOnLine])
                        continue;
                    Lines[paperlineIndex][positionOnLine] = reversedRightSideOfPaper[paperlineIndex][positionOnLine];
                }
                while (Lines[paperlineIndex].Count != reversedRightSideOfPaper[paperlineIndex].Count - 1)
                    Lines[paperlineIndex].RemoveAt(Lines[paperlineIndex].Count - 1);

            }
        }

        public List<List<bool>> ReverseLines(List<List<bool>> lines)
        {
            List<List<bool>> reversedLines = new List<List<bool>>();
            foreach (var line in lines)
                reversedLines.Add(ReverseLine(line));
            return reversedLines;
        }

        public List<bool> ReverseLine(List<bool> line)
        {
            line.Reverse();
            return line;
        }

        public void FoldAllInstructions()
        {
            while (FoldingInstructions.Count > 0)
                Fold();
        }

        public int CountDots()
        {
            int dots = 0;

            foreach(var line in Lines)
                dots += line.Where(isMarked => isMarked).Count();

            return dots;
        }

        public List<string> DecipherCode()
        {
            var decipheredLines = new List<string>();
            foreach(var line in Lines)
            {
                string decipheredLine = string.Empty;
                foreach(var position in line)
                {
                    if (position)
                        decipheredLine += "#";
                    else
                        decipheredLine += "  ";
                }
                decipheredLines.Add(decipheredLine);
            }
            return decipheredLines;
        }
    }
}
