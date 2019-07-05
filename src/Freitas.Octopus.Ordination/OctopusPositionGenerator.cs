namespace Freitas.Octopus
{
    public sealed class OctopusPositionGenerator
    {
        #region Constructors

        private OctopusPositionGenerator()
        {
        }

        #endregion

        #region Fields

        private static OctopusPositionGenerator instance;
        private readonly int letterA = 'A';
        private readonly int letterZ = 'Z';
        private readonly int jump = 4;

        #endregion

        #region Properties

        public static OctopusPositionGenerator Instance
        {
            get
            {
                if (instance == null)
                    instance = new OctopusPositionGenerator();

                return instance;
            }
        }

        #endregion

        #region Public Methods

        public string GeneratePositionValue(string previousPosition = null, string nextPosition = null)
        {
            if (string.IsNullOrEmpty(previousPosition) && string.IsNullOrEmpty(nextPosition))
                return GetFirstPosition();
            else if (!string.IsNullOrEmpty(previousPosition) && string.IsNullOrEmpty(nextPosition))
                return GetNextPosition(previousPosition.ToUpper());
            else if (string.IsNullOrEmpty(previousPosition) && !string.IsNullOrEmpty(nextPosition))
                return GetInnerPosition("AAAA", nextPosition.ToUpper());
            else
                return GetInnerPosition(previousPosition.ToUpper(), nextPosition.ToUpper());
        }

        #endregion

        #region Private Methods

        private string GetFirstPosition()
        {
            return "AAAB";
        }

        private string GetNextPosition(string previousPosition)
        {
            var allChars = previousPosition.ToCharArray();
            for (int i = previousPosition.Length - 1; i >= 0; i--)
            {
                if (allChars[i] != letterZ && (allChars[i] + jump) > letterZ)
                {
                    allChars[i] = (char)(allChars[i] + (letterZ - allChars[i]));
                    break;
                }
                else if (i == (previousPosition.Length - 1) && allChars[i] == letterZ)
                {
                    allChars[i] = (char)letterA;
                    continue;
                }

                if ((char)(allChars[i] + jump) > letterZ)
                {
                    allChars[i - 1] = (char)(allChars[i - 1] + jump);
                    allChars[i] = (char)letterA;
                }
                else
                {
                    allChars[i] = (char)(allChars[i] + jump);
                }

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
                lastNextChar = allNextChars[allNextChars.Length - 1],
                penultimatePrevChar = allPrevChars[allPrevChars.Length - 2];

            if (lastPrevChar == letterZ)
            {
                if ((lastNextChar - penultimatePrevChar) > 1)
                {
                    //AAAAZ
                    //AAAB <----
                    //AAAC
                    var newPosition = allNextChars;
                    newPosition[newPosition.Length - 1] = (char)(lastNextChar - 1);
                    return new string(newPosition);
                }
                else
                {
                    //AAAAZ
                    //AAAAZA <----
                    //AAAB
                    var newPosition = new char[allPrevChars.Length + 1];
                    for (int i = 0; i < allPrevChars.Length; i++)
                        newPosition[i] = allPrevChars[i];
                    newPosition[newPosition.Length - 1] = (char)letterA;
                    return new string(newPosition);
                }
            }
            else
            {
                if ((lastPrevChar + jump) > letterZ)
                {
                    //AAAAY
                    //AAAAZ <----
                    //AAAB

                    //AAAAW
                    //AAAAY <----
                    //AAAB
                    var newPosition = allPrevChars;
                    int newChar = letterZ - ((letterZ - lastPrevChar) / 2);
                    newPosition[newPosition.Length - 1] = (char)newChar;
                    return new string(newPosition);
                }
                else
                {
                    //AAAAE
                    //AAAAI <----
                    //AAAB
                    var newPosition = allPrevChars;
                    newPosition[newPosition.Length - 1] = (char)(lastPrevChar + jump);
                    return new string(newPosition);
                }
            }
        }

        #endregion
    }
}
