using System.Collections;
using System.Collections.Generic;

namespace Freitas.Octopus.Ordination
{
    public sealed class TextOrdination
    {
        #region Constructors

        private TextOrdination()
        {
        }

        #endregion

        #region Fields

        private static TextOrdination instance;
        private int letterA = (int)'A', //65
                    letterZ = (int)'Z', //90
                    jump = 4;

        #endregion

        #region Properties

        public static TextOrdination Instance
        {
            get
            {
                if (instance == null)
                    instance = new TextOrdination();

                return instance;
            }
        }

        #endregion

        #region Public Methods

        public string GenerateOrdination(string previousPosition = null, string nextPosition = null)
        {
            if (string.IsNullOrEmpty(previousPosition))
                return GetFirstPosition();
            else if (string.IsNullOrEmpty(nextPosition))
                return GetLastPosition(previousPosition.ToUpper());
            else
                return GetInnerPosition(previousPosition.ToUpper(), nextPosition.ToUpper());
        }

        #endregion

        #region Private Methods

        private string GetFirstPosition()
        {
            return "AAAA";
        }

        private string GetLastPosition(string previousPosition)
        {
            var allChars = previousPosition.ToCharArray();
            for (int i = previousPosition.Length - 1; i >= 0; i--)
            {
                if (allChars[i] != letterZ && (allChars[i] + jump) > letterZ)
                {
                    allChars[i] = 'Z';
                    continue;
                }
                else if (allChars[i] == letterZ)
                {
                    allChars[i] = 'A';
                    continue;
                }

                allChars[i] = (char)(allChars[i] + jump);
                break;
            }

            return new string(allChars);
        }

        private string GetInnerPosition(string previousPosition, string nextPosition)
        {
            if (previousPosition.Length == nextPosition.Length)
                return GetInnerPositionSameLenght(previousPosition, nextPosition);
            else if (nextPosition.Length > previousPosition.Length)
                return GetInnerPositionNextGTPrevious(previousPosition, nextPosition);
            else
                return GetInnerPositionPreviousGTNext(previousPosition, nextPosition);
        }

        private string GetInnerPositionSameLenght(string previousPosition, string nextPosition)
        {
            char[] allPrevChars = previousPosition.ToCharArray(),
                   allNextChars = nextPosition.ToCharArray();

            int lastPrevChar = allPrevChars[allPrevChars.Length - 1],
                lastNextChar = allNextChars[allNextChars.Length - 1];

            int newChar;
            if ((lastNextChar - lastPrevChar) > 1)
            {
                newChar = lastPrevChar + ((lastNextChar - lastPrevChar) / 2);
                var newPosition = allPrevChars;
                newPosition[newPosition.Length - 1] = (char)newChar;
                return new string(newPosition);
            }
            else
            {
                var newPosition = new char[allPrevChars.Length + 1];
                for (int i = 0; i < allPrevChars.Length; i++)
                    newPosition[i] = allPrevChars[i];

                newPosition[newPosition.Length - 1] = (char)(letterA + jump);
                return new string(newPosition);
            }
        }

        private string GetInnerPositionNextGTPrevious(string previousPosition, string nextPosition)
        {
            char[] allPrevChars = previousPosition.ToCharArray(),
                   allNextChars = nextPosition.ToCharArray();

            int lastNextChar = allNextChars[allNextChars.Length - 1];

            int newChar = letterA + ((lastNextChar - letterA) / 2);
            var newPosition = new char[allPrevChars.Length + 1];
            for (int i = 0; i < allPrevChars.Length; i++)
                newPosition[i] = allPrevChars[i];

            newPosition[newPosition.Length - 1] = (char)newChar;
            return new string(newPosition);
        }

        private string GetInnerPositionPreviousGTNext(string previousPosition, string nextPosition)
        {
            char[] allPrevChars = previousPosition.ToCharArray(),
                   allNextChars = nextPosition.ToCharArray();

            int lastPrevChar = allPrevChars[allPrevChars.Length - 1],
                lastNextChar = allNextChars[allNextChars.Length - 1];

            
            if ((lastPrevChar + jump) > letterZ)
            {
                int penultimatePrevChar = (int)allPrevChars[allPrevChars.Length - 2];
                if ((lastNextChar - penultimatePrevChar) == 1)
                {
                    //AAAAY
                    //AAAAAE --este é o valor correto neste caso
                    //AAAB
                    char[] allNewChars = new char[allPrevChars.Length + 1];
                    
                }
                else
                {
                    //AAAAY
                    //AAACA --este é o valor correto neste caso
                    //AAAE
                }
            }

            return string.Empty;
        }

        #endregion
    }
}
