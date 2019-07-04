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
                if (((int)allChars[i] + jump) > letterZ)
                {
                    allChars[i] = 'A';
                    continue;
                }

                allChars[i] = (char)((int)allChars[i] + jump);
                break;
            }

            return new string(allChars);
        }

        private string GetInnerPosition(string previousPosition, string nextPosition)
        {
            char[] allPrevChars = previousPosition.ToCharArray(),
                   allNextChars = nextPosition.ToCharArray();

            int lastPrevChar = (int)allPrevChars[allPrevChars.Length - 1],
                lastNextChar = (int)allNextChars[allNextChars.Length - 1];

            if (previousPosition.Length == nextPosition.Length)
            {
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
            else if (nextPosition.Length > previousPosition.Length)
            {
                int newChar = letterA + ((lastNextChar - letterA) / 2);
                var newPosition = new char[allPrevChars.Length + 1];
                for (int i = 0; i < allPrevChars.Length; i++)
                    newPosition[i] = allPrevChars[i];

                newPosition[newPosition.Length - 1] = (char)newChar;
                return new string(newPosition);
            }
            else
            {
                //string lastPosition = GetLastPosition(previousPosition);
                return string.Empty;
                //TODO: Pensar nesta lógica:

                //AAAAY
                //AAAAAE --este é o valor correto neste caso
                //AAAB

                //AAAAY
                //AAACA --este é o valor correto neste caso
                //AAAE
            }
        }

        #endregion
    }
}
