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
                return GetNextPosition(previousPosition.ToUpper(), new string('Z', previousPosition.Length + 1), string.Empty);
            else if (string.IsNullOrEmpty(previousPosition) && !string.IsNullOrEmpty(nextPosition))
                return GetNextPosition("AAAA", nextPosition.ToUpper(), string.Empty);
            else
                return GetNextPosition(previousPosition.ToUpper(), nextPosition.ToUpper(), string.Empty);
        }

        #endregion

        #region Private Methods

        private string GetFirstPosition()
        {
            return "AAAB";
        }

        private string GetNextPosition(string previousPosition, string nextPosition, string currentPosition)
        {
            if (!string.IsNullOrEmpty(currentPosition))
            {
                int compareToPrevious = currentPosition.CompareTo(previousPosition),
                compareToNext = currentPosition.CompareTo(nextPosition);

                if (compareToPrevious > 0 && compareToNext < 0)
                    return currentPosition;
            }

            var allPrevChars = previousPosition.ToCharArray();
            var allNextChars = nextPosition.ToCharArray();
            int newChar;

            if (previousPosition.Length > nextPosition.Length)
            {
                for (int i = previousPosition.Length - 1; i >= 0; i--)
                {
                    int currentPrevChar = allPrevChars[i],
                        currentNextChar = i <= allNextChars.Length - 1 ? allNextChars[i] : 0,
                        currentPrevChar2 = i > 0 ? allPrevChars[i - 1] : currentPrevChar;

                    if (currentPrevChar == letterZ)
                    {
                        if ((currentNextChar - currentPrevChar2) > 1)
                        {
                            var tryNewPosition = allNextChars;
                            tryNewPosition[tryNewPosition.Length - 1] = (char)(currentNextChar - 1);
                            var newPosition = GetNextPosition(previousPosition, nextPosition, new string(tryNewPosition));
                            if (!string.IsNullOrEmpty(newPosition))
                                return newPosition;
                        }
                        else
                        {
                            var tryNewPosition = new char[allPrevChars.Length + 1];
                            for (int j = 0; j < allPrevChars.Length; j++)
                                tryNewPosition[j] = allPrevChars[j];
                            tryNewPosition[tryNewPosition.Length - 1] = (char)letterA;
                            var newPosition = GetNextPosition(previousPosition, nextPosition, new string(tryNewPosition));
                            if (!string.IsNullOrEmpty(newPosition))
                                return newPosition;
                        }
                    }
                    else
                    {
                        if ((currentPrevChar + jump) > letterZ)
                        {
                            var tryNewPosition = allPrevChars;
                            newChar = letterZ - ((letterZ - currentPrevChar) / 2);
                            tryNewPosition[tryNewPosition.Length - 1] = (char)newChar;
                            var newPosition = GetNextPosition(previousPosition, nextPosition, new string(tryNewPosition));
                            if (!string.IsNullOrEmpty(newPosition))
                                return newPosition;
                        }
                        else
                        {
                            var tryNewPosition = allPrevChars;
                            tryNewPosition[tryNewPosition.Length - 1] = (char)(currentPrevChar + jump);
                            var newPosition = GetNextPosition(previousPosition, nextPosition, new string(tryNewPosition));
                            if (!string.IsNullOrEmpty(newPosition))
                                return newPosition;
                        }
                    }
                }
            }
            else if (previousPosition.Length < nextPosition.Length)
            {
                for (int i = nextPosition.Length - 1; i >= 0; i--)
                {
                    int currentNextChar = allNextChars[i];

                    newChar = letterA + ((currentNextChar - letterA) / 2);
                    var tryNewPosition = new char[allPrevChars.Length + 1];
                    for (int j = 0; j < allPrevChars.Length; j++)
                        tryNewPosition[j] = allPrevChars[j];

                    tryNewPosition[tryNewPosition.Length - 1] = (char)newChar;
                    var newPosition = GetNextPosition(previousPosition, nextPosition, new string(tryNewPosition));
                    if (!string.IsNullOrEmpty(newPosition))
                        return newPosition;
                }
            }
            else
            {
                for (int i = previousPosition.Length - 1; i >= 0; i--)
                {
                    char currentPrevChar = allPrevChars[i],
                         currentNextChar = allNextChars[i];

                    if ((currentNextChar - currentPrevChar) > 1)
                    {
                        newChar = currentPrevChar + ((currentNextChar - currentPrevChar) / 2);
                        var tryNewPosition = allPrevChars;
                        tryNewPosition[tryNewPosition.Length - 1] = (char)newChar;
                        var newPosition = GetNextPosition(previousPosition, nextPosition, new string(tryNewPosition));
                        if (!string.IsNullOrEmpty(newPosition))
                            return newPosition;
                    }
                    else
                    {
                        var tryNewPosition = new char[allPrevChars.Length + 1];
                        for (int j = 0; j < allPrevChars.Length; j++)
                            tryNewPosition[j] = allPrevChars[j];

                        tryNewPosition[tryNewPosition.Length - 1] = (char)(letterA + jump);
                        var newPosition = GetNextPosition(previousPosition, nextPosition, new string(tryNewPosition));
                        if (!string.IsNullOrEmpty(newPosition))
                            return newPosition;
                    }
                }
            }

            return string.Empty;
        }

        #endregion
    }
}
