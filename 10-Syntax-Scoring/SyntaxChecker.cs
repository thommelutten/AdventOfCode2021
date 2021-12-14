using System;
using System.Collections.Generic;
using System.Text;

namespace _10_Syntax_Scoring
{
    public class SyntaxChecker
    {
        public List<string> FileLines { get; private set; }

        public void LoadFile(string[] fileLines)
        {
            FileLines = new List<string>(fileLines);
        }

        public char CheckLine(int fileLineIndex)
        {
            string line = FileLines[fileLineIndex];

            List<char> openCharacters = new List<char>();

            foreach(char character in line)
            {
                if(character.Equals('(') ||
                    character.Equals('[') ||
                    character.Equals('{') ||
                    character.Equals('<')
                    )
                {
                    openCharacters.Add(character);
                }else
                {
                    if (character.Equals(')') && openCharacters[^1].Equals('('))
                        openCharacters.RemoveAt(openCharacters.Count - 1);
                    else if (character.Equals(']') && openCharacters[^1].Equals('['))
                        openCharacters.RemoveAt(openCharacters.Count - 1);
                    else if (character.Equals('}') && openCharacters[^1].Equals('{'))
                        openCharacters.RemoveAt(openCharacters.Count - 1);
                    else if (character.Equals('>') && openCharacters[^1].Equals('<'))
                        openCharacters.RemoveAt(openCharacters.Count - 1);
                    else
                        return character;
                }
            }
            return '0';
        }

        public int CalculateScore()
        {
            List<char> illegalCharacters = new List<char>();

            for (int index = 0; index < FileLines.Count; index++)
                illegalCharacters.Add(CheckLine(index));

            int score = 0;

            foreach (var illegalCharacter in illegalCharacters)
                score += ConvertCharacterToPoint(illegalCharacter);

            return score;
        }

        private int ConvertCharacterToPoint(char illegalCharacter)
        {
            if (illegalCharacter.Equals(')')) return 3;
            if (illegalCharacter.Equals(']')) return 57;
            if (illegalCharacter.Equals('}')) return 1197;
            if (illegalCharacter.Equals('>')) return 25137;
            return 0;
        }

        public void DiscardIncompleteLines()
        {
            List<string> linesToKeep = new List<string>();

            for(int lineIndex = 0; lineIndex < FileLines.Count; lineIndex++)
            {
                char result = CheckLine(lineIndex);
                if (result.Equals('0'))
                    linesToKeep.Add(FileLines[lineIndex]);
            }

            FileLines = linesToKeep;
        }

        public List<char> CalculateMissingCharacters(int index)
        {
            string line = FileLines[index];

            List<char> openCharacters = new List<char>();
            //List<char> charactersMissingACloser = new List<char>();

            foreach (char character in line)
            {
                if (character.Equals('(') ||
                    character.Equals('[') ||
                    character.Equals('{') ||
                    character.Equals('<')
                    )
                {
                    openCharacters.Add(character);
                }
                else
                {
                    if (character.Equals(')') && openCharacters[^1].Equals('('))
                        openCharacters.RemoveAt(openCharacters.Count - 1);
                    else if (character.Equals(']') && openCharacters[^1].Equals('['))
                        openCharacters.RemoveAt(openCharacters.Count - 1);
                    else if (character.Equals('}') && openCharacters[^1].Equals('{'))
                        openCharacters.RemoveAt(openCharacters.Count - 1);
                    else if (character.Equals('>') && openCharacters[^1].Equals('<'))
                        openCharacters.RemoveAt(openCharacters.Count - 1);
                }
            }

            List<char> closersMissing = new List<char>();

            foreach(char character in openCharacters)
            {
                if (character.Equals('(')) closersMissing.Add(')');
                if (character.Equals('[')) closersMissing.Add(']');
                if (character.Equals('{')) closersMissing.Add('}');
                if (character.Equals('<')) closersMissing.Add('>');
            }

            closersMissing.Reverse();
            return closersMissing;
        }

        public long CalculateMissingCharactersMiddleScore()
        {
            List<List<char>> linesOfMissingCharacters = new List<List<char>>();
            
            for (int index = 0; index < FileLines.Count; index++)
                linesOfMissingCharacters.Add(CalculateMissingCharacters(index));

            List<long> score = new List<long>();

            foreach (var lineOfMissingCharacters in linesOfMissingCharacters)
                score.Add(ConvertMissingCharacterToPoint(lineOfMissingCharacters));

            score.Sort();
            var middleScoreIndex = (score.Count / 2);

            return score[middleScoreIndex];
        }

        private long ConvertMissingCharacterToPoint(List<char> lineOfMissingCharacters)
        {
            long score = 0;
            foreach(char character in lineOfMissingCharacters)
            {
                score *= 5;
                if (character.Equals(')')) score += 1;
                if (character.Equals(']')) score += 2;
                if (character.Equals('}')) score += 3;
                if (character.Equals('>')) score += 4;
            }

            return score;
        }
    }
}
