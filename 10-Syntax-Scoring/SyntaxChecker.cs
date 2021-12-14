using System;
using System.Collections.Generic;
using System.Text;

namespace _10_Syntax_Scoring
{
    public class SyntaxChecker
    {
        public string[] FileLines { get; private set; }

        public void LoadFile(string[] fileLines)
        {
            FileLines = fileLines;
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

            for (int index = 0; index < FileLines.Length; index++)
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
    }
}
