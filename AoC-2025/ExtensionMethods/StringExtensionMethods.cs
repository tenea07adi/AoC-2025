using AoC_2025.Constants;

namespace AoC_2025.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static IEnumerable<string> SplitInChunks(this string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public static char[,] ToCharMatrix(this string str)
        {
            var data = str.Split(FileConstants.EndOfLine);

            return data.ToCharMatrix();
        }

        public static char[,] ToCharMatrix(this string[] data)
        {
            char[,] charMatrix = new char[data.Length, data[0].Length];

            for (int i = 0; i < data.Length; i++)
            {
                var line = data[i].ToCharArray();

                for (int j = 0; j < line.Length; j++)
                {
                    charMatrix[i, j] = line[j];
                }
            }

            return charMatrix;
        }
    }
}
