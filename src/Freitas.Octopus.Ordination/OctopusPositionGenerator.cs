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
        private readonly int totalNewPositions = 4;

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
                //return GetNextPosition(previousPosition.ToUpper(), new string('Z', previousPosition.Length + 1), string.Empty);
                return GetNextPosition(previousPosition.ToUpper(), string.Empty, string.Empty);
            else if (string.IsNullOrEmpty(previousPosition) && !string.IsNullOrEmpty(nextPosition))
                return GetNextPosition("AAAA", nextPosition.ToUpper(), string.Empty);
            else
                return GetNextPosition(previousPosition.ToUpper(), nextPosition.ToUpper(), string.Empty);
        }

        #endregion

        #region Private Methods

        private char[] CopyArrayFrom(char[] copyFrom)
        {
            char[] newArray = new char[copyFrom.Length];
            for (int i = 0; i < copyFrom.Length; i++)
                newArray[i] = copyFrom[i];

            return newArray;
        }

        private string GetFirstPosition()
        {
            return "AAAAAB";
        }

        private string GetNextPosition(string previousPosition, string nextPosition, string currentPosition)
        {
            if (!string.IsNullOrEmpty(currentPosition))
            {
                string nextCompare = nextPosition;
                if (string.IsNullOrEmpty(nextPosition))
                    nextCompare = new string('Z', previousPosition.Length + 1);

                int compareToPrevious = currentPosition.CompareTo(previousPosition),
                compareToNext = currentPosition.CompareTo(nextCompare);

                if (compareToPrevious > 0 && compareToNext < 0)
                    return currentPosition;
            }

            char[] allPrevChars = previousPosition.ToCharArray();
            char[] allNextChars = null;
            int newChar;

            if (!string.IsNullOrEmpty(previousPosition) && string.IsNullOrEmpty(nextPosition))
            {
                var newPositionChars = CopyArrayFrom(allPrevChars);

                for (int i = newPositionChars.Length - 1; i >= 0; i--)
                {

                    if ((newPositionChars[i] + jump) > letterZ)
                    {
                        if (i == 0)
                        {
                            if (newPositionChars[i] != letterZ)
                            {
                                newPositionChars[i] = (char)(letterZ - ((letterZ - newPositionChars[i]) / 2));
                                return GetNextPosition(previousPosition, nextPosition, new string(newPositionChars));
                            }
                            else
                            {
                                newPositionChars = new char[allPrevChars.Length + totalNewPositions];
                                for (int j = 0; j < allPrevChars.Length; j++)
                                    newPositionChars[j] = allPrevChars[j];

                                for (int j = allPrevChars.Length; j < allPrevChars.Length + totalNewPositions; j++)
                                    newPositionChars[j] = (char)letterA;

                                return GetNextPosition(previousPosition, nextPosition, new string(newPositionChars));
                            }
                        }

                        if (newPositionChars[i] == letterZ)
                        {
                            newPositionChars[i] = (char)letterA;
                            continue;
                        }

                        newPositionChars[i] = (char)(letterZ - ((letterZ - newPositionChars[i]) / 2));
                        return GetNextPosition(previousPosition, nextPosition, new string(newPositionChars));
                    }
                    else
                    {
                        newPositionChars[i] = (char)(newPositionChars[i] + jump);
                        return GetNextPosition(previousPosition, nextPosition, new string(newPositionChars));
                    }
                }

                return GetNextPosition(previousPosition, nextPosition, new string(newPositionChars));
            }

            allNextChars = nextPosition.ToCharArray();

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
                            var tryNewPosition = CopyArrayFrom(allNextChars);
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
                            var tryNewPosition = CopyArrayFrom(allPrevChars);
                            newChar = letterZ - ((letterZ - currentPrevChar) / 2);
                            tryNewPosition[tryNewPosition.Length - 1] = (char)newChar;
                            var newPosition = GetNextPosition(previousPosition, nextPosition, new string(tryNewPosition));
                            if (!string.IsNullOrEmpty(newPosition))
                                return newPosition;
                        }
                        else
                        {
                            var tryNewPosition = CopyArrayFrom(allPrevChars);
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
                        var tryNewPosition = CopyArrayFrom(allPrevChars);
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
